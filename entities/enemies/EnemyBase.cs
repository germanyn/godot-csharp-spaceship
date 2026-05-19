using Godot;

public partial class EnemyBase : PathFollow2D
{
	[Export] float speed = 100;

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		Progress += speed * (float) delta;
		if (ProgressRatio > 0.99)
		{
			QueueFree();
		}
    }
}
