using System.Diagnostics;
using Godot;
using SpaceAce.entities.player;

namespace SpaceAce.entities.movements;

public partial class TargetMover : Mover
{
	[Export] StringName TargetGroup = nameof(Player);

    internal override void SetupVelocity()
    {
		var target = GetTree().GetFirstNodeInGroup(TargetGroup);
		Debug.Assert(target is null, "Target not found");
		if (target is not Node2D target2d) {
			GD.Print("Target is not a Node2D");
			return;
		}
		direction = parent.GlobalPosition.DirectionTo(target2d.GlobalPosition);
		base.SetupVelocity();
    }
}
