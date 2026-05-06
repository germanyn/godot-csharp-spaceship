using System;
using Godot;

namespace SpaceAce.entities.components;

public partial class PlayerMovement : Node
{
	[Export]
    float speed = 300;

	[Export]
	float margin = 64;

	Node2D player;
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

		player = node2dParent;
		movementRect = player.GetViewportRect().Grow(-margin);
	}

    public override void _PhysicsProcess(double delta)
    {
		var input = Input.GetVector("left", "right", "up", "down").Normalized();
		var newPosition = player.Position + input * speed * (float)delta;

		player.Position = newPosition.Clamp(
			movementRect.Position,
			movementRect.End
		);
    }
}
