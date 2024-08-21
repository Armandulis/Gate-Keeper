using System.Collections.Generic;
using System.Linq;

public class CombatData
{
    private List<Segment> segments = new List<Segment>();
    private Segment currentSegment;
    private float lastDamageTime;
    private const float segmentTimeout = 20f;

    public CombatData()
    {
        StartNewSegment();
    }

    private void StartNewSegment()
    {
        float currentTime = GetGameTime();
        currentSegment = new Segment(currentTime);
        segments.Add(currentSegment);
    }

    private void EndCurrentSegment()
    {
        float currentTime = GetGameTime();
        currentSegment.EndSegment(currentTime);
    }

    public void AddDamage(SpellMetadata spellMetadata)
    {
        float currentTime = GetGameTime();

        if (currentTime - lastDamageTime > segmentTimeout)
        {
            EndCurrentSegment();
            StartNewSegment();
        }

        lastDamageTime = currentTime;

        DamageDealer dealer = currentSegment.DamageDealers.FirstOrDefault(d => d.Name == spellMetadata.casterId);
        if (dealer == null)
        {
            dealer = new DamageDealer { Name = spellMetadata.casterId };
            currentSegment.AddDamageDealer(dealer);
        }

        var combatEvent = new CombatEvent(spellMetadata, currentTime);
        dealer.AddCombatEvent(combatEvent);
    }

    public List<DamageDealer> GetOverallDamage()
    {
        var overallDamage = new Dictionary<string, DamageDealer>();

        foreach (var segment in segments)
        {
            foreach (var dealer in segment.DamageDealers)
            {
                if (!overallDamage.ContainsKey(dealer.Name))
                {
                    overallDamage[dealer.Name] = new DamageDealer { Name = dealer.Name };
                }

                overallDamage[dealer.Name].TotalDamage += dealer.TotalDamage;
                foreach (var spell in dealer.Spells)
                {
                    if (!overallDamage[dealer.Name].Spells.ContainsKey(spell.Key))
                    {
                        overallDamage[dealer.Name].Spells[spell.Key] = new List<CombatEvent>();
                    }

                    overallDamage[dealer.Name].Spells[spell.Key].AddRange(spell.Value);
                }
            }
        }

        return overallDamage.Values.ToList();
    }
    private float GetGameTime()
    {
        return 1000f; // OS.GetTicksMsec() / Example function to get current game time in seconds
    }
}
