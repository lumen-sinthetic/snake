using System;
using Godot;

public partial class Apple : Node2D
{

	// readonly Random random = new((int)DateTimeOffset.UtcNow.ToUnixTimeSeconds());
	readonly Random rng = new();

	// double NextGaussian(double mean, double stdDev)
	// {
	// 	// равномерные (0;1]
	// 	double u1 = 1.0 - rng.NextDouble();
	// 	double u2 = 1.0 - rng.NextDouble();

	// 	// стандартное нормальное распределение
	// 	double randStdNormal =
	// 			Math.Sqrt(-2.0 * Math.Log(u1)) *
	// 			Math.Sin(2.0 * Math.PI * u2);

	// 	// смещение и масштабирование
	// 	return mean + stdDev * randStdNormal;
	// }

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
		Position = new Vector2(rng.Next(10, 1000), rng.Next(10, 500));
	}
}
