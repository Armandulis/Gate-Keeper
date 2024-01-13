using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Linq;

public partial class DamageMeter : Node
{
    private float cooldown = 2;
	private double timer = 0;
    private double dpsTimer = 0;

    public float dps = 0;
    public float totalDamage = 0;

    public static DamageMeter instance;
    public DamageDone damageDone = new DamageDone();
    private Label label;

    public string dmgText0 = "";
    public string dmgText1 = "";
    
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

    // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        // $dmageDone<string, array(segment)> => dmage done array 
        // $segments<Total|Current|Boss, damageInfo> => segments done array
        // $$currentSegment{float: totalDamage, spells<string, spell values> }
        // $spells{float: value, bool: isCrit}
        dpsTimer += delta;
		timer += delta;
        int index = 0;
		if(timer > cooldown )
		{
            
			timer = 0;
            foreach(var caster in damageDone.casters )
            {
                string casterId = caster.Key;
                string text = "Player: " + casterId + " | "; 
                CasterDamageDone casterDamageDone = caster.Value;

                DamageSegment damageSegment = casterDamageDone.damageSegments["Overall"];
                text = text + "Total damage done: " + damageSegment.totalDamage + " | ";
                text = text + "DPS: " + (damageSegment.totalDamage/dpsTimer) + " | ";
                if( index == 0 )
                {
                    dmgText0 = text;
                }
                if(index == 1)
                {
                    dmgText1 = text;
                }
                index++;
            }
            // foreach (SpellMetadata spellMetadata in spellMetadataList)
            // {
            //     totalDamage += spellMetadata.actualValue;
            // }
			// GD.Print( "DPS: " + (totalDamage/dpsTimer));
			// GD.Print( "Total Damage: " + totalDamage);
		}
	}

    public void AddDamageSpell( SpellMetadata spellMetadata )
    {
        if( !IsMultiplayerAuthority() )
        {
            return;
        }
        Rpc(nameof(HandleCasterSpell),SpellMetadata.ConvertToRawString(spellMetadata));
    }


    [Rpc(MultiplayerApi.RpcMode.Authority,CallLocal = true)]
    void HandleCasterSpell(
        string spellMetadataRaw
    )
    {
        damageDone.AddCasterSpell(SpellMetadata.ConvertFromRawString(spellMetadataRaw));
     }

    
	// [Rpc(MultiplayerApi.RpcMode.AnyPeer,CallLocal = true)]

}
