using Godot;
using System;

public partial class GlobalDamageComponent : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		SpellMetadata spellMetadata = new SpellMetadata();
		Hitbox hitbox = (Hitbox)FindChild("Hitbox");
		
		spellMetadata.spellId = "GlobalDamage";
		hitbox.CollisionLayer = 16;
			
		spellMetadata.casterId = "Soulbinder";
		spellMetadata.value = 100;
		spellMetadata.isDot = false;;
		hitbox.spellMetadata = spellMetadata;
	}
}
