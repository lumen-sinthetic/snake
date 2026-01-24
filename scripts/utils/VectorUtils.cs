using Godot;

namespace Scripts.Utils;

public static class VectorUtils
{
  public static bool IsOrdinal(Vector2 v) => v.X != 0 && v.Y != 0 && Mathf.Abs(v.X) == Mathf.Abs(v.Y);


  // public override () { }
}