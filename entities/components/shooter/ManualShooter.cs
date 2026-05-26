using Godot;
using System;

public partial class ManualShooter : Node2D
{
	[Export] PackedScene projectileScene;
	const string INPUT_SHOOT = "shoot";

    public override void _UnhandledInput(InputEvent @event)
	{
		if (@event.IsActionPressed(INPUT_SHOOT))
		{
			SignalHub.Instance?.EmitSpawnPoolObject(GlobalPosition, projectileScene);
		}
	}
}
