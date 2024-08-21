
using System.Collections.Generic;

public class DamageDealer
{
    public string Name { get; set; }
    public int TotalDamage { get; set; }
    public Dictionary<string, List<CombatEvent>> Spells { get; set; } = new Dictionary<string, List<CombatEvent>>();

    public void AddCombatEvent(CombatEvent combatEvent)
    {
        TotalDamage += (int)combatEvent.SpellData.actualValue;
        if (!Spells.ContainsKey(combatEvent.SpellData.spellId))
        {
            Spells[combatEvent.SpellData.spellId] = new List<CombatEvent>();
        }
        Spells[combatEvent.SpellData.spellId].Add(combatEvent);
    }
}
