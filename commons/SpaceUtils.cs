using Godot;
using System;

public partial class SpaceUtils
{
	public static void SetStartTimer(Timer timer, float target, float variance)
	{
		timer.Start(target + GD.RandRange(-variance, variance));
	}

	public static Vector2 GetPointOutsideRect(Rect2 rect, float margin)
	{
		var r = rect.Grow(margin);
		var side = GD.RandRange(1, 4);
		var randX = GD.RandRange(r.Position.X, r.End.X);
		var randY = GD.RandRange(r.Position.Y, r.End.Y);
		return side switch
		{
			1 => new Vector2((float) randX, r.Position.Y),
			2 => new Vector2((float) randX, r.End.Y),
			3 => new Vector2(r.Position.X, (float) randY),
			_ => new Vector2(r.End.Y, (float) randY),
		};
	}

	public static Vector2 RandomPointInRect(Rect2 rect, float margin)
	{
		var r = rect.Grow(margin);
		return new Vector2(
			(float) GD.RandRange(r.Position.X, r.End.X),
			(float) GD.RandRange(r.Position.Y, r.End. Y)
		);
	}
}
