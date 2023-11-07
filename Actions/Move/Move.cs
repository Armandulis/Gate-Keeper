using Godot;
using System;
using System.Reflection.Metadata.Ecma335;


public partial class Move : Node2D
{
	private float speed = 200;

    public void Execute(object body)
    {
		if( body is CharacterBody2D characterBody2D)
		{

			Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			characterBody2D.Velocity = inputDirection * speed;
			characterBody2D.MoveAndSlide();
		}
    }

}
