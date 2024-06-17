using Godot;
using System;
using System.Reflection;
using System.Security.AccessControl;
using System.Threading;
using System.Xml.Schema;

public partial class Player : CharacterBody2D
{
	public string playerId = "player";
	public AnimatedSprite2D animatedSprite2D;

	private string currentDirrection = "down";
    private string currentAnimation = "";
	private bool isRunning = false;
	private bool isDashing = false;
	private bool isDashOnCooldown = false;
	
	public Godot.Timer dashTimer;
	public Godot.Timer dashCooldownTimer;

	private PlayerMovementComponent playerMovementComponent;

	private BoltCasterComponent boltCasterComponent;
    public MultiplayerSynchronizer multiplayerSyncronizer;
    // public DamageDetails damageDetails;
	private Godot.Camera2D camera;
	public bool isMoving => isRunning || isDashing;

	[Export]
	public GravitonBuffComponent gravitonBuffComponent;

    public override void _Ready()
    {
		this.camera = (Godot.Camera2D)FindChild("Camera2D");
		this.multiplayerSyncronizer = GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer");
		this.multiplayerSyncronizer.SetMultiplayerAuthority(int.Parse(Name ));
		this.boltCasterComponent = (BoltCasterComponent)FindChild("BoltCasterComponent");
		this.animatedSprite2D = (AnimatedSprite2D) FindChild("AnimatedSprite2D");
		this.playerMovementComponent = (PlayerMovementComponent)FindChild("PlayerMovementComponent");
		
		dashTimer = (Godot.Timer)FindChild("DashTimer");
		dashCooldownTimer = (Godot.Timer)FindChild("DashCooldownTimer");
		
		if(this.multiplayerSyncronizer.GetMultiplayerAuthority() == this.Multiplayer.GetUniqueId())
		{
				camera.MakeCurrent();
				PackedScene scene = GD.Load<PackedScene>("res://UI/PlayerInterfaceController/PlayerInterfaceController.tscn");
				Node sceneNode = scene.Instantiate();
				camera.AddChild(sceneNode);
				
				
			// damageDetails = (DamageDetails)FindChild("DamageDetails");
		}
		this.HandleAnimations();
	}

	public void _OnDashCooldownTimeout()
	{
		this.isDashOnCooldown = false;
	}


	public void _OnDashTimeout()
	{
		this.playerMovementComponent.speed = this.playerMovementComponent.baseSpeed;
		this.isDashing = false;
	}

    public override void _Process(double delta)
	{
	}

	public bool isMultiplayerAuthority()
	{
		return multiplayerSyncronizer.GetMultiplayerAuthority() == Multiplayer.GetUniqueId();
	}

	public override void _PhysicsProcess(double delta)
	{
		if(this.multiplayerSyncronizer.GetMultiplayerAuthority() != this.Multiplayer.GetUniqueId())
		{
			return;
		}
		
		if(Input.IsMouseButtonPressed( MouseButton.Left ) )
		{
			boltCasterComponent.Cast();
		}

		this.playerMovementComponent.Execute(this);
		MoveAndSlide();
		this.isRunning = false;

		if(!this.isDashing)
		{
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
		}
			if (Input.IsMouseButtonPressed(MouseButton.Right))
			{	
				this.HandleDash();
			}
		
			// timer.Start( 1 );
		
		this.HandleAnimations();
	}

	public void HandleDash()
	{
		if(!this.isDashOnCooldown)
		{
			this.playerMovementComponent.speed = this.playerMovementComponent.dashSpeed;
			this.isDashing = true;
			this.isDashOnCooldown = true;
			this.dashCooldownTimer.Start(2);
			this.dashTimer.Start(0.14);
		}
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

	
	public Node LoadAction(string name)
	{
		PackedScene scene = GD.Load<PackedScene>("res://Components//" + name + "// " + name + ".tscn");
		Node sceneNode = scene.Instantiate();
		AddChild(sceneNode);
		return sceneNode;
	}
}