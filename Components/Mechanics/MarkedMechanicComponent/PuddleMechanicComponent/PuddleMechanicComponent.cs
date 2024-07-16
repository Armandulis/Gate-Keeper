using Godot;
using System;

public partial class PuddleMechanicComponent : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		SpellMetadata spellMetadata = new SpellMetadata();

		Hitbox hitbox = (Hitbox)FindChild("Hitbox");
		Player player = GetParent<Node2D>().GetParentOrNull<Player>();
		
		
		spellMetadata.spellId = "Puddle";
		spellMetadata.spellId = "Puddle";
		hitbox.CollisionLayer = 16;
			
		spellMetadata.casterId = "Soulbinder";
		spellMetadata.value = 10;
		spellMetadata.isCrit = IsCrit();
		spellMetadata.isDot = true;
		spellMetadata.isAOEDot = true;
		spellMetadata.dotInterval = 1;
		spellMetadata.dotDuration = 999999;
			
		hitbox.spellMetadata = spellMetadata;
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

	public void _OnAreaEntered(Area2D area2D)
	{
		if( area2D.GetParentOrNull<PuddleMechanicComponent>() != null )
		{

			var scene = GD.Load<PackedScene>("res://Components/Mechanics/GlobalDamageComponent/GlobalDamageComponent.tscn");
			GlobalDamageComponent globalDamageComponent = (GlobalDamageComponent)scene.Instantiate();
			
			globalDamageComponent.Position = GlobalPosition;
		
			CallDeferred("add_child", globalDamageComponent);
			globalDamageComponent.TopLevel = true;
			
		}
	}
}
