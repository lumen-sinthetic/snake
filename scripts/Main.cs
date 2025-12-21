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
		var instance = GetNode("Snake");
		var snake = instance.GetNode<Snake>("CharacterBody2D");
		snake.ActionRequested += OnAppleEaten;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	void OnAppleEaten()
	{
		// GD.Print("Here");
		// var appleScene = GD.Load<PackedScene>("res://scenes/partial/apple.tscn");
		// var scene = appleScene.Instantiate();


		// apple.InitRandomly();
		// AddChild(apple);
	}

}
