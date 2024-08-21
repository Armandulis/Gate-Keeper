using System;
using System.Text.Json.Serialization;
using Godot;
using System.Text.Json;
using System.Collections.Generic;

public partial class SpellMetadata : GodotObject
{ 
    public string id = "default";
    public string spellId = "default";
    public string casterId = "default";
    
    // Can be dmage or heal
    public float value = 0;
    // Actual damage or heal after modifiers
    public float actualValue = 0;
    public bool isCrit = false;
    public bool isDot = false;
    public float dotDuration = 0;
    public float dotInterval = 1;
    public bool isAOEDot = false;

    public SpellMetadata()
    {
        if(id != "default")
        {
        Guid myuuid = Guid.NewGuid();
        id = myuuid.ToString();
        }
    }

    public static SpellMetadata ConvertFromRawString( string rawData) 
    {
        JsonElement  data = JsonSerializer.Deserialize<JsonElement>( rawData);
        SpellMetadata spellMetadata = new SpellMetadata
        {
            id = data.GetProperty("id").GetString(),
            spellId = data.GetProperty("spellId").GetString(),
            casterId = data.GetProperty("casterId").GetString(),
            value = data.GetProperty("value").GetSingle(),
            actualValue =data.GetProperty("actualValue").GetSingle(),
            isCrit = data.GetProperty("isCrit").GetBoolean(),
            dotDuration = data.GetProperty("dotDuration").GetSingle(),
            dotInterval = data.GetProperty("dotInterval").GetSingle(),
            isAOEDot = data.GetProperty("isAOEDot").GetBoolean(),
        };
        return spellMetadata;
    }
    
    public static string ConvertToRawString( SpellMetadata spellMetadata)
    {
        Dictionary<string, dynamic> data = new Dictionary<string, dynamic>
        {
            { "id", spellMetadata.id },
            { "spellId", spellMetadata.spellId },
            { "casterId", spellMetadata.casterId },
            { "value", spellMetadata.value },
            { "actualValue", spellMetadata.actualValue },
            { "isCrit", spellMetadata.isCrit },
            { "isDot", spellMetadata.isDot },
            { "dotDuration", spellMetadata.dotDuration },
            { "dotInterval", spellMetadata.dotInterval },
            { "isAOEDot", spellMetadata.isAOEDot },
        };
        

        return JsonSerializer.Serialize(data);
    }
}
