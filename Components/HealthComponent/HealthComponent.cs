using Godot;
using System;

public partial class HealthComponent : Node2D
{
	
	[Signal]
	public delegate void DamagedEventHandler( SpellMetadata spellMetadata );

    public override void _Ready()
    {

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
		EmitSignal(SignalName.Damaged, spellMetadata);
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

}
