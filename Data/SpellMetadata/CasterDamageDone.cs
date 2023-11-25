using System;
using System.Collections.Generic;
using Godot;

public partial class CasterDamageDone : GodotObject
{
    /// <summary>
    /// SegmendID => DamageSegment
    /// </summary>
    public IDictionary<string, DamageSegment> damageSegments = new Dictionary<string, DamageSegment>(); 


    public void AddDamageSegment( SpellMetadata spellMetadata )
    {
        if(!damageSegments.ContainsKey( "Overall" ) )
        {
            DamageSegment damageSegment = new DamageSegment();
            damageSegment.SpellMetadataCollection( spellMetadata );
            damageSegments.Add("Overall", damageSegment );
        }
        else
        {
            damageSegments["Overall"].SpellMetadataCollection( spellMetadata );
        }
    }
}