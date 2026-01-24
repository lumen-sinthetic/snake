using Godot;

namespace Scripts.Utils;

public static class VectorUtils
{
  public static bool IsOrdinal(Vector2 v) => v.X != 0 && v.Y != 0 && Mathf.Abs(v.X) == Mathf.Abs(v.Y);

  // знак → слева / справа
  // 0 → коллинеарны
  public static float Cross2D(Vector2 a, Vector2 b)
  {
    return a.X * b.Y - a.Y * b.X;
  }
}