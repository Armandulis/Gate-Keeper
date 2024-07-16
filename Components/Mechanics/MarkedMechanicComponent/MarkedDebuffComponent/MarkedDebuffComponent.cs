using Godot;
using System;

public partial class MarkedDebuffComponent : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _OnDurationTimerTimeout()
	{
		// Create a puddle
			var scene = GD.Load<PackedScene>("res://Components/Mechanics/MarkedMechanicComponent/PuddleMechanicComponent/PuddleMechanicComponent.tscn");
			PuddleMechanicComponent puddleMechanicComponent = (PuddleMechanicComponent)scene.Instantiate();
			
			puddleMechanicComponent.Position = GlobalPosition;
		
			GetParent().AddChild(puddleMechanicComponent);
			puddleMechanicComponent.TopLevel = true;

		// remove self
		QueueFree();
	}
}
