using Godot;
using System;

public partial class MarkedMechanicComponent : Node2D
{
	[Export]
	private Timer cooldownTimer;

	private bool isMechanicActive = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _OnCooldownTimerTimeout()
	{
		// find a player
		var scene = GD.Load<PackedScene>("res://Components/Mechanics/MarkedMechanicComponent/MarkedDebuffComponent/MarkedDebuffComponent.tscn");
		MarkedDebuffComponent instance = (MarkedDebuffComponent)scene.Instantiate();
		
		// // Get player's position
		Player player = Core.instance.GetNearestPlayer(this);
	
		// add a debuff to the player
		player.AddChild(instance);
		
		// start cooldown again
		cooldownTimer.Start( 20 );
	}
}
