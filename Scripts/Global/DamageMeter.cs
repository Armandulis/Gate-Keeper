using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Linq;
using Godot.Collections;

public partial class DamageMeter : Node
{
    public static DamageMeter instance;
    public CombatData combatData = new CombatData();

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
    }

    public CombatData GetCombatData()
{
    return combatData;
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
        combatData.AddDamage(SpellMetadata.ConvertFromRawString(spellMetadataRaw));
    }
}
