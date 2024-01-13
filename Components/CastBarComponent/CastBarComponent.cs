using Godot;
using System;
using System.ComponentModel;

public partial class CastBarComponent : Node2D
{
	[Export]
	public ProgressBar castBar;

	
	[Export]
	public Sprite2D interuptIcon;

	public Timer castTimer;

	private int interuptsNeeded;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TrainingDummy trainingDummy = GetParent<TrainingDummy>();
		castTimer = (Timer)trainingDummy.FindChild("CastTime");
		interuptsNeeded = Core.instance.GetTotalPlayers();

		// for (int i = 0; i < 5; i++)
		// {
		// 	var scene = GD.Load<PackedScene>("res:///Components/CastBarComponent/InteruptIcon.tscn");
		// 	Node2D instance = (Node2D)scene.Instantiate();
			
		// 	float offset = i * 10;
		// 	instance.Position = GlobalPosition;
			
		// 	// instance.Position. += offset;
		// 	AddChild(instance);
		// }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		castBar.Value = (5 - castTimer.TimeLeft) / 5 * 100;
	}
}
