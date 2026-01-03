using System;
using System.Linq;
using System.Numerics;
using Godot;

public partial class Apples : TileMapLayer
{
	[Export]
	TileMapLayer Terrain = null!;

	private readonly Random rng = new();


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddApple();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void AddApple()
	{
		var cells = Terrain.GetUsedCells();

		var minCell = cells.Min();
		var maxCell = cells.Max();

		var xPos = rng.Next(minCell.X, maxCell.X + 1);
		var yPos = rng.Next(minCell.Y, maxCell.Y + 1);

		SetCell(new(xPos, yPos), 2, new(0, 0));
	}


	public void RemoveApple(Vector2I pos)
	{
		SetCell(pos, -1);
	}
}
