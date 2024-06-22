using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class SingularityCasterComponent : Node2D
{
	
	[Export]
	public CastBarComponent castBarComponent;

	private bool isCasting = false;
	private bool interruptedCast = false;
	private QuantumMage quantumMage;
	private GravitonBuffComponent gravitonBuffComponent;
	private SpellCastManagerComponent spellCastManagerComponent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		quantumMage = (QuantumMage)GetParent<QuantumMage>();
		gravitonBuffComponent = quantumMage.gravitonBuffComponent;
		spellCastManagerComponent = quantumMage.spellCastManagerComponent;


		// ProgressBar progressBar = (ProgressBar)castBarComponent.progressBar;
		castBarComponent.CastFinished += () => {
			castFinished();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if( !spellCastManagerComponent.player.isMultiplayerAuthority() )
		{
			return;
		}

		interruptedCast = spellCastManagerComponent.castInterupted();

		if( interruptedCast && isCasting)
		{
			
			spellCastManagerComponent.finishedCastingSpell();
			isCasting = false;
			castBarComponent.castInerupted();
		}

		if( Input.IsKeyPressed( Key.Key1 ) && !isCasting && spellCastManagerComponent.tryCastSpell() )
		{
			if( gravitonBuffComponent.trySpendOn( 2 ) )
			{
				castFinished();
			}
			else
			{
				startCast();	
			}
		}
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	public void startCast()
	{
			isCasting = true;
			
			castBarComponent.startCast( 3 );
	}


	public void castFinished()
	{
		isCasting = false;
		gravitonBuffComponent.addStacks( 0.5f );
		Rpc(method: "CastSingularity", GetGlobalMousePosition());
		spellCastManagerComponent.finishedCastingSpell();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	private void CastSingularity(Vector2 aim)
	{

			var scene = GD.Load<PackedScene>("res:///Components/BoltCasterComponent/Bolt.tscn");
			Bolt instance = (Bolt)scene.Instantiate();
			
			instance.Position = GetParent<Node2D>().GlobalPosition;
			instance.aim = GlobalPosition.DirectionTo( aim);
		
			GetParent<Node2D>().AddChild(instance);
			instance.TopLevel = true;
	}
}
