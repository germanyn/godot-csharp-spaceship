using Godot;
using GodotUtilities;

[Scene]
public partial class _CLASS_ : _BASE_
{
	public override void _Notification(int what)
	{
		if (what == NotificationSceneInstantiated) WireNodes();
		base._Notification(what);
	}
}