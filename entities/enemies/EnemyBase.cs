using System;
using Godot;
using GodotUtilities;

[Scene]
public partial class EnemyBase : PathFollow2D
{
	[Export] float speed = 100;
	[Export] PackedScene explosion;

	[Node] HitBox hitBox;
	[Node] HealthBar healthBar;
	[Node] Sprite2D sprite2D;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
	}

    public override void _EnterTree()
    {
		hitBox.Hit += OnHit;
		healthBar.Died += OnDied;
        base._EnterTree();
    }

    public override void _ExitTree()
    {
		hitBox.Hit -= OnHit;
		healthBar.Died -= OnDied;
        base._ExitTree();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
		Progress += speed * (float) delta;
		if (ProgressRatio > 0.99)
		{
			QueueFree();
		}
    }

    private void OnHit(int damage) => healthBar.TakeDamage(damage);

    private void OnDied()
    {
		SignalHub.EmitSpawnPoolObject(GlobalPosition, explosion);
		var tween = CreateTween();
		tween.TweenProperty(
			sprite2D,
			(string)Sprite2D.PropertyName.Modulate,
			Colors.Red,
			0.25
		);
		tween.TweenCallback(Callable.From(QueueFree));
    }

}
