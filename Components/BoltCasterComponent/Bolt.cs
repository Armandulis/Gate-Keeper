using Godot;
using System;

public partial class Bolt : CharacterBody2D
{
	public Vector2 aim;
	public float damage = 100;
	Timer timer;
	Area2D impactDetector;

	public string casterId;
    public override void _Ready()
    {	
		
		SpellMetadata spellMetadata = new SpellMetadata();

		timer = (Timer)FindChild("Timer");
		Hitbox hitbox = (Hitbox)FindChild("Hitbox");
		Player player = GetParent<Node2D>().GetParentOrNull<Player>();
		impactDetector = (Area2D)FindChild("ImpactDetector");
		
		spellMetadata.spellId = "Bolt";
		if( player != null)
		{
			hitbox.CollisionLayer = 8;
			hitbox.spellMetadata = spellMetadata;
			
			spellMetadata.casterId = player.multiplayerSyncronizer.GetMultiplayerAuthority().ToString();
			spellMetadata.value = damage;
			spellMetadata.isCrit = IsCrit();
			spellMetadata.isDot = false;
			impactDetector.SetCollisionMaskValue(2, false);
			impactDetector.SetCollisionMaskValue(4, true);
		}
		else
		{
			spellMetadata.value = damage;
			
			impactDetector.SetCollisionMaskValue(2, true);
			impactDetector.SetCollisionMaskValue(4, false);
			hitbox.CollisionLayer = 16;
		}
		hitbox.spellMetadata = spellMetadata;

		// Position = GetParent<Node2D>().GetParent<Node2D>().GlobalPosition;
		// TopLevel = true;
		LookAt(aim);

		timer.Start(4);
    }

    public override void _PhysicsProcess(double delta)
	{
		var collision = MoveAndCollide(motion: aim.Normalized() * (float)delta * 1300);
		
	}

	public void _OnImpactDetectorBodyEntered(Node2D body)
	{
		QueueFree();
	}

	public void _OnTimerTimeout()
	{
		
		QueueFree();
	}

	public bool IsCrit()
	{	
		Random random = new Random();
		int chance  = random.Next(1, 100);
		if( chance > 50)
		{
			return true;
		}

		return false;
	}
}
