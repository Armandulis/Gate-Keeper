using Godot;


public partial class PlayerMovementComponent : Node2D
{
    public float baseSpeed = 100;
    public float dashSpeed = 600;
	public float speed = 100;
    
    public Vector2 inputDirection;
    public void Execute(CharacterBody2D body)
    {
        if( speed != dashSpeed)
        {
            inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        }
        body.Velocity = inputDirection * speed;
		body.MoveAndSlide();
    }
}
