using Godot;
using Godot.Collections;
using System;
using System.Diagnostics;

public partial class SpellButton : TextureButton
{
	[Export]
	private TextureProgressBar cooldownProgressBar;
	
	[Export]
	private Label labelKey;
	
	[Export]
	private Label labelTime;

	[Export]
	private Timer timer;

	string keyText = "";
	
	private SpellMetadata SpellMetadata = null;

	public string KeyText 	
	{
		get => keyText;
		set 
		{
			keyText = value;
			labelKey.Text = value;	

			Shortcut shortcut = new Shortcut();
			InputEventKey eventKey = new InputEventKey();
			
			if( Enum.TryParse(keyText, out Key keyEnum))
			{
				eventKey.Keycode =  Key.Key1;
				eventKey.Pressed = true;

				Godot.Collections.Array inputEvents = new Godot.Collections.Array { eventKey };
                shortcut.Events = inputEvents;

				Shortcut = shortcut;
				
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		KeyText = "1";
		cooldownProgressBar.MaxValue = timer.WaitTime;
		
		SetProcess( false );
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		labelTime.Text = timer.TimeLeft.ToString();
		cooldownProgressBar.Value = timer.TimeLeft;
	}

	public void _OnPressedSpellButton()
	{
		if( SpellMetadata != null )
		{
			// cast the spell
		}
		timer.Start();
		Disabled = true;
		SetProcess(true);

	}
	
	public void _OnTimerTimeout()
	{
		Disabled = false;
		labelTime.Text = "";
		cooldownProgressBar.Value = 0;
		SetProcess(false);
	}
}
