using Godot;
using System;

public partial class QuasarCasterComponent : Node2D
{
	
	[Export]
	public CastBarComponent castBarComponent;
	
	private bool isCasting = false;
	private bool interruptedCast = false;
	private Player player;
	private GravitonBuffComponent gravitonBuffComponent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (Player)GetParent<Player>();
		gravitonBuffComponent = player.gravitonBuffComponent;

		// ProgressBar progressBar = (ProgressBar)castBarComponent.progressBar;
		castBarComponent.CastFinished += () => {
			castFinished();
		};
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if( !player.isMultiplayerAuthority() )
		{
			return;
		}

		// interruptedCast = player.isMoving;

		// if( interruptedCast && isCasting)
		// {
		// 	isCasting = false;
		// 	castBarComponent.castInerupted();
		// }

		if( Input.IsKeyPressed( Key.Key2 ) && !isCasting && player.tryCastSpell() )
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
		player.finishedCastingSpell();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	private void CastQuasar(Vector2 aim)
	{
			var scene = GD.Load<PackedScene>("res:///Components/BoltCasterComponent/Bolt.tscn");
			Bolt instance = (Bolt)scene.Instantiate();
			
			instance.Position = GetParent<Node2D>().GlobalPosition;
			instance.aim = GlobalPosition.DirectionTo( aim);
		
			AddChild(instance);
			instance.TopLevel = true;
	}
}
