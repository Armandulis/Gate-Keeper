using Godot;
using System;

public partial class Hitbox : Area2D
{
	[Export]
	public float damage = 10;
	public string casterId = "default"; 

	
}
