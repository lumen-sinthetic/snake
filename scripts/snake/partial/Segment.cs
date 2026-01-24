using System;
using Godot;
using Scripts.Utils;


public partial class Segment : Node2D
{
	[Export]
	private Sprite2D SegmentSprite = null!;

	[ExportGroup("Textures")]
	[Export] private Texture2D TailTexture = null!;
	[Export] private Texture2D BodyTexture = null!;
	[Export] private Texture2D TurnedBodyTexture = null!;

	public Segment? NextSegment;
	public Vector2 EndPos { get; private set; }

	public void ToTail() => SegmentSprite.Texture = TailTexture;
	public void ToBody() => SegmentSprite.Texture = BodyTexture;

	private float CalculateAngle(Node2D target)
	{
		Vector2 direction = target.GlobalPosition - GlobalPosition;

		float angle = direction.Angle();

		return angle;
	}

	public void RotateTo(Node2D target) => Rotation = CalculateAngle(target);



	async public void Move(Vector2 dir, float duration, Node2D next)
	{
		EndPos = dir;

		// var tween = CreateTween();
		// tween.SetTrans(Tween.TransitionType.Linear);
		// tween.TweenProperty(this, "global_position", dir, duration);
		// tween.TweenProperty(this, "rotation", CalculateAngle(next), 0);

		// await ToSignal(GetTree().CreateTimer(duration), "timeout");

		var timer = GetTree().CreateTimer(duration);
		timer.Timeout += () =>
		{
			GlobalPosition = dir;
			// Rotation = CalculateAngle(next);
		};
	}
}
