using System;
using System.Collections;
using System.Collections.Generic;
using Godot;

public partial class SpellMetadataCollection : GodotObject
{
    /// <summary>
    /// SpellId => spells array
    /// </summary>
    public IDictionary<string, List<SpellMetadata>> spellsCollection = new Dictionary<string, List<SpellMetadata>>();

    internal void AddSpell(SpellMetadata spellMetadata)
    {
        if(!spellsCollection.ContainsKey( spellMetadata.spellId ) )
        {
            List<SpellMetadata> spells = new List<SpellMetadata>(); 
            spells.Add(spellMetadata);
            spellsCollection.Add(spellMetadata.spellId, spells );
        }
        else
        {
            spellsCollection[spellMetadata.spellId].Add( spellMetadata );
        }
    }
}