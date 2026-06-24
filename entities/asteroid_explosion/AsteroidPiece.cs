using System;
using Godot;
using Godot.Collections;
using GodotUtilities;
using SpaceAce.commons;

[Scene]
public partial class AsteroidPiece : Node2D
{
	[Export] float speed = 150;
	[Export] float rotationSpeedDegrees = 360;
	[Export] Array<Texture2D> textures = [];

	[Node] Sprite2D sprite2D;

	Vector2 velocity = Vector2.Right;
	float startRotationSpeedDegrees = 0;
	Vector2 startPosition = Vector2.Zero;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
		base._Notification(what);
	}

	public override void _Ready()
	{
		base._Ready();
		sprite2D.Texture = textures.PickRandom();
		startPosition = Position;
	}

    public override void _Process(double delta)
    {
        base._Process(delta);
		Translate(velocity * (float) delta);
		Rotate(float.DegreesToRadians(startRotationSpeedDegrees * (float)delta));
    }

	public void Reset()
	{
		Position = startPosition;
		startRotationSpeedDegrees = (float) (rotationSpeedDegrees * GD.RandRange(0.6, 1.2)) * Math.Sign(GD.RandRange(-1, 1));
		velocity = Position.Normalized() * speed * (float) GD.RandRange(0.6, 1.2);
		sprite2D.Texture = textures.PickRandom();
	}
}
