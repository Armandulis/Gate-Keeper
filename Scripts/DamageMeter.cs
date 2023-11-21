using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

public partial class DamageMeter : Node
{
    private float cooldown = 2;
	private double timer = 0;
    private double dpsTimer = 0;

    public float dps = 0;
    public float totalDamage = 0;
    public static DamageMeter instance;
    public List<SpellMetadata> spellMetadataList = new List<SpellMetadata>();
    
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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        dpsTimer += delta;
		timer += delta;
		if(timer > cooldown )
		{
			timer = 0;

            // foreach (SpellMetadata spellMetadata in spellMetadataList)
            // {
            //     totalDamage += spellMetadata.actualValue;
            // }
			GD.Print( "DPS: " + (totalDamage/dpsTimer));
			GD.Print( "Total Damage: " + totalDamage);
		}
	}

    public void AddDamageSpell( SpellMetadata spellMetadata )
    {
        spellMetadataList.Add(spellMetadata);
        totalDamage += spellMetadata.actualValue;
    }

}
