using System.Collections.Generic;
using Godot;

public partial class Snake : Node2D
{

	[ExportGroup("BodyParts")]
	[Export] private SnakeHead SnakeHeadNode = null!;
	[Export] private PackedScene SegmentScene = null!;


	[ExportGroup("Landscape")]
	[Export] private Apples ApplesField = null!;
	[Export] private TileMapLayer Terrain = null!;

	private const float MoveDuration = 0.7f;
	private const int SegmentsOffset = 1;
	private int PathHistoryLength;

	private readonly List<Segment> Segments = [];
	private List<Vector2I> PathHistory = [];

	public override void _Ready()
	{
		AddSegment();
		SnakeHeadNode.EatApple += OnAppleEaten;
		SnakeHeadNode.HeadMove += OnHeadMove;
		PathHistoryLength = Terrain.GetUsedCells().Count;
	}

	public override void _Process(double delta)
	{
		var input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		SnakeHeadNode.Move(input, MoveDuration);
	}

	private void OnAppleEaten(Vector2I pos)
	{
		ApplesField.RemoveApple(pos);
		ApplesField.AddApple();
		Global.Instance.AddScore();
		AddSegment();
	}

	private void OnHeadMove(Vector2I pos)
	{
		PathHistory.Add(pos);
		MoveSegments();

		if (PathHistory.Count > PathHistoryLength)
		{
			var slice = PathHistory.GetRange(PathHistory.Count - PathHistoryLength, PathHistoryLength);
			PathHistory = slice;
		}
	}


	private void MoveSegments()
	{
		for (int i = 0; i < Segments.Count; i++)
		{
			var currentSegment = Segments[i];
			Segment? nextSegment = currentSegment.NextSegment;

			Vector2 moveTo = Terrain.MapToLocal(PathHistory[^(i + SegmentsOffset)]);
			currentSegment.Move(moveTo, MoveDuration);

			currentSegment.RotateTo((Node2D?)nextSegment ?? SnakeHeadNode);
		}
	}

	private void AddSegment()
	{
		var newSegment = SegmentScene.Instantiate<Segment>();

		var pos = PathHistory.Count > (Segments.Count + SegmentsOffset)
		? PathHistory[^(Segments.Count + SegmentsOffset)]
		: SnakeHeadNode.PrevPos;

		if (Segments.Count > 0)
		{
			var nextSegment = Segments[^1];
			nextSegment.ToBody();
			newSegment.NextSegment = nextSegment;
			// newSegment.RotateTo(newSegment);
		}

		AddChild(newSegment);
		Segments.Add(newSegment);

		newSegment.GlobalPosition = Terrain.MapToLocal(pos);
	}
}
