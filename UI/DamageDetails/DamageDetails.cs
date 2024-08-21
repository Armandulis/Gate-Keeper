using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DamageDetails : Control
{
    private List<ProgressBar> progressBars = new List<ProgressBar>();
    
    [Export]
    private VBoxContainer container; // Or any other container that suits your needs


	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        CombatData combatData = DamageMeter.instance.GetCombatData();
        List<DamageDealer> damageDealers = combatData.GetOverallDamage().OrderByDescending(d => d.TotalDamage).ToList();
        UpdateItems(damageDealers);
	}

       public void UpdateItems(List<DamageDealer> damageDealers)
{
    // Calculate total damage done by all players
    float totalDamage = damageDealers.Sum(d => d.TotalDamage);

    // Ensure the progress bars list is the same size as the damage dealers list
    while (progressBars.Count < damageDealers.Count)
    {
        ProgressBar progressBar = new ProgressBar();
        progressBar.MinValue = 0;
        progressBar.MaxValue = 100;

        Label nameLabel = new Label();
        // nameLabel.VerticalAlignment = Label.Cert;
        progressBar.AddChild(nameLabel);

        Label damageLabel = new Label();
        // damageLabel.Align = Label.AlignEnum.Right;
        progressBar.AddChild(damageLabel);

        container.AddChild(progressBar);
        progressBars.Add(progressBar);
    }

    // Update existing progress bars with damage data
    for (int i = 0; i < damageDealers.Count; i++)
    {
        var dealer = damageDealers[i];
        var progressBar = progressBars[i];

        // Calculate the percentage of total damage done by this dealer
        float damagePercentage = totalDamage > 0 ? (dealer.TotalDamage / totalDamage) * 100 : 0;

        // Update progress bar value
        progressBar.Value = damagePercentage;

        // Update the name and damage labels
        Label nameLabel = (Label)progressBar.GetChild(0);
        nameLabel.Text = dealer.Name;

        Label damageLabel = (Label)progressBar.GetChild(1);
        damageLabel.Text = $"{dealer.TotalDamage} ({damagePercentage:F1}%)";
    }

    // If there are extra progress bars from a previous update, hide them
    for (int i = damageDealers.Count; i < progressBars.Count; i++)
    {
        progressBars[i].Visible = false;
    }
}
}
