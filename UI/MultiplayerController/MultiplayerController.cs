using Godot;
using System;

public partial class MultiplayerController : Control
{
	[Export]
	private int port = 8910;
	
	[Export]
	private string address = "127.0.0.1";

	private ENetMultiplayerPeer peer;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Multiplayer.PeerConnected += PeerConnected;
		Multiplayer.PeerDisconnected += PeerDisconnect;
		Multiplayer.ConnectedToServer += ConnectedToServer;
		Multiplayer.ConnectionFailed += ConnectionFailed;
	}

	/// <summary>
	/// Runs when the connection fails and runs only on the client
	/// </summary>
	/// <exception cref="NotImplementedException"></exception>
    private void ConnectionFailed()
    {
		GD.Print("ConnectionFailed");
    }


	/// <summary>
	/// Runs when the connection is successfull and runs only on the client
	/// </summary>
	/// <exception cref="NotImplementedException"></exception>
    private void ConnectedToServer()
    {
		RpcId(1, nameof(SendPlayerInformation), GetNode<LineEdit>("LineEdit").Text, Multiplayer.GetUniqueId() );
		GD.Print("ConnectedToServer");
    }


	/// <summary>
	/// Runs when a player disconnects, runs on all peers
	/// </summary>
	/// <param name="id">Id of the player that connected</param>
	/// <exception cref="NotImplementedException"></exception>
    private void PeerDisconnect(long id)
    {
		GD.Print("PeerDisconnect" + id.ToString());
    }

	/// <summary>
	/// Runs when the player connects, runs on all peers
	/// </summary>
	/// <param name="id">Id of the player that connected</param>
	/// <exception cref="NotImplementedException"></exception>
    private void PeerConnected(long id)
    {
		GD.Print("PeerConnected" + id.ToString());
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.

    public override void _Process(double delta)
	{
	}

	public void _on_host_button_down()
	{
		peer = new ENetMultiplayerPeer();
		var error = peer.CreateServer( port, 5);
		if(error != Error.Ok)
		{
			GD.Print( "Canno't host: " + error.ToString() );
			return;
		}

		peer.Host.Compress( ENetConnection.CompressionMode.RangeCoder );
		Multiplayer.MultiplayerPeer = peer;
		SendPlayerInformation(GetNode<LineEdit>("LineEdit").Text, 1);
		GD.Print( "Waiting for Players!" );
	}

	public void _on_join_button_down()
	{
		peer = new ENetMultiplayerPeer();
		peer.CreateClient(address, port);
		peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
		Multiplayer.MultiplayerPeer = peer;
		GD.Print( "Joining Game!" );
	}

	public void _on_start_button_down()
	{
		Rpc("StartGame");
		// StartGame();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
	private void StartGame()
	{
		foreach(var player in GameManager.players)
		{
			GD.Print(player.name + " is playing");
		}
		var scene = ResourceLoader.Load<PackedScene>("res://Scenes/World.tscn").Instantiate();
		GetTree().Root.AddChild(scene);
		this.Hide();
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	private void SendPlayerInformation(string name, int id)
	{
		PlayerInfo playerInfo = new PlayerInfo();
		playerInfo.id = id;
		playerInfo.name = name;

		if(!GameManager.players.Contains(playerInfo))
		{
			GameManager.players.Add(playerInfo);
		}
		
		if(Multiplayer.IsServer())
		{
			foreach( var player in GameManager.players )
			{
				Rpc("SendPlayerInformation", name, id );
			}
		}
	}
}
