using System;
using Godot;


public partial class Segment : Node2D
{
	[Export]
	private Sprite2D SegmentSprite;

	[ExportGroup("Textures")]
	[Export] private Texture2D TailTexture;
	[Export] private Texture2D[] BodyTextures = new Texture2D[3];

#nullable enable
	private readonly Random rng = new();


	// Called when the node enters the scene tree for the first time.
	// public override void _Ready()
	// {
	// 	SegmentSprite.Texture = Index != 0 ? BodyTextures[rng.Next(0, 3)] : TailTexture;
	// }

	public void ToTail() => SegmentSprite.Texture = TailTexture;
	public void ToBody() => SegmentSprite.Texture = BodyTextures[rng.Next(0, 3)];

	public void Move(Vector2 dir, float duration)
	{
		var tween = CreateTween();
		tween.TweenProperty(this, "global_position", dir, duration).SetTrans(Tween.TransitionType.Linear);
	}
}
