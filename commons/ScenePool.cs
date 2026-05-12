using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Godot;

namespace SpaceAce.commons;

public partial class ScenePool : RefCounted
{
    const bool DEBUG_POOL = true;
    private readonly HashSet<Poolable> Items = [];
    private readonly PackedScene PackedScene;
    private readonly Node Container;
    int NumberActive {get => Items.Aggregate(0, (acc, item) => acc + (item.IsAvailable() ? 1: 0)); }

    // Standard constructor with a body
    public ScenePool(PackedScene packedScene, Node container, int initialCount = 0)
    {
        PackedScene = packedScene;
        Container = container;

        ValidateScene();

        LogMessage($"Creating a pool with {initialCount} items");

        for (var i = 0; i < initialCount; ++i)
        {
            AddNewItem();
        }
    }

    public Poolable AddNewItem()
    {
        var newItem = PackedScene.Instantiate<Poolable>();

        Container.AddChild(newItem);
        Items.Add(newItem);

        LogMessage($"AddNewItem() -> New Instance: {newItem.Name} :: {Items.Count} total");

        return newItem;
    }

    public void ActivateNext(Vector2 newPosition)
    {
        LogMessage($"ActivateNext() Available: {NumberActive}/{Items.Count} newPosition{newPosition}");
        var item = Items
            .FirstOrDefault(item => item.IsAvailable()) ?? AddNewItem();

        item.GlobalPosition = newPosition;
        item.Activate();
    }

    public void ValidateScene()
    {
        var test = PackedScene.Instantiate();
        var isValid = test is Poolable;
        test.Free();
        Debug.Assert(isValid, "PackedScene root must extend Poolable: ", PackedScene.ResourcePath);
    }

    void LogMessage(string message)
    {
        if (DEBUG_POOL) GD.Print($"[ScenePool:{PackedScene.ResourcePath}] {message}");
    }
}