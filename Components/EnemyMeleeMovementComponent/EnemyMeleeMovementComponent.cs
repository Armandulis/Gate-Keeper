using Godot;
using System;
using System.Linq;

public partial class EnemyMeleeMovementComponent : Node2D
{
	private float speed = 50;

    public bool Chase(CharacterBody2D body, float delta)
    {
		// Find the nearest player and chase them
		Godot.Collections.Array<Node> players = GetTree().GetNodesInGroup("Players");
		
		if( players.Count == 0 )
		{
			return false;
		}
		Player nearestPlayer = (Player)players.First();
		foreach( Player player in players )
		{
			float distance = player.GlobalPosition.DistanceTo(body.GlobalPosition );
			float nearestDistance = nearestPlayer.GlobalPosition.DistanceTo(body.GlobalPosition);
			nearestPlayer = nearestDistance < distance ? nearestPlayer : player;
		}
		body.Velocity = GlobalPosition.DirectionTo(nearestPlayer.GlobalPosition) * speed;
		
		body.MoveAndCollide( body.Velocity*delta);

		return true;
    }
}
