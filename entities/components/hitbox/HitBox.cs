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
	[Export] int maxCollisions = 1;
	[Export] int damageDealt = 15;

	int collisionsLeft = 0;

	public event Action<Area2D> Died;
	public event Action<int> Hit;

	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
	}

    public override void _EnterTree()
    {
		AreaEntered += OnAreaEntered;
        base._EnterTree();
    }

    public override void _Ready()
	{
		base._Ready();
		ApplyShape();
		Reset();
	}

	void ApplyShape()
	{
		collisionShape2D.Shape = collisionShape;
	}

	public void Reset()
	{
		collisionsLeft = maxCollisions;
	}

    private void OnAreaEntered(Area2D area)
    {
		if (area is HitBox hitBox) Hit?.Invoke(hitBox.damageDealt);

        collisionsLeft -= 1;
		if (collisionsLeft <= 0)
		{
			Died?.Invoke(area);
		}
    }
}
