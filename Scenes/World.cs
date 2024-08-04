using Godot;
using System;

public partial class World : Node2D
{
	
	[Export]
	private SceneNamesEnum sceneNamesEnum;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var spawnPoints = GetTree().GetNodesInGroup("PlayerSpawnPoints");
        if( spawnPoints.Count == 0 )
        {
            return;
        }
        
        int index = 0;
		foreach( PlayerInfo playerInfo in GameManager.players )
		{
            var playerScene = GD.Load<PackedScene>("res://Characters/Player/Player.tscn");
			Player currentPlayer = playerScene.Instantiate<Player>();
			currentPlayer.Name = playerInfo.id.ToString();
			AddChild(currentPlayer);

			foreach(Node2D spawnPoint in spawnPoints )
			{
				if(int.Parse(spawnPoint.Name) == index)
				{
					currentPlayer.GlobalPosition = spawnPoint.GlobalPosition;
				}
			}
			
			index++;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
