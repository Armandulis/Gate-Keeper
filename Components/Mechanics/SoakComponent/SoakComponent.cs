using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class SoakComponent : Node2D
{
	[Export]
	public CastBarComponent castBarComponent;

	[Export]
	public Timer cooldownTimer;

	private bool isCasting = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cooldownTimer.Start( 20 );
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
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
		Rpc(method: "CastSingularity", GetGlobalMousePosition());
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	private void CastSingularity(Vector2 aim)
	{
			var scene = GD.Load<PackedScene>("res://Components/Mechanics/SoakComponent/SoakPuddleComponent/SoakPuddleComponent.tscn");
			SoakPuddleComponent instance = (SoakPuddleComponent)scene.Instantiate();
			
			  // Get player's position
            Vector2 playerPosition = Core.instance.GetNearestPlayer(this).GlobalPosition;

			Random rnd = new Random();
            // Add random offset within 100 pixels around the player
            float offsetX =  rnd.Next(-50, 51);
            float offsetY = rnd.Next(-50, 51);
            Vector2 randomPosition = playerPosition + new Vector2(offsetX, offsetY);

			instance.Position = randomPosition;
		
			AddChild(instance);
			instance.TopLevel = true;
			cooldownTimer.Start( 24 );
	}

	public void _OnCooldownTimerTimeout()
	{

		castFinished();	
	}
}
