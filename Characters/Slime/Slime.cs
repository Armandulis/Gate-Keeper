using Godot;
using System;
using System.Collections.Specialized;

public partial class Slime : CharacterBody2D
{
	[Export]
	public HealthComponent healthComponent;
	
	public AnimatedSprite2D animatedSprite2D;
	private float speed = 100;
	private bool isChasing = false;
	private Node2D player = null;
    private string currentAnimation = "";
	
	private BoltCasterComponent boltCasterComponent;

	[Export]
	private EnemyMeleeMovementComponent enemyMeleeMovementComponent;

    public override void _Ready()
    {
		this.boltCasterComponent = (BoltCasterComponent)FindChild("BoltCasterComponent");
		this.animatedSprite2D = (AnimatedSprite2D) FindChild("AnimatedSprite2D");
		
		this.HandleAnimations();
		
    }
    public override void _PhysicsProcess(double delta)
	{	
		boltCasterComponent.Cast();
		this.enemyMeleeMovementComponent.Chase(this, (float)delta);	
		if(this.isChasing == true )
		{
			if(this.player.Position.X - Position.X > 0)
			{
				animatedSprite2D.FlipH = false;
			}
			else
			{
				animatedSprite2D.FlipH = true;	
			}

		}
		HandleAnimations();
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

}
