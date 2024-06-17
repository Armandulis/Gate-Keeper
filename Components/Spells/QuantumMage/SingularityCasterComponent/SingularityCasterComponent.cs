using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class SingularityCasterComponent : Node2D
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

		interruptedCast = player.isMoving;

		if( interruptedCast && isCasting)
		{
			isCasting = false;
			castBarComponent.castInerupted();
		}

		if( Input.IsKeyPressed( Key.Key1 ) && !isCasting )
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
			castBarComponent.startCast();
	}


	public void castFinished()
	{
		isCasting = false;
		gravitonBuffComponent.addStacks( 0.5f );
		Rpc(method: "CastSingularity", GetGlobalMousePosition());
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	private void CastSingularity(Vector2 aim)
	{
			var scene = GD.Load<PackedScene>("res:///Components/BoltCasterComponent/Bolt.tscn");
			Bolt instance = (Bolt)scene.Instantiate();
			
			instance.Position = GetParent<Node2D>().GlobalPosition;
			instance.aim = GlobalPosition.DirectionTo( aim);
		
			AddChild(instance);
			instance.TopLevel = true;
	}
}
