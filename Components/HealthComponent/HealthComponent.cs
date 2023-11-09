using Godot;
using System;

public partial class HealthComponent : Node2D
{
	
	[Export]
	public float maxHealth = 100;
	public float currentHealth = 100;
	

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
	public void Damage( float amount )
	{
		CurrentHealth -= amount;
	}

	

	/// <summary>
	/// Adds amount to current health 
	/// </summary>
	public void Heal( float amount )
	{
		CurrentHealth += amount;
	}

}
