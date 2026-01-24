using System;
using Godot;

public partial class Apples : TileMapLayer
{
	[Export]
	TileMapLayer Terrain = null!;
	[Export]
	Snake SnakeInstance = null!;


	private readonly Random rng = new();

	private const int _startingApplesCount = 3;
	private const int _maxCreationAttempts = 5;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < _startingApplesCount; i++) AddApple();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public void AddApple()
	{
		var cells = Terrain.GetUsedCells();

		int attemptsCount = 0;

		do
		{
			var minCell = cells.Min();
			var maxCell = cells.Max();

			var xPos = rng.Next(minCell.X, maxCell.X + 1);
			var yPos = rng.Next(minCell.Y, maxCell.Y + 1);
			var readyPos = new Vector2I(xPos, yPos);


			if (!SnakeInstance.CheckPresence(readyPos))
			{
				int randomResId = rng.Next(6, 9);
				SetCell(readyPos, randomResId, new(0, 0));
				break;
			}
			attemptsCount++;
		} while (attemptsCount < _maxCreationAttempts);
	}


	public void RemoveApple(Vector2I pos)
	{
		SetCell(pos, -1);
	}
}
