using System;
using Godot;
using GodotUtilities;

[Scene]
public partial class EnemyBase : PathFollow2D
{
	[Export] float speed = 100;
	[Export] PackedScene explosionScene;
	[Export] float powerupChance = 25;
	[Export] PackedScene powerupScene;
	[Export] float homingMissileChance = 25;
	[Export] PackedScene homingMissileScene;
	[Export] int points = 0;

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

	void createRandomChance(PackedScene? scene, float chance)
	{
		if (scene is null || GD.Randf() >= chance) return;
		
		SignalHub.EmitSpawnPoolObject(GlobalPosition, scene);
	}

    private void OnDied()
    {
		SignalHub.EmitPointsScored(points);
		SignalHub.EmitSpawnPoolObject(GlobalPosition, explosionScene);
		createRandomChance(powerupScene, powerupChance);
		createRandomChance(homingMissileScene, homingMissileChance);

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
