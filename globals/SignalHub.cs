using System;
using Godot;

public partial class SignalHub : Node
{
	public static SignalHub Instance { get; private set; }
	[Signal] public delegate void SpawnPoolObjectEventHandler(Vector2 position, PackedScene scene);

    public event Action<int>? PlayerTakeDamage;
    public event Action<int>? PlayerHealthBoost;

    public override void _EnterTree()
    {
		Instance = this;
        base._EnterTree();
    }

    public static void EmitSpawnPoolObject(Vector2 position, PackedScene scene)
    {
        Instance.EmitSignal(SignalName.SpawnPoolObject, position, scene);
    }

    public static void EmitPlayerTakeDamage(int damage) =>
        Instance.PlayerTakeDamage?.Invoke(damage);

    public static void EmitPlayerHealthBoost(int boost) =>
        Instance.PlayerHealthBoost?.Invoke(boost);
}
