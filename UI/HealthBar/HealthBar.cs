using Godot;
using System;

public partial class HealthBar : ProgressBar
{
	[Export]
	private ProgressBar damageBar;
	
	[Export]
	private Timer timer; 

    private float health;
	public float Health 	
	{
		get => health;
		set 
		{
			float previousHealth = health;
			health = value;
			Value = value;
			timer.Start();
			
		}
	}
	public void _OnTimerTimeout()
	{
		damageBar.Value = health;
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void initHealth(float health)
	{
		this.Health = health;
		MaxValue = health;
		Value = health;
		damageBar.MaxValue = health;
		damageBar.Value = health;
	}
}
