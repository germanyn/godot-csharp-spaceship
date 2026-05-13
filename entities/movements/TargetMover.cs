using System.Diagnostics;
using Godot;
using SpaceAce.entities.player;

namespace SpaceAce.entities.movements;

public partial class TargetMover : Mover
{
	[Export] StringName TargetGroup = nameof(Player);

	protected Node2D target;

    public override void _Ready()
    {
		var node = GetTree().GetFirstNodeInGroup(TargetGroup);
		Debug.Assert(target is null, "Target not found");
		if (node is not Node2D node2d) {
			GD.Print("Target is not a Node2D");
			return;
		}
		target = node2d;
        base._Ready();
    }

    internal override void SetupVelocity()
    {
		direction = parent.GlobalPosition.DirectionTo(target.GlobalPosition);
		base.SetupVelocity();
    }
}
