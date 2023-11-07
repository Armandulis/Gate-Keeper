using Godot;
using System;

public partial class Core : Node
{
	// SINGLETON

    public static Core instance;

    public override void _Ready()
    {
		if( instance == null )
		{
			instance = this; 
		}
    }


	// CLASS
    public bool isPlayerAttacking = false;

}
