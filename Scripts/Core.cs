using Godot;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

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

	public Player GetNearestPlayer(Node2D nearestToBody)
	{
		Godot.Collections.Array<Node> players = GetTree().GetNodesInGroup("Players");
		
		if( players.Count == 0 )
		{
			return null;
		}
		Player nearestPlayer = (Player)players.First();
		foreach( Player player in players )
		{
			float distance = player.GlobalPosition.DistanceTo(nearestToBody.GlobalPosition );
			float nearestDistance = nearestPlayer.GlobalPosition.DistanceTo(nearestToBody.GlobalPosition);
			nearestPlayer = nearestDistance < distance ? nearestPlayer : player;
		}

		return nearestPlayer;
	}

	public int GetTotalPlayers()
	{
		return GetTree().GetNodesInGroup("Players").Count;
	}

}
