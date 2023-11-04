using Godot;
using System;
using System.Collections.Specialized;

public partial class slime : CharacterBody2D
{
	
	public AnimatedSprite2D animatedSprite2D;
	private float speed = 100;
	private bool isChasing = false;
	private Node2D player = null;
    private string currentAnimation = "";


    public override void _Ready()
    {
		
		this.animatedSprite2D = (AnimatedSprite2D) FindChild("AnimatedSprite2D");
		this.HandleAnimations();
    }
    public override void _PhysicsProcess(double delta)
	{
		if(this.isChasing == true )
		{
			// Vector2 playerPosition = GlobalPosition.DirectionTo(this.player.GlobalPosition);
			Velocity = GlobalPosition.DirectionTo(this.player.GlobalPosition) * speed;
			if(this.player.Position.X - Position.X > 0)
			{
				animatedSprite2D.FlipH = false;
			}
			else
			{
				animatedSprite2D.FlipH = true;	
			}

			MoveAndCollide( Velocity*(float)delta);
			// Position += (this.player.Position - Position) / speed;
			// Velocity = this.Position.MoveToward(playerPosition, (float)delta);
			// MoveAndSlide();
        	// MoveAndCollide(velocity * this.speed * delta);
			
			// Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
       		//  Velocity = inputDirection * speed;
       		// var collision = MoveAndCollide(Velocity*(float)delta);
			
		}
		HandleAnimations();
	}

	public void OnBodyEntered(Node2D body)
	{
		this.player = body;
		this.isChasing = true;
		
	}
	
	public void OnBodyExited(Node2D body)
	{
		
		GD.Print("0");
		this.player = null;
		this.isChasing = false;
	}

	public void HandleAnimations()
	{
		if(this.isChasing == true && this.currentAnimation != "running")
		{
			this.currentAnimation = "running";
			this.animatedSprite2D.Play(this.currentAnimation);
		}
		if( this.isChasing == false && this.currentAnimation != "idle" )
		{
			this.currentAnimation = "idle";
			this.animatedSprite2D.Play(this.currentAnimation);
		}
	}
}
