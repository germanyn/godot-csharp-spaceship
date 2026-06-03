using Godot;
using System;

public partial class HealthBar : TextureProgressBar
{
	public event Action? Died;

	static readonly Color COLOR_DANGER = new("#cc0000");
	static readonly Color COLOR_MIDDLE = new("#ff9900");
	static readonly Color COLOR_GOOD = new("#33cc33");

	[Export] public double LevelLow = 30;
	[Export] public double LevelMed = 65;
	[Export] public double StartHealth = 100;
	[Export] public double MaxHealth = 100;

	bool dead = false;

	public override void _Ready()
	{
		MaxValue = MaxHealth;
		Value = StartHealth;
		UpdateColor();
	}

	void UpdateColor()
	{
		if (Value < LevelLow)
			TintProgress = COLOR_DANGER;
		else if (Value < LevelMed)
			TintProgress = COLOR_MIDDLE;
		else
			TintProgress = COLOR_GOOD;
	}
	public void TakeDamage(int value) => IncrValue(-value);

	public void IncrValue(int value)
	{
		Value += value;
		if (Value <= 0 && !dead) {
			dead = true;
			Died?.Invoke();
		}
		UpdateColor();
	}
}
