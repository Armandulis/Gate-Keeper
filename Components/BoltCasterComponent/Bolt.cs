using Godot;
using System;

public partial class Bolt : CharacterBody2D
{
	private Vector2 aim;
    public override void _Ready()
    {
	
		
		if(GetParent<Node2D>().GetParentOrNull<Player>() != null)
		{
			aim = GlobalPosition.DirectionTo( GetGlobalMousePosition());
		}
		else
		{
			aim = GlobalPosition.DirectionTo(Core.instance.GetNearestPlayer(this).GlobalPosition);
		}
		Position = GetParent<Node2D>().GetParent<Player>().GlobalPosition;
		TopLevel = true;
		LookAt(aim);
    }

    public override void _PhysicsProcess(double delta)
	{
		// GD.Print(aim);
		var collision = MoveAndCollide(aim.Normalized() * (float)delta * 1000);
	}
}
