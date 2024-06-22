using Godot;
using System;

public partial class SoakPuddleComponent : Node2D
{
	
	[Export]
	public CastBarComponent castBarComponent;
    private bool disarmed = false;
	private GlobalDamageComponent globalDamageComponent;
	[Export]
	private Timer timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
				// ProgressBar progressBar = (ProgressBar)castBarComponent.progressBar;
		castBarComponent.CastFinished += () => {
			explode();
		};
		
			castBarComponent.startCast( 5 );
	}

	public void explode()
	{
		if( !disarmed )
		{
			Rpc(method: "CastSingularity" );
		}
		else
		{
			QueueFree();
		}
		
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	private void CastSingularity()
	{
			var scene = GD.Load<PackedScene>("res://Components/Mechanics/GlobalDamageComponent/GlobalDamageComponent.tscn");
			globalDamageComponent = (GlobalDamageComponent)scene.Instantiate();
			
			globalDamageComponent.Position = GlobalPosition;
		
			AddChild(globalDamageComponent);
			globalDamageComponent.TopLevel = true;
			timer.Start(0.5);
	}
	
	public void _OnTimerTimeout()
	{
		QueueFree();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	public void _OnSoakPuddleEntered( Area2D area2D )
	{
		disarmed = true;
	}

	public void _OnSoakPuddleExited( Area2D area2D  )
	{
		disarmed = false;
	}
}
