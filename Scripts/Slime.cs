using Godot;
using System;
using System.Collections.Specialized;

public partial class Slime : CharacterBody2D
{
	
	public AnimatedSprite2D animatedSprite2D;
	private float speed = 100;
	private bool isChasing = false;
	private Node2D player = null;
    private string currentAnimation = "";
	private float health = 100;
	private bool isPlayerInRange = false;



    public override void _Ready()
    {
		
		this.animatedSprite2D = (AnimatedSprite2D) FindChild("AnimatedSprite2D");
		this.HandleAnimations();
		
    }
    public override void _PhysicsProcess(double delta)
	{
		
		dealDamage();	
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

	public void Enemy()
	{

	}

	public void OnHitboxBodyEntered(Node2D body)
	{
		if(body.HasMethod("PlayerClass") )
		{
			isPlayerInRange = true;
		}
	}

	public void OnHitboxBodyExited(Node2D body)
	{
		
		if(body.HasMethod("PlayerClass") )
		{
			isPlayerInRange = false;
		}
		
	}

	public void dealDamage()
	{
		if( isPlayerInRange && Core.instance.isPlayerAttacking )
		{
			health -= 10;
			GD.Print(health + "slime");
			if( health <= 0 )
			{
				// handle death;
			}
		}
	}
}
