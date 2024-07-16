using Godot;
using System;

public partial class SpellButton : TextureButton
{
	[Export]
	private TextureProgressBar textureProgressBar;
	[Export]
	private Label timeLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateCooldownProgress( float timeLeft )
	{
		timeLabel.Text = timeLeft.ToString();
		textureProgressBar.Value = 50;

	}
}
