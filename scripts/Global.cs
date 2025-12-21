using Godot;
using System;

public partial class Global : Node
{


  public static Global Instance { get; private set; }


  public int Score { get; private set; } = 0;


  public void AddScore(int points = 1)
  {
    Score += points;
  }

  public override void _Ready()
  {
    Instance = this;
  }
}
