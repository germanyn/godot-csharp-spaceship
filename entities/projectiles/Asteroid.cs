using Godot;
using Godot.Collections;
using GodotUtilities;

[Scene]
public partial class Asteroid : Node2D
{
	[Export] Array<Texture2D> textures = [];
	[Node] Sprite2D sprite2D;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
		base._Notification(what);
	}

	public override void _Ready()
	{
		base._Ready();
		sprite2D.Texture = textures.PickRandom();
	}
}
