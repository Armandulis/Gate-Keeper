using System;
using Godot;

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
}
