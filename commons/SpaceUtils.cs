using Godot;
using System;

public partial class SpaceUtils
{
	public static void SetStartTimer(Timer timer, float target, float variance)
	{
		timer.Start(target + GD.RandRange(-variance, variance));
	}
}
