using Godot;


public partial class PlayerMovementComponent : Node2D
{
	private float speed = 100;

    public void Execute(CharacterBody2D body)
    {
        Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        body.Velocity = inputDirection * speed;
		body.MoveAndSlide();
    }
}
