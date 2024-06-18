using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class QuantumFluxCasterComponent : Node2D
{	
	[Export]
	private Timer cooldownTimer;
	private Player player;
	private GravitonBuffComponent gravitonBuffComponent;
	private int stacks = 3;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (Player)GetParent<Player>();
		gravitonBuffComponent = player.gravitonBuffComponent;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if( !player.isMultiplayerAuthority() )
		{
			return;
		}


		if( Input.IsActionJustPressed( "Spell 3" ) )
		{

			castFinished();	
		}
	}

	public void castFinished()
	{
		if( stacks == 0)
		{
			return;
		}
		gravitonBuffComponent.addStacks( 1 );
		stacks -= 1;
		cooldownTimer.Start( 2.5 );
		Rpc(method: "CastSingularity", GetGlobalMousePosition());
	}

	public void OnCooldownTimerTimeout()
	{
		stacks += 1;
	
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
