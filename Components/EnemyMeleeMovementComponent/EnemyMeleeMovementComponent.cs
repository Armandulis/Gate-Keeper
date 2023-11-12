using Godot;
using System;
using System.Linq;

public partial class EnemyMeleeMovementComponent : Node2D
{
	private float speed = 50;

    public bool Chase(CharacterBody2D body, float delta)
    {
		Player nearestPlayer = Core.instance.GetNearestPlayer(body);
		
		body.Velocity = GlobalPosition.DirectionTo(nearestPlayer.GlobalPosition) * speed;
		
		body.MoveAndCollide( body.Velocity*delta);

		return true;
    }
}
