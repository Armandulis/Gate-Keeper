 using Godot;
using System;

public partial class PuddleCasterComponent : Node2D
{
	public bool casted = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Cast();
	}

	public void Cast()
	{
		if( casted == false )
		{
			
			Rpc("CastPuddle");
			casted = true;
		}
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	private void CastPuddle()
	{
		
			var scene = GD.Load<PackedScene>("res:///Components/PuddleCasterComponent/Puddle.tscn");
			Puddle instance = (Puddle)scene.Instantiate();
			
			Vector2 offset  = new Vector2(-800,-200);
			instance.Position = GetParent<Node2D>().GlobalPosition + offset;
			
			AddChild(instance);
			instance.TopLevel = true;
			// timer.Start( 1 );
			// isOnCooldown = true;

			var scene2 = GD.Load<PackedScene>("res:///Components/PuddleCasterComponent/Puddle.tscn");
			Puddle instance2 = (Puddle)scene2.Instantiate();
			
			Vector2 offset2  = new Vector2(-800,200);
			instance2.Position = GetParent<Node2D>().GlobalPosition + offset2;
			
			AddChild(instance2);
			instance2.TopLevel = true;
			// timer.Start( 1 );
			// isOnCooldown = true;
	}
}
