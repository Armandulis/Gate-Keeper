using Godot;
using System;
using System.Xml.Schema;

public partial class player : CharacterBody2D
{
	public override void _Process(double delta)
	{
		if(Input.IsKeyPressed( Key.W))
		{
			this.Position += new Vector2(0,-5);
		}
		
		if(Input.IsKeyPressed( Key.S))
		{
			this.Position += new Vector2(0,5);
		}

		
		if(Input.IsKeyPressed( Key.A))
		{
			this.Position += new Vector2(-5,0);
		}

		
		if(Input.IsKeyPressed( Key.D))
		{
			this.Position += new Vector2(5,0);
		}
	}
}
