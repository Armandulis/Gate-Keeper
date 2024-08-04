using Godot;
using System;

public partial class HealthComponent : Node2D
{
	
	[Export]
	public AnimatedSprite2D animatedSprite2D;

	private DamageFloatComponent damageFloatComponent;

	[Signal]
	public delegate void DamagedEventHandler( SpellMetadata spellMetadata );

    [Export]
	public float maxHealth;
	public float currentHealth;
	
	[Export]
	private HealthBar healthBar;
	

    public override void _Ready()
    {
		damageFloatComponent = new DamageFloatComponent();
		AddChild(damageFloatComponent);
		currentHealth = maxHealth;
		if(healthBar != null )
		{
			healthBar.initHealth(maxHealth);
		}
    }

	public float MaxHealth 
	{
		get => maxHealth;
		set 
		{
			maxHealth = value;
		}
	}

	public float CurrentHealth
	{
		get => currentHealth;
		set {
			currentHealth = value;
			if(currentHealth < 0 )
			{
				currentHealth = 0;
				GetParent().QueueFree();
			}

			if(currentHealth > MaxHealth )
			{
				currentHealth = MaxHealth;
			}
		}
	}

	public bool IsDamaged => CurrentHealth < MaxHealth;
	public double hpPercentage => CurrentHealth / MaxHealth * 100f;


	/// <summary>
	/// Subtracts amount from current health 
	/// </summary>
	public void Damage( SpellMetadata spellMetadata )
	{	
		animatedSprite2D.Play("damaged");
		damageFloatComponent.HandleDamageFloat(spellMetadata.value);
		spellMetadata.actualValue = spellMetadata.value;
		DamageMeter.instance.AddDamageSpell(spellMetadata);
		CurrentHealth -= spellMetadata.value;
		PlayerHealthBar.instance.HealthChange(maxHealth, currentHealth);

		if(healthBar != null)
		{
			
			healthBar.Health = CurrentHealth;
		}
	}	

	

	/// <summary>
	/// Adds amount to current health 
	/// </summary>
	public void Heal( SpellMetadata spellMetadata )
	{
		CurrentHealth += spellMetadata.value;
	}

	public void HandleSpell( SpellMetadata spellMetadata)
	{
		// If it is Dot, deal damage over time
		if( spellMetadata.isDot ) {
			
			Damage(spellMetadata);
		}
		else
		{

			Damage(spellMetadata);
		}
	}

}
