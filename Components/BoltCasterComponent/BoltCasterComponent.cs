using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;

public partial class BoltCasterComponent : Node2D
{
	private bool isOnCooldown = false;
	private Timer timer;
	private int boltStage = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		timer = (Timer)FindChild("CooldownTimer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Cast()
	{
		if( !isOnCooldown )
		{
			
			if( GetParentOrNull<Player>() == null )
			{
				Rpc("CastBolt", Core.instance.GetNearestPlayer(this).GlobalPosition, 1);
			}
			else
			{
				Rpc(method: "CastBolt", GetGlobalMousePosition(), 1);
			}
			
			
		}
	}

	public void _OnCooldownTimeout()
	{
			isOnCooldown = false;
	}

	
	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	private void CastBolt(Vector2 aim, int cooldown)
	{
			// if(this.multiplayerSyncronizer.GetMultiplayerAuthority() != this.Multiplayer.GetUniqueId())
			// {
			// 	return;
			// }
			var scene = GD.Load<PackedScene>("res:///Components/BoltCasterComponent/Bolt.tscn");
			Bolt instance = (Bolt)scene.Instantiate();
			
			instance.Position = GetParent<Node2D>().GlobalPosition;
			instance.aim = GlobalPosition.DirectionTo( aim);
		
			
			AddChild(instance);
			instance.TopLevel = true;
			timer.Start( cooldown);
			isOnCooldown = true;
	}
}
