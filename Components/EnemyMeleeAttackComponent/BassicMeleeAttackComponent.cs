using Godot;
using System;

public partial class BassicMeleeAttackComponent : Area2D
{
	[Export]
	private Timer attackActionTimer;
	[Export]
	private Timer attackCooldownTimer;
	
	[Export]
	private Hitbox hitbox;

	public bool isAttacking = false;
	public bool isProccessedAttack = false;
	public bool isInRange = false;
	public bool isAttackCooldown;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// SetCollisionMaskValue(2, true);
		// SetCollisionMaskValue(4, false);
		// hitbox.SetCollisionLayerValue(16, false); 
		SpellMetadata spellMetadata = new SpellMetadata();
		spellMetadata.spellId = "BasicAttack";
		spellMetadata.casterId = "Slime";
		spellMetadata.value = 2;
		spellMetadata.isDot = false;
		hitbox.spellMetadata = spellMetadata;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(isInRange == true && isAttacking == false && isAttackCooldown == false)
		{
			isAttacking = true;
			attackActionTimer.Start(timeSec: 5);
		}
		else
		{ 
			isProccessedAttack = true;
		}

		// if(isProccessedAttack)
		// {
		// 	hitbox.SetCollisionLayerValue(16, false );
		// }
		// else
		// {
		// 	hitbox.SetCollisionLayerValue(16, true );
		// 	isProccessedAttack = true;
		// }
	}

	public void OnMeleeAttackAreaEntered(Node2D hurtbox)
	{	
		Player player = hurtbox.GetParentOrNull<Player>();
		if( player == null )
		{
			return;
		}
		
		isInRange = true;
	}

	public void OnAreaExited(Hurtbox hurtbox)
	{
		
		Player player = hurtbox.GetParentOrNull<Player>();
		if( player == null )
		{
			return;
		}
		isInRange = false;
	}

	public void OnAttackActionTimerTimeout()
	{
		hitbox.SetCollisionLayerValue(16, true); 
		
		isAttackCooldown = true;
		isAttacking = false;

		attackCooldownTimer.Start(3);
	}

	public void OnAttackCooldownTimerTimeout()
	{
		isAttackCooldown = false;
	}
}
