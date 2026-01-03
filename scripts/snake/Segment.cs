using System;
using System.Collections.Generic;
using Godot;


public partial class Segment : Node2D
{

	[Export]
	private Sprite2D SegmentSprite;

	[ExportGroup("Textures")]
	[Export] private Texture2D TailTexture;
	[Export] private Texture2D[] BodyTextures = new Texture2D[3];

	private readonly Random rng = new();

#nullable enable
	public int Index;
	public Segment? PrevSegment;
	public Segment? NextSegment;

	public Vector2I PrevTilePos;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SegmentSprite.Texture = Index != 0 ? BodyTextures[rng.Next(0, 3)] : TailTexture;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Move(Vector2 dir, float duration)
	{
		var tween = CreateTween();

		tween.TweenProperty(this, "global_position", dir, duration).SetTrans(Tween.TransitionType.Linear);

		tween.Finished += () =>
		{
			GlobalPosition = dir;
		};
	}
}
