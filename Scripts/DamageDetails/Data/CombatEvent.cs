public class CombatEvent
{
    public SpellMetadata SpellData { get; set; }
    public float TimeStamp { get; set; } // Time the damage occurred
    
    public CombatEvent(SpellMetadata spellData, float timeStamp)
    {
        SpellData = spellData;
        TimeStamp = timeStamp;
    }
}