using Godot;

namespace SpaceAce.entities.movements;

public partial class Mover : Node
{
	[Export] protected float speed = 0;
	[Export] protected float rotationSpeedDegrees = 0;
	[Export] protected Vector2 direction = Vector2.Zero;

	protected Node2D parent;
	protected Vector2 velocity;

	// Called when the node enters the scene tree for the first time.
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
		SetupVelocity();
	}
	
	virtual internal void SetupVelocity()
	{
		velocity = speed * direction.Normalized();
	}

    public override void _PhysicsProcess(double delta)
    {
		parent.Translate(velocity * (float) delta);
		parent.RotationDegrees += rotationSpeedDegrees * (float) delta;
    }
}
