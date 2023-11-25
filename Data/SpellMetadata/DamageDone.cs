using System;
using System.Collections.Generic;
using Godot;

public partial class DamageDone : GodotObject
{
    // CasterId => casterDamageDone
    public IDictionary<string, CasterDamageDone> casters = new Dictionary<string, CasterDamageDone>();


    public void AddCasterSpell( SpellMetadata spellMetadata )
    {
        if(!casters.ContainsKey( spellMetadata.casterId) )
        {
            CasterDamageDone casterDamageDone = new CasterDamageDone();
            casterDamageDone.AddDamageSegment( spellMetadata );
            casters.Add(spellMetadata.casterId, casterDamageDone );
        }
        else
        {
            casters[spellMetadata.casterId].AddDamageSegment( spellMetadata );
        }
    }


}