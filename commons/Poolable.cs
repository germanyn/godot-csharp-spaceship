using Godot;

namespace SpaceAce.commons;

public partial class Poolable : Node2D
{
	public bool IsAvailable() => !Visible;
    public override void _EnterTree() => Deactivate();

	virtual public void Activate()
	{
		SetDeferred(PropertyName.ProcessMode, (int) ProcessModeEnum.Inherit);
		Show();
	}

	public void Deactivate()
	{
		SetDeferred(PropertyName.ProcessMode, (int) ProcessModeEnum.Disabled);
		Hide();
	}
}
