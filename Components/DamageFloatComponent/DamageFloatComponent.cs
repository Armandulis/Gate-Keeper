using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

public partial class DamageFloatComponent : Node2D
{
	List<Godot.Label> currentFloaters = new List<Godot.Label>(); 

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach( Godot.Label floater in currentFloaters.ToList()){
			Vector2 newPosition = floater.Position;
			newPosition.Y -= 2; 
			floater.Position = newPosition;

			if(floater.Position.Y < Position.Y - 50 )
			{
				floater.QueueFree();
				currentFloaters.Remove(floater);
			}
		}
	}

	public void HandleDamageFloat( float amount )
	{
		Godot.Label label = new Godot.Label();

		label.Text = amount.ToString();
		AddChild(label);
		currentFloaters.Add(label);
		
		Random rnd = new Random();
		int side  = rnd.Next(0, 2);
		if(side == 1)
		{
			Vector2 newPosition = label.Position;
			newPosition.X -= 50;
			label.Position = newPosition;
		}
	
	}
}
