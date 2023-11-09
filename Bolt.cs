using Godot;
using System;

public partial class Bolt : CharacterBody2D
{
	private Vector2 pos;
	public override void _PhysicsProcess(double delta)
	{
		Velocity = GlobalPosition.DirectionTo( pos) * 300;
		MoveAndSlide();
	}

public override void _Input(InputEvent @event)
{
    // Mouse in viewport coordinates.
    if (@event is InputEventMouseButton eventMouseButton)
	this.pos = eventMouseButton.Position;
    else if (@event is InputEventMouseMotion eventMouseMotion)
        GD.Print("Mouse Motion at: ", eventMouseMotion.Position);

    // Print the size of the viewport.
    GD.Print("Viewport Resolution is: ", GetViewport().GetVisibleRect().Size);
}
}
