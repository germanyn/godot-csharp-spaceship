using SpaceAce.commons;
using Godot;
using GodotUtilities;
using System;

namespace SpaceAce.tests;

[Scene]
public partial class TestPoolable : Poolable
{
  static int count = 1;

  [Node] Label Label;
  [Node] Timer Timer;

  public override void _Notification(int what)
  {
    if (what == NotificationSceneInstantiated) WireNodes();
  }

  public override void _Ready()
  {
    Label.Text = count.ToString();
    ++count;
    Timer.Timeout += OnTimerTimeout;
  }

  public override void Activate()
  {
    base.Activate();
    Timer.Start();
  }

  private void OnTimerTimeout() => Deactivate();
}