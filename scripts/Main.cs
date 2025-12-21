using Godot;
using System;

public partial class Main : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var child = GetChild(0);
		GD.Print("here");

		if (child is Snake snake)
		{
			GD.Print("here 2");
			snake.ActionRequested += OnAppleEaten;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void OnAppleEaten()
	{
		var appleScene = GD.Load<PackedScene>("res://scenes/partial/apple.tscn");
		var apple = appleScene.Instantiate<Apple>();
		apple.InitRandomly();
		AddChild(apple);
	}
}
