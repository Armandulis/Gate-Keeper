using Godot;
using System;

public partial class Bolt : CharacterBody2D
{
	private Vector2 velocity;
	private Vector2 aim;
    public override void _Ready()
    {
		aim = GlobalPosition.DirectionTo( GetGlobalMousePosition());
		Position = GetParent<Player>().GlobalPosition;
		TopLevel = true;
		LookAt(aim);
    }
    public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(aim.Normalized() * (float)delta * 1000);
	}
}
