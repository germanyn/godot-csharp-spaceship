using Godot;

public partial class SignalHub : Node
{
	public static SignalHub Instance { get; private set; }
	[Signal] public delegate void SpawnPoolObjectEventHandler(Vector2 position, PackedScene scene);

    public override void _EnterTree()
    {
		Instance = this;
        base._EnterTree();
    }

    public void EmitSpawnPoolObject(Vector2 position, PackedScene scene)
    {
        Instance.EmitSignal(SignalName.SpawnPoolObject, position, scene);
    }
}
