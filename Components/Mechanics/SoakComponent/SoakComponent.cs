using Godot;
using System;

public partial class SoakComponent : Node2D
{
		[Export]
	public CastBarComponent castBarComponent;

	private bool isCasting = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if( Input.IsActionJustPressed( "Spell 1" ) )
		{
			
			castFinished();	
		}
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	public void startCast()
	{
			isCasting = true;
			castBarComponent.startCast( 3 );
	}

	
	public void castFinished()
	{
		isCasting = false;
		Rpc(method: "CastSingularity", GetGlobalMousePosition());
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]
	private void CastSingularity(Vector2 aim)
	{
			var scene = GD.Load<PackedScene>("res://Components/Mechanics/SoakComponent/SoakPuddleComponent/SoakPuddleComponent.tscn");
			SoakPuddleComponent instance = (SoakPuddleComponent)scene.Instantiate();
			
			instance.Position = aim;
		
			AddChild(instance);
			instance.TopLevel = true;
	}
}
