using System;
using Godot;

public partial class Apple : Node2D
{

	readonly Random random = new();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public AppleArea Area
	{
		get
		{
			var area = GetNode<AppleArea>("Hitbox");
			return area;
		}
	}


	public void InitRandomly()
	{

		Position = new Vector2(random.Next(10, 1000), random.Next(10, 500));
	}
}
