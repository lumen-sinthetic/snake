using System.Collections.Generic;
using Godot;

public partial class Snake : Node2D
{

	[ExportGroup("BodyParts")]
	[Export] private SnakeHead SnakeHeadNode;
	[Export] private PackedScene SegmentScene;


	[ExportGroup("Landscape")]
	[Export] private Apples ApplesField;
	[Export] private TileMapLayer Terrain;

	private readonly List<Segment> Segments = [];
	private const float MoveDuration = 0.75f;


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
		SnakeHeadNode.Move(input, MoveDuration);
	}

	private void OnAppleEaten(Vector2I pos)
	{
		ApplesField.RemoveApple(pos);
		ApplesField.AddApple();
		Global.Instance.AddScore();
		AddSegment();
	}


	private void MoveSegments(Vector2I pos)
	{
		for (int i = 0; i < Segments.Count; i++)
		{
			var currentSegment = Segments[i];
			Vector2 moveTo = currentSegment.NextSegment != null ? currentSegment.NextSegment.GlobalPosition : Terrain.MapToLocal(pos);
			currentSegment.Move(moveTo, MoveDuration);
		}
	}

	private void AddSegment()
	{
		var newSegment = SegmentScene.Instantiate<Segment>();

		var last = Segments.Count > 0 ? Segments[^1] : null;

		if (last != null) last.NextSegment = newSegment;
		newSegment.PrevSegment = last;

		newSegment.Index = Segments.Count;

		Segments.Add(newSegment);
		AddChild(newSegment);
	}
}
