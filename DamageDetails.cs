using Godot;
using System;

public partial class DamageDetails : Control
{
	[Export]
	public Label label0;
	
	[Export]
	public Label label1;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		label0.Text = DamageMeter.instance.dmgText0;
		label1.Text = DamageMeter.instance.dmgText1;
	}
}
