using Godot;
using GodotUtilities;
using SpaceAce.commons;
using System;

[Scene]
public partial class PowerUp : Poolable
{
	[Node] Timer LifeTimer;
	[Node] Area2D area2D;

	[Export] int boost = 25;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
		base._Notification(what);
	}

	public override void _EnterTree()
	{
		LifeTimer.Timeout += OnTimeout;
		area2D.AreaEntered += OnAreaEntered;
        base._EnterTree();
	}

    public override void _ExitTree()
    {
		LifeTimer.Timeout -= OnTimeout;
		area2D.AreaEntered -= OnAreaEntered;
        base._ExitTree();
    }

    public override void Activate()
    {
        base.Activate();
		LifeTimer.Start();
    }

    private void OnTimeout() => Deactivate();

    private void OnAreaEntered(Area2D area)
	{
		SignalHub.EmitPlayerHealthBoost(boost);
		Deactivate();
	}
}
