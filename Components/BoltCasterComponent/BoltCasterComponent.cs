using Godot;

public partial class BoltCasterComponent : Node2D
{
	private bool isOnCooldown = false;
	
	[Export]
	private Timer fireRateTimer;

	
	[Export]
	private Timer cooldownTimer;
	private int boltStage = 1;
	private int stageBoltCount = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cooldownTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Cast()
	{
		// if( !isOnCooldown )
		// {	
			// if( GetParentOrNull<Player>() == null )
			// {
			Rpc("CastBolt", Core.instance.GetNearestPlayer(this).GlobalPosition );
			// }
			// else
			// {
			// 	Rpc(method: "CastBolt", GetGlobalMousePosition(), 1);
			// }
			
			
		// }
	}
	public void StartStage()
	{
		// stage 0
		if( boltStage == 1)
		{
			if( stageBoltCount < 3 )
			{
				Cast();
				stageBoltCount++;
				return;
			}
		}

		stageBoltCount = 0;

		cooldownTimer.Start( 7 );
	}

	public void _OnCooldownTimeout()
	{
		StartStage();
	}

	public void _OnFireRateTimerTimeout()
	{
		
		StartStage();

	}

	
	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	private void CastBolt(Vector2 aim )
	{

			// if(this.multiplayerSyncronizer.GetMultiplayerAuthority() != this.Multiplayer.GetUniqueId())
			// {
			// 	return;
			// }
			var scene = GD.Load<PackedScene>("res:///Components/BoltCasterComponent/Bolt.tscn");
			Bolt instance = (Bolt)scene.Instantiate();
			
			
		
		
		Vector2 direction = GlobalPosition.DirectionTo(aim);

		Vector2 finalDirection = direction;

        // Calculate the perpendicular vector for the spread
        Vector2 perpendicular = new Vector2(-direction.Y, direction.X).Normalized();

        // Define the spread angle (in radians)
        float spreadAngle = 5 * Mathf.Pi / 180; // 10 degrees converted to radians

        // Calculate the directions for the other bolts

		if( stageBoltCount == 1)
		{

         finalDirection = direction.Rotated(spreadAngle);
		}
		if( stageBoltCount == 2)
		{

         finalDirection = direction.Rotated(-spreadAngle);
		}
		
        


			
			instance.Position = GetParent<Node2D>().GlobalPosition;
			instance.aim = finalDirection;
		
			
			AddChild(instance);
			instance.TopLevel = true;
			fireRateTimer.Start(0.1);
			isOnCooldown = true;
	}
}
