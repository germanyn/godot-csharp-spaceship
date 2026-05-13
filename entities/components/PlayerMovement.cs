using System;
using Godot;

namespace SpaceAce.entities.components;

public partial class PlayerMovement : Node
{
	[Export]
    float speed = 300;

	[Export]
	float margin = 64;

	Node2D parent;
	Rect2 movementRect;

	public override void _Ready()
	{
		var parent = GetParent();
		if (parent is not Node2D node2dParent)
		{
			GD.Print("Parent is not a Node2D");
			QueueFree();
			return;
		}

		this.parent = node2dParent;
        movementRect = this.parent.GetViewportRect().Grow(-margin);
		base._Ready();
	}

    public override void _PhysicsProcess(double delta)
    {
		var input = Input.GetVector("left", "right", "up", "down").Normalized();
		var newPosition = parent.Position + input * speed * (float)delta;

		parent.Position = newPosition.Clamp(
			movementRect.Position,
			movementRect.End
		);
    }
}
