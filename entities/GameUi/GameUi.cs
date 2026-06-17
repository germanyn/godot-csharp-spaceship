using System;
using Godot;
using GodotUtilities;

[Scene]
public partial class GameUi : Control
{
	[Node] HealthBar healthBar;
	[Node] AudioStreamPlayer music;
	[Node] AudioStreamPlayer boostSound;
	[Node] Label scoreLabel;

	int score = 0;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
		base._Notification(what);
	}

	public override void _EnterTree()
	{
		SignalHub.Instance?.PointsScored += OnPointsScored;
		SignalHub.Instance?.PlayerTakeDamage += OnPlayerTakeDamage;
		SignalHub.Instance?.PlayerHealthBoost += OnPlayerHealthBoost;
		healthBar.Died += OnHealthBarDied;
		base._EnterTree();
	}

    private void OnPointsScored(int points)
    {
		score += points;
		scoreLabel.Text = score.ToString().PadLeft(4, '0');
    }

    private void OnHealthBarDied()
    {
		music.Stop();
		GetTree().Paused = true;
    }

    public override void _ExitTree()
    {
		SignalHub.Instance?.PlayerTakeDamage -= OnPlayerTakeDamage;
		SignalHub.Instance?.PlayerHealthBoost -= OnPlayerHealthBoost;
		healthBar.Died -= OnHealthBarDied;
		base._EnterTree();
    }

    private void OnPlayerTakeDamage(int damage) =>
		healthBar.TakeDamage(damage);

    private void OnPlayerHealthBoost(int boost) {
		GD.Print("sanidade 2");
		healthBar.IncrValue(boost);
		boostSound.Play();
	}
}
