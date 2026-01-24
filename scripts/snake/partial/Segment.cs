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

	public Segment? NextSegment = null;
	public Segment? PrevSegment = null;
	public Vector2 EndPos { get; private set; }

	public void ToTail() => SegmentSprite.Texture = TailTexture;
	public void ToBody() => SegmentSprite.Texture = BodyTexture;
	public void ToTurnedBody() => SegmentSprite.Texture = TurnedBodyTexture;


	// public float GetAngle()=> 

	private float CalculateAngle(Node2D target)
	{
		Vector2 direction = target.GlobalPosition - GlobalPosition;

		float angle = direction.Angle();

		return angle;
	}


	// private void TurnSegment(Vector2 nextDir)
	// {
	// 	if (VectorUtils.Cross2D(GlobalPosition, nextDir) != 0)
	// 	{
	// 		ToBody();
	// 	}
	// 	else
	// 	{
	// 		ToTurnedBody();
	// 	}

	// }


	async public void Move(Vector2 dir, float duration, Node2D next)
	{
		EndPos = dir;

		// var tween = CreateTween();
		// tween.SetTrans(Tween.TransitionType.Linear);
		// tween.TweenProperty(this, "global_position", dir, duration);
		// tween.TweenProperty(this, "rotation", CalculateAngle(next), 0);

		var angle = CalculateAngle(next);
		Rotation = angle;

		await ToSignal(GetTree().CreateTimer(duration), "timeout");


		GlobalPosition = dir;
		// TurnSegment(next.GlobalPosition);

		// if (PrevSegment is null) return;


		// if (PrevSegment.Rotation != Rotation)
		// {
		// 	PrevSegment.ToTurnedBody();
		// }

		// var timer = GetTree().CreateTimer(duration);
		// timer.Timeout += () =>
		// {
		// };
	}
}
