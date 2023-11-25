using System;
using System.Collections.Generic;
using Godot;
public partial class DamageSegment : GodotObject
{
    public float totalDamage = 0;

    /// <summary>
    /// spellId => spellMetadataCollection 
    /// </summary>
    public IDictionary<string, SpellMetadataCollection> castedSpells = new Dictionary<string, SpellMetadataCollection>();

    internal void SpellMetadataCollection(SpellMetadata spellMetadata)
    {
        totalDamage += spellMetadata.actualValue;
        
        if(!castedSpells.ContainsKey( spellMetadata.spellId ) )
        {
            SpellMetadataCollection spellMetadataCollection = new SpellMetadataCollection();
            spellMetadataCollection.AddSpell( spellMetadata );
            castedSpells.Add(spellMetadata.spellId, spellMetadataCollection );
        }
        else
        {
            castedSpells[spellMetadata.spellId].AddSpell( spellMetadata );
        }
    }
}