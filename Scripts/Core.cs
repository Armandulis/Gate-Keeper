using Godot;
using System;
using System.Linq;

public partial class Core : Node
{
    public static Core instance;
    public bool isPlayerAttacking = false;

    public override void _Ready()
    {
		if( instance == null )
		{
			instance = this; 
		}
    }


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
