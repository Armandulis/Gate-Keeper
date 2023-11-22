using Godot;
using System;
using System.Collections.Generic;

public partial class GameManager : Node
{
    public static List<PlayerInfo> players = new List<PlayerInfo>();

    public static GameManager instance;

    public override void _Ready()
    {
      	if( instance == null )
		{
			instance = this; 
		}
        else
        {
            QueueFree();
        }
    }
}
