using Godot;
using System;

public partial class NewScript : Resource
{
    [Export] public PackedScene enemyScene;
    [Export] public int enemyCount;
    [Export] public float spawnInterval;
    [Export] public float waveInterval;
}
