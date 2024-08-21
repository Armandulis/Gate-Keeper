using Godot;
using System;
using System.Linq;

public partial class SpellBar : HBoxContainer
{
	private SpellBar[] slots;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (var child in GetChildren() )
		{
			if( child.GetType() == typeof(SpellButton) )
			{
				slots.Append( child );
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
