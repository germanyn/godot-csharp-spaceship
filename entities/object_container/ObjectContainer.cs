using SpaceAce.commons;
using Godot;
using Godot.Collections;

namespace SpaceAce.entities.object_container;

public partial class ObjectContainer : Node
{
	[Export] PackedScene TestScene;

	Dictionary<PackedScene, ScenePool> Pools = [];

    public override void _Ready()
    {
		SignalHub.Instance.SpawnPoolObject += OnSpawnPoolObject;
        base._Ready();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
		if (@event.IsActionPressed("test"))
		{
			OnSpawnPoolObject(
				new Vector2(
					GD.RandRange(400, 1000),
					GD.RandRange(300, 700)
				),
				TestScene
			);
		}
    }

	void OnSpawnPoolObject(Vector2 position, PackedScene scene)
	{
		CallDeferred(nameof(SpawnDeferred), position, scene);
	}

	void SpawnDeferred(Vector2 position, PackedScene scene)
	{
		if (!Pools.ContainsKey(scene))
		{
			var newPool = new ScenePool(scene, this, 5);
			Pools.Add(scene, newPool);
		}
		if (Pools.TryGetValue(scene, out var value)) value.ActivateNext(position);
	}
}
