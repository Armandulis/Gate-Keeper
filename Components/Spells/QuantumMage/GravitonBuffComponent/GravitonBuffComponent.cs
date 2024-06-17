using Godot;
using System;

public partial class GravitonBuffComponent : Node2D
{
	private float stacks = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void addStacks( float amount )
	{
		stacks += amount;
	}


	private void subtractStacks( float amount )
	{
		stacks -= amount;
	}

	public bool trySpendOn( int amount )
	{
		if( amount <= stacks ) 
		{
			subtractStacks( amount );
			return true;
		}
		return false;
	}
}
