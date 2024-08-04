using Godot;
using System;

public partial class MovingOrbMechanicComponent : Node2D
{
    [Export]
    public float Speed = 50f; 
	private Vector2 target;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Get all nodes in the "PuddleMechanicUnit" group
        var bosses = GetTree().GetNodesInGroup("MainBoss");
        if (bosses.Count == 0)
        {
            return;
        }
		
		Random random = new Random();
        
		// Select a random node from the list
        int randomIndex = random.Next(bosses.Count);
		SoulbinderXalthar soulbinderXalthar = (SoulbinderXalthar)bosses[randomIndex];
		target = soulbinderXalthar.GlobalPosition;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
 		// Move towards the target position
        Vector2 direction = (target - Position).Normalized();
        Position += direction * Speed * (float)delta;

        // Optional: Stop moving if the node is close enough to the target position
        if (Position.DistanceTo(target) < 1f)
        {
            Position = target;
        }
	}

	public void _OnMechanicAreaEntered( Area2D area2D )
	{
		if( area2D.GetParentOrNull<Player>() != null )
		{
			// give player buff?
			QueueFree();
			return;
		}

		if( area2D.GetParentOrNull<SoulbinderXalthar>() != null )
		{
			var scene = GD.Load<PackedScene>("res://Components/Mechanics/GlobalDamageComponent/GlobalDamageComponent.tscn");
			GlobalDamageComponent globalDamageComponent = (GlobalDamageComponent)scene.Instantiate();
			
			globalDamageComponent.Position = GlobalPosition;
		
			GetParent().CallDeferred("add_child", globalDamageComponent);
			globalDamageComponent.TopLevel = true;
			QueueFree();
			return;
		}
	}
}
