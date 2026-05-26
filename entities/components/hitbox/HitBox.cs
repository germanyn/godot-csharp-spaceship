using Godot;
using GodotUtilities;

[Scene]
[Tool]
public partial class HitBox : Area2D
{
	[Export] Shape2D collisionShape {
		get;
		set
		{
			field = value;
			if (IsNodeReady())
			{
				ApplyShape();
			}
		}
	}
	[Node] CollisionShape2D collisionShape2D;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
	}

	public override void _Ready()
	{
		WireNodes();
		base._Ready();
		ApplyShape();
	}

	void ApplyShape()
	{
		collisionShape2D.Shape = collisionShape;
	}
}
