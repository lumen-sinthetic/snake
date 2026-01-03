using Godot;
using System;

public partial class Hud : Control
{
	[Export]
	private Label ScoreLabel = null!;
	[Export]
	private Label LoseLabel = null!;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ScoreLabel.Text = $"Score: {Global.Instance.Score}";
		LoseLabel.Visible = Global.Instance.IsLost;
	}
}
