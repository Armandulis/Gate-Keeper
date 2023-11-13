using Godot;
using System;

public partial class Bolt : CharacterBody2D
{
	private Vector2 aim;
    public override void _Ready()
    {	
		Hitbox hitbox = (Hitbox)FindChild("Hitbox");
		if(GetParent<Node2D>().GetParentOrNull<Player>() != null)
		{
			aim = GlobalPosition.DirectionTo( GetGlobalMousePosition());
			hitbox.CollisionLayer = 8;
			
		}
		else
		{
			aim = GlobalPosition.DirectionTo(Core.instance.GetNearestPlayer(this).GlobalPosition);
			hitbox.CollisionLayer = 16;
		}
		Position = GetParent<Node2D>().GetParent<Node2D>().GlobalPosition;
		TopLevel = true;
		LookAt(aim);
    }

    public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(aim.Normalized() * (float)delta * 1000);
	}
}
