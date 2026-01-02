using System.Collections.Generic;
using Godot;

public partial class Snake : Node2D
{
	[Export]
	private SnakeHead SnakeHeadNode;

	[Export]
	private PackedScene SegmentScene;

	[Export]
	private Apples ApplesField;

	[Export]
	private TileMapLayer Terrain;

	private readonly List<Segment> Segments = [];



	public override void _Ready()
	{
		SnakeHeadNode.EatApple += OnAppleEaten;
		SnakeHeadNode.HeadMoveEnd += MoveSegments;
		AddSegment();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		SnakeHeadNode.Move(input);
	}


	// TODO: Make segments position calculation base on tile coords


	private void OnAppleEaten(Vector2I pos)
	{
		ApplesField.RemoveApple(pos);
		ApplesField.AddApple();
		Global.Instance.AddScore();
		AddSegment();
	}


	private void MoveSegments(Vector2 headPos)
	{
		for (int i = 0; i < Segments.Count; i++)
		{
			var currentSegment = Segments[i];
			var prevPos = CalcPreviousPosition((Node2D)currentSegment.PrevSegment ?? SnakeHeadNode, Segment.SegmentLength);
			currentSegment.SetPosition(prevPos);
		}
	}

	private void AddSegment()
	{
		var newSegment = SegmentScene.Instantiate<Segment>();

		var last = Segments.Count > 0 ? Segments[^1] : null;

		newSegment.PrevSegment = last;
		newSegment.Index = Segments.Count;



		Vector2 behindPoint = CalcPreviousPosition((Node2D)last ?? SnakeHeadNode, Segment.SegmentLength);
		newSegment.GlobalPosition = behindPoint;

		Segments.Add(newSegment);
		AddChild(newSegment);
	}



	private static Vector2 CalcPreviousPosition(Node2D node, int margin)
	{
		return node.GlobalPosition - Vector2.Right.Rotated(node.Rotation) * margin;
	}
}
