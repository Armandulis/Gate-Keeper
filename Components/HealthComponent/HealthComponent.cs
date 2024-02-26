using Godot;
using System;

public partial class HealthComponent : Node2D
{

	private DamageFloatComponent damageFloatComponent;

	[Signal]
	public delegate void DamagedEventHandler( SpellMetadata spellMetadata );

    public override void _Ready()
    {
		damageFloatComponent = new DamageFloatComponent();
		AddChild(damageFloatComponent);
		currentHealth = maxHealth;
    }



    [Export]
	public float maxHealth;
	public float currentHealth ;
	

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
			}

			if(currentHealth > MaxHealth )
			{
				currentHealth = MaxHealth;
			}
		}
	}

	public bool IsDamaged => CurrentHealth < MaxHealth;


	/// <summary>
	/// Subtracts amount from current health 
	/// </summary>
	public void Damage( SpellMetadata spellMetadata )
	{	
		damageFloatComponent.HandleDamageFloat(spellMetadata.value);
		spellMetadata.actualValue = spellMetadata.value;
		DamageMeter.instance.AddDamageSpell(spellMetadata);
		CurrentHealth -= spellMetadata.value;
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

		}
	}

}
