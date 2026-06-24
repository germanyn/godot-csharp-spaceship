using System.Linq;
using Godot;
using Godot.Collections;
using GodotUtilities;
using SpaceAce.commons;

[Scene]
public partial class AsteroidExplosion : Poolable
{
	[Node] Node2D piecesGroup;
	[Node] Timer timer;
	[Node] AudioStreamPlayer audioStreamPlayer;

	Array<AsteroidPiece> pieces = [];

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
		base._Notification(what);
	}

    public override void _Ready()
    {
        base._Ready();
		pieces = [ ..piecesGroup.GetChildren().OfType<AsteroidPiece>() ];
        foreach(var piece in pieces) piece.Reset();
    }

    public override void _EnterTree()
    {
		timer.Timeout += Deactivate;
        base._EnterTree();
    }

    public override void Deactivate()
    {
		foreach (var piece in pieces)
			piece.Reset();

        base.Deactivate();
    }

    public override void Activate()
    {
        base.Activate();
		timer.Start();
		audioStreamPlayer.Play();
    }
}
