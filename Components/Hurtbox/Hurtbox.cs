using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Hurtbox : Area2D
{
	private Dictionary<string, SpellMetadata> dots = new Dictionary<string, SpellMetadata>();
	private Dictionary<SpellMetadata, double> dotRemainingTimes = new Dictionary<SpellMetadata, double>();


	
	private double timer = 0;
	
	[Export]
	public HealthComponent healthComponent;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		timer += delta;

		foreach( KeyValuePair<string, SpellMetadata> entry in dots.ToList() )
		{
			SpellMetadata dot = entry.Value;

			if (!dotRemainingTimes.ContainsKey(dot))
            {
                // Start the timer for this dot
                dotRemainingTimes[dot] = dot.dotInterval;
            }
			  dotRemainingTimes[dot] -= delta;

 			if (dotRemainingTimes[dot] <= 0)
            {
				
				GD.Print("inside loop" + dot.id);
                healthComponent.Damage(dot);

                // Reset remaining time for the next interval
                dotRemainingTimes[dot] = dot.dotInterval;

                dot.dotDuration -= (float)delta;

                // If the dot has expired, remove it from the dictionary
                if (dot.dotDuration <= 0)
                {
                    dotRemainingTimes.Remove(dot);
                    dots.Remove(entry.Key);
                }
            }
		}
	}
	
	public void OnHurtboxAreaEntered(Hitbox hitbox)
	{	
		if(hitbox == null)
		{
			return;
		}

		if(hitbox.spellMetadata.isDot)
		{
			dots[hitbox.spellMetadata.id] = hitbox.spellMetadata;
		}

		healthComponent.Damage( hitbox.spellMetadata );
	}

	public void OnHurtboxAreaExited(Hitbox hitbox)
	{	
		GD.Print(hitbox.spellMetadata.spellId);
		if(hitbox == null || !hitbox.spellMetadata.isAOEDot )
		{
			return;
		}
		
		GD.Print("leavbing eara" + hitbox.spellMetadata.id);

		dots.Remove(hitbox.spellMetadata.id);
		dotRemainingTimes.Remove(hitbox.spellMetadata);
	}
}
