using System;
using System.Collections.Generic;
using Godot;

public partial class BoltCasterComponent : Node2D
{	
	[Export]
	private Timer fireRateTimer;
	[Export]
	private Timer cooldownTimer;
	[Export]
	private Timer markerTimer;

	private List<Vector2> directions = new List<Vector2>();
	private List<Line2D> markers = new List<Line2D>();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		cooldownTimer.Start();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _OnCooldownTimeout()
	{
		Vector2 direction = GetParent<Node2D>().GlobalPosition.DirectionTo(Core.instance.GetNearestPlayer(this).GlobalPosition).Normalized();
		float spreadAngle = 25 * Mathf.Pi / 180;

		Vector2 directionMiddle = direction;
		Vector2 directionUp = direction.Rotated(spreadAngle);
		Vector2 directionDown = direction.Rotated(-spreadAngle);

		directions.Add(directionMiddle);
		directions.Add(directionUp);
		directions.Add(directionDown);

		markerTimer.Start(2);
		displayMarkers();
	}
	
	private void displayMarkers()
    {
		Vector2 startPosition = GetParent<Node2D>().GlobalPosition;
		float length = 1000; // Adjust the length as needed

		foreach(var direction in directions)
		{
			Line2D marker = new Line2D();
			marker.DefaultColor = new Color(0, 0, 0); // Black color
			marker.Width = 5; // Adjust the width as needed
			marker.AddPoint(startPosition);
			marker.AddPoint(startPosition + direction * length);
			marker.TopLevel = true;

			AddChild(marker);
			markers.Add(marker);
		}
    }

	public void _OnMarkerTimerTimeout()
	{
		foreach( Line2D marker in markers )
		{
			marker.QueueFree();
		}
		markers = new List<Line2D>();
		StartStage();
	}

	public void StartStage()
	{
		CastBolt();
		cooldownTimer.Start( 7 );
	}

	private void CastBolt()
	{	
		Vector2 startPosition = GetParent<Node2D>().GlobalPosition;

		foreach (var direction in directions)
		{
			var scene = GD.Load<PackedScene>("res:///Components/BoltCasterComponent/Bolt.tscn");
			Bolt instance = (Bolt)scene.Instantiate();

			instance.Position = startPosition;
			instance.aim = direction;

			AddChild(instance);
			instance.TopLevel = true;
		}

		directions = new List<Vector2>();
	}

	public void _OnFireRateTimerTimeout()
	{
	}



}
