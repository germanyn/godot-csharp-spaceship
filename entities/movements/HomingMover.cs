using SpaceAce.entities.movements;
using Godot;
using System;

namespace SpaceAce.entities.movements;

public partial class HomingMover : TargetMover
{
    public override void _PhysicsProcess(double delta)
    {
		var angleToTarget = parent.Transform.X.AngleTo(
			parent.GlobalPosition.DirectionTo(target.GlobalPosition)
		);

		var step = Mathf.Min(
			Mathf.Abs(angleToTarget),
			Mathf.DegToRad(rotationSpeedDegrees) * (float) delta
		);

		parent.Rotate(step * Mathf.Sign(angleToTarget));
		parent.Position += parent.Transform.X * speed * (float) delta;
    }
}
