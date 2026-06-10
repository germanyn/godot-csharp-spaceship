using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class Shooter : Node2D
{
	[Node] Timer ShootTimer = null!;
	[Node] AudioStreamPlayer2D audioStreamPlayer = null!;

	[Export] float ShootWait = 3;
	[Export] float ShootVariance = 1;
	[Export] PackedScene? projectileScene;
	[Export] AudioStream? audioStream;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		WireNodes();
		audioStreamPlayer.Stream = audioStream;
		ShootTimer.Timeout += OnShoot;
		RestartTimer();
	}

	void OnShoot()
	{
		if (projectileScene is not null)
            SignalHub.EmitSpawnPoolObject(GlobalPosition, projectileScene);
		audioStreamPlayer.Play();
		RestartTimer();
	}

	void RestartTimer()
	{
		SpaceUtils.SetStartTimer(ShootTimer, ShootWait, ShootVariance);
	}
}
