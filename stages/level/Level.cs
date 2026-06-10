using Godot;
using GodotUtilities;
using System;

[Scene]
public partial class Level : Node
{
	[Node] GameUi gameUi;
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
		base._Notification(what);
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
}
