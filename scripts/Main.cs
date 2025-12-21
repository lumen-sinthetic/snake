using Godot;
using System;
using System.Threading;

public partial class Main : Node2D
{
	// [Export]
	// public Snake Snake;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var snake = GetNode<Snake>("Snake");
		snake.Head.ActionRequested += OnAppleEaten;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void OnAppleEaten()
	{
		Global.Instance.AddScore();
		var appleScene = GD.Load<PackedScene>("res://scenes/partial/apple.tscn");
		var apple = appleScene.Instantiate<Apple>();

		apple.InitRandomly();
		CallDeferred("add_child", apple);
	}

}
