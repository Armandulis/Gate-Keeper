using Godot;
using System;

public partial class SoulbinderXalthar : CharacterBody2D
{
	private BoltCasterComponent boltCasterComponent;

    public override void _Ready()
    {
		boltCasterComponent = (BoltCasterComponent)FindChild("BoltCasterComponent");
	
    }
    public override void _PhysicsProcess(double delta)
	{
		boltCasterComponent.Cast();
	}
	
}
