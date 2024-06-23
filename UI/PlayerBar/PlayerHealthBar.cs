using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class PlayerHealthBar : HBoxContainer
{
	
    public static PlayerHealthBar instance;

	 public override void _Ready()
    {
        // get_tree().get_root().find_node("node_name")
        //GetNode<Label>("Label");
        
      	if( instance == null )
		{
			instance = this;
 
		}
        else
        {
            QueueFree();
        }
    }

	[Export]
	private TextureProgressBar textureProgressBar; 

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void HealthChange( float maxHealth, float currentHealth)
	{
		textureProgressBar.MaxValue = maxHealth;
		textureProgressBar.Value = currentHealth;
	}
}
