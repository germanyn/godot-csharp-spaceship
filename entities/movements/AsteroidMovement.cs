using Godot;
using SpaceAce.entities.movements;
using System;

public partial class AsteroidMovement : Mover
{
	const float MARGIN_OUT = 80;
	const float MARGIN_IN = 300;

	float baseRotationSpeed = 0;

	public override void _Ready()
	{
		baseRotationSpeed = rotationSpeedDegrees;
		base._Ready();
	}

	override internal void SetupVelocity()
	{
		var startPos = SpaceUtils.GetPointOutsideRect(
			parent.GetViewportRect(),
			MARGIN_OUT
		);
		direction = startPos.DirectionTo(
			SpaceUtils.RandomPointInRect(
				parent.GetViewportRect(),
				-MARGIN_IN
			)
		);

		velocity = speed * direction;

		parent.GlobalPosition = startPos;
		parent.RotationDegrees = (float) GD.RandRange(0.0, 360.0);

		rotationSpeedDegrees = (float)(baseRotationSpeed * GD.RandRange(0.8, 1.2));
		rotationSpeedDegrees *= Math.Sign(GD.RandRange(-1, 1));
	}
}
