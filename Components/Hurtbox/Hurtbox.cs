using Godot;
using System;

public partial class Hurtbox : Area2D
{
	
	[Export]
	public HealthComponent healthComponent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void OnHurtboxAreaEntered(Hitbox hitbox)
	{	
		if(hitbox == null)
		{
			return;
		}
		
		healthComponent.Damage(hitbox.spellMetadata);
	}
}
