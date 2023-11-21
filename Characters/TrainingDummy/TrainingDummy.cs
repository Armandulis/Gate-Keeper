using Godot;
using System;

public partial class TrainingDummy : CharacterBody2D
{
	string currentAnimation = "";
    public override void _Ready()
    {
		HealthComponent hp = (HealthComponent)FindChild("HealthComponent");
		AnimationPlayer player = (AnimationPlayer)FindChild("AnimationPlayer");
		hp.Damaged += ( SpellMetadata spellMetadata) => {
			
		if( spellMetadata.isDot == true)
		{
			player.Play("DotHit");
		}
		if( spellMetadata.isCrit == false )
		{
			player.Play("NormalHit");
		}
		if( spellMetadata.isCrit == true )
		{
			player.Play("CritHit");
		}
		};
    }
    public override void _PhysicsProcess(double delta)
	{
	}

}
