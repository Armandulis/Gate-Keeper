using Godot;
using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime;

public partial class BoltCasterComponent : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Cast()
	{
		var scene = GD.Load<PackedScene>("res:///Components/BoltCasterComponent/Bolt.tscn");
		Bolt instance = (Bolt)scene.Instantiate();
		AddChild(instance);
	}
}
