using System;
using System.Text.Json.Serialization;
using Godot;
using System.Text.Json;
using System.Collections.Generic;

public partial class SpellMetadata : GodotObject
{
    public string spellId = "default";
    public string casterId = "default";
    
    // Can be dmage or heal
    public float value = 0;
    // Actual damage or heal after modifiers
    public float actualValue = 0;
    public bool isCrit = false;
    public bool isDot = false;

    public static SpellMetadata ConvertFromRawString( string rawData)
    {
        dynamic data = JsonSerializer.Deserialize<SpellMetadata>( rawData);
        SpellMetadata spellMetadata = new SpellMetadata
        {
            spellId = data.spellId,
            casterId = data.casterId,
            value = data.value,
            actualValue = data.actualValue,
            isCrit = data.isCrit,
            isDot = data.isDot
        };
        return spellMetadata;
    }
    
    public static string ConvertToRawString( SpellMetadata spellMetadata)
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>
        {
            { "spellId", spellMetadata.spellId },
            { "casterId", spellMetadata.casterId },
            { "value", spellMetadata.value },
            { "actualValue", spellMetadata.actualValue },
            { "isCrit", spellMetadata.isCrit },
            { "isDot", spellMetadata.isDot }
        };

        return JsonSerializer.Serialize(data);
    }
}
