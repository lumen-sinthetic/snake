using Godot;
using System;
using System.Threading;

public partial class Snake : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	void Move()
	{
		var vector = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		// var axis = Input.GetAxis("ui_left", "ui_right");
		Position += vector * 4;
		// Rotation = axis;
	}

	public override void _PhysicsProcess(double delta)
	{
		Move();
		// Vector2 velocity = Velocity;

		// // Add the gravity.
		// if (!IsOnFloor())
		// {
		// 	velocity += GetGravity() * (float)delta;
		// }

		// // Handle Jump.
		// if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		// {
		// 	velocity.Y = JumpVelocity;
		// }

		// // Get the input direction and handle the movement/deceleration.
		// // As good practice, you should replace UI actions with custom gameplay actions.
		// Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		// if (direction != Vector2.Zero)
		// {
		// 	velocity.X = direction.X * Speed;
		// }
		// else
		// {
		// 	velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		// }

		// Velocity = velocity;
		// MoveAndSlide();
	}
}
