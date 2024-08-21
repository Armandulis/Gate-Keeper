using System.Collections.Generic;

public class Segment
{
    public float StartTime { get; private set; }
    public float EndTime { get; private set; }
    public List<DamageDealer> DamageDealers { get; private set; } = new List<DamageDealer>();

    public Segment(float startTime)
    {
        StartTime = startTime;
    }

    public void EndSegment(float endTime)
    {
        EndTime = endTime;
    }

    public void AddDamageDealer(DamageDealer dealer)
    {
        DamageDealers.Add(dealer);
    }
}