using System;
using Godot;


public partial class Segment : Node2D
{
	[Export]
	private Sprite2D SegmentSprite = null!;

	[ExportGroup("Textures")]
	[Export] private Texture2D TailTexture = null!;
	[Export] private Texture2D[] BodyTextures = new Texture2D[3];

	private readonly Random rng = new();

	public Segment? NextSegment;
	public Vector2 EndPos { get; private set; }

	public void ToTail() => SegmentSprite.Texture = TailTexture;
	public void ToBody() => SegmentSprite.Texture = BodyTextures[rng.Next(0, 3)];

	public void RotateTo(Node2D target)
	{
		// 1. Получаем вектор на цель
		Vector2 direction = target.GlobalPosition - GlobalPosition;

		// 2. Вычисляем угол (в радианах)
		float angle = direction.Angle();

		// 3. Поворачиваем ноду
		Rotation = angle;
	}

	public void Move(Vector2 dir, float duration)
	{
		EndPos = dir;
		var tween = CreateTween();
		tween.TweenProperty(this, "global_position", dir, duration).SetTrans(Tween.TransitionType.Linear);
	}
}
