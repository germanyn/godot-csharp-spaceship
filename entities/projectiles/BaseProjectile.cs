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
	[Node] Sprite2D Sprite2D;
	[Node] VisibleOnScreenNotifier2D ScreenNotifier2D;
	[Node] Timer LifeTimer;

	Mover Mover;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
	}

    public override void Activate()
    {
        base.Activate();
		Mover.SetupVelocity();
		LifeTimer.Start();
    }

    public override void _Ready()
    {
		ScreenNotifier2D.ScreenExited += OnScreenExited;
		LifeTimer.Timeout += Deactivate;
		
		Mover = GetChildren().OfType<Mover>().FirstOrDefault()
			?? throw new ArgumentNullException("Node has no Mover child");

        base._Ready();
    }

	void OnScreenExited()
	{
		if (Visible) Deactivate();
	}

    public override void _ExitTree()
    {
        base._ExitTree();
		ScreenNotifier2D.ScreenExited -= OnScreenExited;
    }
}
