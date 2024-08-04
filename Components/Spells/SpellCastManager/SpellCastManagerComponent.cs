using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class SpellCastManagerComponent : Node2D
{
	[Export]
	public AnimatedSprite2D animatedSprite2D;
	
	[Export]
	public Timer globalCooldownTimer;

    private bool isInGlobalCooldown = false;
    public bool isCasting = false;

	[Export]
	public Player player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
	}

	public bool castInterupted()
	{
		return player.isMoving;
	}

	public void spellCasted()
	{
		isCasting = true;
		this.isInGlobalCooldown = true;
		globalCooldownTimer.Start( 0.5 );
	}

	public void finishedCastingSpell()
	{
		this.animatedSprite2D.Play("idle");
		isCasting = false;
	}

	public bool tryCastSpell()
	{
		if( !isInGlobalCooldown && !isCasting )
		{
			this.animatedSprite2D.Play("cast");
			spellCasted();
			return true;
		}
		return false;
	}

	public void _OnGlobalCooldownTimeout()
	{
		this.isInGlobalCooldown = false;
	}

}
