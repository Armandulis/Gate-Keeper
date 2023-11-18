using Godot;
using System;

public partial class FPSCounter : Node2D
{
	private float cooldown = 2;
	private double timer = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timer += delta;
		if(timer > cooldown )
		{
			timer = 0;
			GD.Print(Engine.GetFramesPerSecond() );
		}
	}
}
