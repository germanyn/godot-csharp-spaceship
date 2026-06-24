using System;
using Godot;
using GodotUtilities;

[Scene]
[Tool]
public partial class HitBox : Area2D
{
	[Export] Shape2D collisionShape {
		get;
		set
		{
			field = value;
			if (IsNodeReady())
			{
				ApplyShape();
			}
		}
	}
	[Node] CollisionShape2D collisionShape2D;
	[Node] Timer invincibilityTimer;
	[Export] int maxCollisions = 1;
	[Export] int damageDealt = 15;
	[Export] double invincibleTime =  0.1;

	int collisionsLeft = 0;
	bool invincible = false;

	public event Action<Area2D> Died;
	public event Action<int> Hit;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
	}

    public override void _EnterTree()
    {
		AreaEntered += OnAreaEntered;
		invincibilityTimer.Timeout += OnInvincibilityTimeout;
        base._EnterTree();
    }

    private void OnInvincibilityTimeout() => invincible = false;

    public override void _Ready()
	{
		base._Ready();
		ApplyShape();
		Reset();
	}

	void ApplyShape() =>
		collisionShape2D?.Shape = collisionShape;

	public void Reset()
	{
		collisionsLeft = maxCollisions;
		invincible = !Mathf.IsZeroApprox(invincibleTime);
		if (invincible) invincibilityTimer.Start(invincibleTime);
	}

    private void OnAreaEntered(Area2D area)
    {
		if (invincible) return;

		if (area is HitBox hitBox) Hit?.Invoke(hitBox.damageDealt);

        collisionsLeft -= 1;
		if (collisionsLeft <= 0)
		{
			Died?.Invoke(area);
		}
    }
}
