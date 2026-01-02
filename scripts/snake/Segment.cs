using Godot;
using System;
using System.Runtime.InteropServices;


#nullable enable

public partial class Segment : Node2D
{

	public int Index;
	public Segment? PrevSegment;
	public Segment? NextSegment;

	public static readonly int SegmentLength = 128;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


	public new void SetPosition(Vector2 pos)
	{
		GlobalPosition = pos;
		// GlobalRotation = pos.Angle();
	}
}
