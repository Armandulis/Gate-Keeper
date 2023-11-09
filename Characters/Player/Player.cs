using Godot;
using System;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading;
using System.Xml.Schema;

public partial class Player : CharacterBody2D
{
	[Export]
	public HealthComponent healthComponent;
	public AnimatedSprite2D animatedSprite2D;

	private string currentDirrection = "down";
    private string currentAnimation = "";
	private bool isRunning = false;

	private bool isInAttachRangeEnemy = false;
	private bool isInCooldownEnemy = false;
	private Godot.Timer attackCooldown;
	private Godot.Timer playerAttackCooldown;

	private PlayerMovementComponent playerMovementComponent;

    public override void _Ready()
    {
		this.attackCooldown = (Godot.Timer) FindChild("AttackCooldown");
		this.animatedSprite2D = (AnimatedSprite2D) FindChild("AnimatedSprite2D");
		this.playerAttackCooldown = (Godot.Timer) FindChild("PlayerAttackCooldown");
		this.playerMovementComponent = (PlayerMovementComponent)FindChild("PlayerMovementComponent");
		
		this.HandleAnimations();
	}

    public override void _Process(double delta)
	{
	}


	public override void _PhysicsProcess(double delta)
	{
		if(Input.IsKeyPressed(Key.F))
		{
			var scene = GD.Load<PackedScene>("res://Bolt.tscn");
			var instance = scene.Instantiate();
			AddChild(instance);
		}
		this.playerMovementComponent.Execute(this);
		this.EnemyAttack();
		this.Attack();
		MoveAndSlide();
		this.isRunning = false;

		if (Input.IsKeyPressed(Key.W))
		{
			this.currentDirrection = "up";
			// this.Position += new Vector2(0, -5);
			
			this.isRunning = true;
		}
		if (Input.IsKeyPressed(Key.S))
		{
			this.currentDirrection = "down";
			// this.Position += new Vector2(0, 5);
			this.isRunning = true;
		}
		if (Input.IsKeyPressed(Key.A))
		{
			this.currentDirrection = "left";
			// this.Position += new Vector2(-5, 0);
			this.isRunning = true;
		}
		if (Input.IsKeyPressed(Key.D))
		{	
			this.currentDirrection = "right";
			// this.Position += new Vector2(5, 0);
			this.isRunning = true;
		}
		
		this.HandleAnimations();
	}

	public void HandleAnimations()
	{
		if(this.isRunning == true && this.currentAnimation != "running")
		{
			this.currentAnimation = "running";
			this.animatedSprite2D.Play(this.currentAnimation);
		}
		if( this.isRunning == false && this.currentAnimation != "idle" )
		{
			this.currentAnimation = "idle";
			this.animatedSprite2D.Play(this.currentAnimation);
		}


		if (this.currentDirrection == "up")
		{
		}

		if (this.currentDirrection == "down")
		{
		}

		if (this.currentDirrection == "right")
		{
			this.animatedSprite2D.FlipH = false;
		}


		if (this.currentDirrection == "left")
		{
			this.animatedSprite2D.FlipH = true;
		}
	}

	public void PlayerClass()
	{
		
	}

	public void Attack()
	{
		if( Input.IsKeyPressed(Key.F) )
		{
			Core.instance.isPlayerAttacking = true;
			this.playerAttackCooldown.Start();
		}
	}

	public void EnemyAttack()
	{
		if(this.isInAttachRangeEnemy && !isInCooldownEnemy )
		{
			healthComponent.Damage(10);
			this.isInCooldownEnemy = true;
			this.attackCooldown.Start();
		}
	}
	

	public void OnHitboxEntered(Node2D body)
	{
		if(body.HasMethod("Enemy"))
		{
			isInAttachRangeEnemy = true;
		}
	}

	
	public void OnHitboxExited(Node2D body)
	{
		if(body.HasMethod("Enemy"))
		{
			isInAttachRangeEnemy = false;
		}
	}

	public void OnAttackCooldownTimeout()
	{
		this.isInCooldownEnemy = false;
	}

	public void OnPlayerAttackCooldownTimeout()
	{
			this.playerAttackCooldown.Stop();
			Core.instance.isPlayerAttacking = false;
	}

	// public Node LoadAction()
	// {
	// 	PackedScene scene = GD.Load<PackedScene>("res://Components/PlayerMovement/PlayerMovement.tscn");
	// 	Node sceneNode = scene.Instantiate();
	// 	AddChild(sceneNode);
	// 	return sceneNode;
	// }
}