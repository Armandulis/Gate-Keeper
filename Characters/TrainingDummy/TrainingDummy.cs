using Godot;
using System;

public partial class TrainingDummy : CharacterBody2D
{
	string currentAnimation = "";
    public override void _Ready()
    {
		HealthComponent hp = (HealthComponent)FindChild("HealthComponent");
		AnimationPlayer player = (AnimationPlayer)FindChild("AnimationPlayer");
		hp.MySignal += () => {
		Random rnd = new Random();
		int month  = rnd.Next(1, 4);
		if( month == 1)
		{
			player.Play("DotHit");
		}
		if( month == 2 )
		{
			player.Play("NormalHit");
		}
		if( month == 3 )
		{
			player.Play("CritHit");
		}
		};
    }
    public override void _PhysicsProcess(double delta)
	{
	}

}
