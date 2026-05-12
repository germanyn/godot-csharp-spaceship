using Godot;
using GodotUtilities;
using SpaceAce.commons;
using System;

namespace SpaceAce.entities.effects;

[Scene]
public partial class Explosion : Poolable
{
	[Node] CpuParticles2D Emitter;
	[Node] AudioStreamPlayer Sound;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
	}

    public override void _Ready()
    {
		Sound.Finished += Deactivate;
    }

    public override void Activate()
    {
		base.Activate();
		Emitter.Restart();
		Sound.Play();
    }
}
