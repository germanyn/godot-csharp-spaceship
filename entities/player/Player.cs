using Godot;
using System;

namespace SpaceAce.entities.player;

public partial class Player : Node2D
{
	public override void _Ready()
	{
		GD.Print(nameof(Player));
		AddToGroup(nameof(Player));
	}
}
