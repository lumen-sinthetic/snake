using Godot;
using System;

public partial class Apple : Area2D
{
	// Called when the node enters the scene tree for the first time.
	readonly Random rand = new();

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void InitRandomly()
	{
		Position = new Vector2(rand.Next(10, 100), rand.Next(10, 100));
	}



	void OnBodyEntered(Node body)
	{
		if (body is Snake snake)
		{
			snake.Eat(this);
			QueueFree();
		}
	}
}
