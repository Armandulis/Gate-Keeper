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

	private PlayerMovementComponent playerMovementComponent;

	private BoltCasterComponent boltCasterComponent;
    private MultiplayerSynchronizer multiplayerSyncronizer;
	private Godot.Camera2D camera;


    public override void _Ready()
    {
		this.camera = (Godot.Camera2D)FindChild("Camera2D");
		this.multiplayerSyncronizer = GetNode<MultiplayerSynchronizer>("MultiplayerSynchronizer");
		this.multiplayerSyncronizer.SetMultiplayerAuthority(int.Parse(Name ));
		this.boltCasterComponent = (BoltCasterComponent)FindChild("BoltCasterComponent");
		this.animatedSprite2D = (AnimatedSprite2D) FindChild("AnimatedSprite2D");
		this.playerMovementComponent = (PlayerMovementComponent)FindChild("PlayerMovementComponent");
		

		if(this.multiplayerSyncronizer.GetMultiplayerAuthority() == this.Multiplayer.GetUniqueId())
		{
			camera.MakeCurrent();
		}
		this.HandleAnimations();
	}

    public override void _Process(double delta)
	{
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

	
	public Node LoadAction(string name)
	{
		PackedScene scene = GD.Load<PackedScene>("res://Components//" + name + "// " + name + ".tscn");
		Node sceneNode = scene.Instantiate();
		AddChild(sceneNode);
		return sceneNode;
	}
}