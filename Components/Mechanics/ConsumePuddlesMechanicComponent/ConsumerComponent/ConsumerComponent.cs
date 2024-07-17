using Godot;
using System;

public partial class ConsumerComponent : Node2D
{
	[Export]
	private Timer consumeTimer;
	[Export]
	private HealthComponent healthComponent;
	[Export]
	private bool isMechanicActive = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if( healthComponent.hpPercentage <= 30 && isMechanicActive == false )
		{
			isMechanicActive = true;
			consumeTimer.Start(timeSec: 1);
		}
	}

	public void _OnConsumeTimerTimeout()
	{
		if( isMechanicActive == false)
		{
			return;
		}

		// Get all nodes in the "PuddleMechanicUnit" group
        var puddleNodes = GetTree().GetNodesInGroup("PuddleMechanicUnit");
        if (puddleNodes.Count == 0)
        {
            return;
        }
		
		Random random = new Random();
        
		// Select a random node from the list
        int randomIndex = random.Next(puddleNodes.Count);
		PuddleMechanicComponent puddleMechanicComponent = (PuddleMechanicComponent)puddleNodes[randomIndex];

		Vector2 position = puddleMechanicComponent.GlobalPosition;
		puddleMechanicComponent.QueueFree();

		var scene = GD.Load<PackedScene>("res://Components/Mechanics/ConsumePuddlesMechanicComponent/MovingOrbMechanicComponent/MovingOrbMechanicComponent.tscn");
		MovingOrbMechanicComponent movingOrbMechanicComponent = (MovingOrbMechanicComponent)scene.Instantiate();
		
		movingOrbMechanicComponent.Position = position;
	
		AddChild(movingOrbMechanicComponent);
		movingOrbMechanicComponent.TopLevel = true;

		consumeTimer.Start( 12 );
	}
}
