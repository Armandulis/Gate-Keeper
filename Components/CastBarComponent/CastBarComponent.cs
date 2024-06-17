using Godot;

public partial class CastBarComponent : Node2D
{
	[Export]
	public ProgressBar progressBar;
	
	[Export]
	public Sprite2D interuptIcon;

	public Timer castTimer;

	private int interuptsNeeded;

	private bool isCasting = false;

	[Signal]
	public delegate void CastFinishedEventHandler();

	public override void _Ready()
	{
		this.Hide();
		castTimer = (Timer)FindChild("CastTime");
	}

	public override void _Process(double delta)
	{
		if( isCasting == true )
		{
			progressBar.Value = (2 - castTimer.TimeLeft) / 2 * 100;
		}
		else
		{
			progressBar.Value = 0;
		}
	}
	
	public void startCast()
	{
		this.Show();
		isCasting = true;
		castTimer.Start( 2 );
	}

	public void castInerupted()
	{
		this.Hide();
		isCasting = false;
		castTimer.Stop();
		progressBar.Value = 0;
	}

	public void _OnCastTimeTimeout()
	{
		this.Hide();
		EmitSignal(SignalName.CastFinished);
		progressBar.Value = 100;
		castTimer.Stop();
	}
}
