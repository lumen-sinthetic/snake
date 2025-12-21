using Godot;

public partial class SnakeHead : CharacterBody2D
{
	const float Speed = 200f;
	Vector2 _Destination = Vector2.Right;
	Sprite2D _Sprite;


	[Signal]
	public delegate void ActionRequestedEventHandler();



	public override void _PhysicsProcess(double delta)
	{
		var input = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");

		if (input != Vector2.Zero) _Destination = input;

		Velocity = _Destination * Speed;

		MoveAndSlide();

		Rotation = _Destination.Angle();
	}

	public override void _Ready()
	{
		_Sprite = GetNode<Sprite2D>("Sprite2D");
	}

	public void Eat()
	{
		OpenMouth();
		EmitSignal(SignalName.ActionRequested);
	}


	void OpenMouth()
	{
		var newTexture = GD.Load<Texture2D>("res://art/snake/голова-ест.png");
		var normalTexture = GD.Load<Texture2D>("res://art/snake/голова.png");
		_Sprite.Texture = newTexture;

		var timer = GetTree().CreateTimer(0.3f);
		timer.Timeout += () =>
		{
			_Sprite.Texture = normalTexture;
		};
	}
}
