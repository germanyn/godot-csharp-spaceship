using Godot;
using GodotUtilities;
using System;

namespace SpaceAce.entities.player;

[Scene]
public partial class Player : Node2D
{
	[Node] HitBox hitBox;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
		base._Notification(what);
	}

    public override void _EnterTree()
    {
		AddToGroup(nameof(Player));
		hitBox.Hit += OnHitBoxHit;
        base._EnterTree();
	}

    private void OnHitBoxHit(int damage) =>
		SignalHub.EmitPlayerTakeDamage(damage);
}
