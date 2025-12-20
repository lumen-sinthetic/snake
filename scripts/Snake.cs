using Godot;

public partial class Snake : CharacterBody2D
{
	const float JumpVelocity = -400.0f;
	const float Speed = 200f;
	Vector2 Destination = Vector2.Right;

	public override void _PhysicsProcess(double delta)
	{
		var input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if (input != Vector2.Zero) Destination = input;

		Velocity = Destination * Speed;

		MoveAndSlide();

		Rotation = Destination.Angle();
	}
}
