using Godot;
using System;

public partial class Bolt : CharacterBody2D
{
	private Vector2 aim;
	Timer timer;
    public override void _Ready()
    {	
		timer = (Timer)FindChild("Timer");
		Hitbox hitbox = (Hitbox)FindChild("Hitbox");
		Player player = GetParent<Node2D>().GetParentOrNull<Player>();
		if( player != null)
		{
			aim = GlobalPosition.DirectionTo( GetGlobalMousePosition());
			hitbox.CollisionLayer = 8;
			hitbox.casterId = player.playerId;
			hitbox.damage = 100;
		}
		else
		{
			aim = GlobalPosition.DirectionTo(Core.instance.GetNearestPlayer(this).GlobalPosition);
			hitbox.CollisionLayer = 16;
		}
		Position = GetParent<Node2D>().GetParent<Node2D>().GlobalPosition;
		TopLevel = true;
		LookAt(aim);

		timer.Start(4);
    }

    public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(aim.Normalized() * (float)delta * 1000);
		
	}

	public void _OnImpactDetectorBodyEntered(Node2D body)
	{
		QueueFree();
	}

	public void _OnTimerTimeout()
	{
		
		QueueFree();
	}
}
