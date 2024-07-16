using Godot;
using System;

public partial class QuasarCasterComponent : Node2D
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

		if( Input.IsKeyPressed( Key.Key2 ) && !isCasting && spellCastManagerComponent.tryCastSpell() )
		{
				startCast();	
		}
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	public void startCast()
	{
			isCasting = true;
			castBarComponent.startCast( 1 );
	}


	public void castFinished()
	{
		isCasting = false;
		gravitonBuffComponent.addStacks( 1 );
		Rpc(method: "CastQuasar", GetGlobalMousePosition());
		spellCastManagerComponent.finishedCastingSpell();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	private void CastQuasar(Vector2 aim)
	{
			var scene = GD.Load<PackedScene>("res:///Components/BoltCasterComponent/Bolt.tscn");
			Bolt instance = (Bolt)scene.Instantiate();
			
			instance.Position = GetParent<Node2D>().GlobalPosition;
			instance.aim = GlobalPosition.DirectionTo( aim);
			instance.damage = 75;
		
			GetParent<Node2D>().AddChild(instance);
			instance.TopLevel = true;
	}
}
