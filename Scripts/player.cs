using Godot;
using System;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading;
using System.Xml.Schema;

public partial class Player : CharacterBody2D
{
	public AnimatedSprite2D animatedSprite2D;

	private string currentDirrection = "down";
    private string currentAnimation = "";
	private bool isRunning = false;

	private bool isInAttachRangeEnemy = false;
	private bool isInCooldownEnemy = false;
	private float health = 100;
	private bool isAlive = true;
	private Godot.Timer attackCooldown;
	private Godot.Timer playerAttackCooldown;
	private Move move;

    public override void _Ready()
    {
		this.attackCooldown = (Godot.Timer) FindChild("AttackCooldown");
		this.animatedSprite2D = (AnimatedSprite2D) FindChild("AnimatedSprite2D");
		this.HandleAnimations();
		this.playerAttackCooldown = (Godot.Timer) FindChild("PlayerAttackCooldown");

		this.move = LoadAction();
    }
    public override void _Process(double delta)
	{
	}


	public override void _PhysicsProcess(double delta)
	{
		this.move.Execute(this);
		this.EnemyAttack();
		this.Attack();
		// Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        // Velocity = inputDirection * speed;
        // var collision = MoveAndCollide(Velocity*(float)delta);
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
			this.health -= 10;
			this.isInCooldownEnemy = true;
			this.attackCooldown.Start();
			GD.Print(this.health);
			if( this.health <= 0 )
			{
				this.isAlive = false;
			}
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

	public Move LoadAction()
	{
		PackedScene scene = GD.Load<PackedScene>("res://Actions/Move/Move.tscn");
		Node sceneNode = scene.Instantiate();
		AddChild(sceneNode);

		return (Move)sceneNode;
			//  if(sceneNode.GetScript().Obj.GetType().FullName is Move moveScript)
		// {
		// 	return moveScript;
		// }

		// return null;
	
	}

	public Variant GetNodeScript(Node scene)
	{
		return scene.GetScript();
	}
}