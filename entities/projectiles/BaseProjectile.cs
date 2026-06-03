using System;
using System.Linq;
using Godot;
using GodotUtilities;
using SpaceAce.commons;
using SpaceAce.entities.movements;

namespace SpaceAce.commons.projectiles;

[Scene]
public partial class BaseProjectile : Poolable
{
	[Export] PackedScene ExplosionScene;
	[Export] float ExplosionMargin = 40;
	[Node] Sprite2D Sprite2D;
	[Node] VisibleOnScreenNotifier2D ScreenNotifier2D;
	[Node] Timer LifeTimer;
	[Node] HitBox HitBox;

	Mover Mover;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
	}

    public override void _EnterTree()
    {
		ScreenNotifier2D.ScreenExited += OnScreenExited;
		LifeTimer.Timeout += Deactivate;
		HitBox.Died += OnHitBoxDied;
        base._EnterTree();
    }

    public override void _Ready()
    {
        base._Ready();
		
		Mover = GetChildren().OfType<Mover>().FirstOrDefault()
			?? throw new ArgumentNullException("Node has no Mover child");
    }

    public override void Activate()
    {
        base.Activate();
		Mover.SetupVelocity();
		LifeTimer.Start();
		HitBox.Reset();
    }

	public void Explode(Vector2 colliderPosition)
	{
		if (ExplosionScene is null) return;

		var direction = GlobalPosition.DirectionTo(colliderPosition);
		var explosionPosition = GlobalPosition + direction * ExplosionMargin;

		SignalHub.Instance?.EmitSpawnPoolObject(
			explosionPosition,
			ExplosionScene
		);
	}

	void OnScreenExited()
	{
		if (Visible) Deactivate();
	}

    private void OnHitBoxDied(Area2D area)
    {
		Explode(area.GlobalPosition);
		Deactivate();
    }
}
