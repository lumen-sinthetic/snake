using Godot;

public partial class SnakeHead : CharacterBody2D
{

	[Export]
	protected TileMapLayer TerrainLayer;
	[Export]
	protected TileMapLayer AppleLayer;
	[Export]
	protected Sprite2D HeadSprite;

	[Signal]
	public delegate void EatAppleEventHandler(Vector2I pos);
	[Signal]
	public delegate void HeadMoveEndEventHandler(Vector2 pos);


	private const float MoveDuration = 0.75f;

	private float Angle;
	private bool IsMoving = false;
	private Vector2I Direction = Vector2I.Zero;
	public Vector2I TilePos { get; private set; } = new(3, 0);


	private readonly Texture2D NormalTexture = GD.Load<Texture2D>("res://art/snake/голова.png");
	private readonly Texture2D OpenMouthTexture = GD.Load<Texture2D>("res://art/snake/голова-ест.png");


	// public override void _PhysicsProcess(double delta)
	// {
	// }

	public override void _Process(double delta)
	{
	}


	public void Move(Vector2 input)
	{
		if (input != Vector2.Zero && !IsOrdinal(input))
		{
			Direction = new Vector2I((int)input.X, (int)input.Y);
			Angle = input.Angle();
		}

		TryEatApple();
		if (!IsMoving) TryMove(Direction, Angle);
	}

	public override void _Ready()
	{
		GlobalPosition = TerrainLayer.MapToLocal(TilePos);
	}

#nullable enable

	private void TryMove(Vector2I dir, float angle = 0)
	{
		Vector2I targetTile = TilePos + dir;

		Vector2 startPos = GlobalPosition;
		Vector2 endPos = TerrainLayer.MapToLocal(targetTile);
		Rotation = angle;


		if (TerrainLayer.GetCellSourceId(targetTile) == -1) return;

		IsMoving = true;
		TilePos = targetTile;


		// var tween = CreateTween();

		// tween.TweenProperty(this, "global_position", endPos, MoveDuration).SetTrans(Tween.TransitionType.Linear);

		// tween.Finished += () =>
		// {
		// 	GlobalPosition = endPos;
		// 	IsMoving = false;
		// };

		var timer = GetTree().CreateTimer(MoveDuration);
		timer.Timeout += () =>
		{
			GlobalPosition = endPos;
			IsMoving = false;
			EmitSignal(SignalName.HeadMoveEnd, GlobalPosition);
		};
	}
	private void TryEatApple()
	{
		TileData? tileData = AppleLayer.GetCellTileData(TilePos);
		if (tileData == null) return;
		var tileType = tileData.GetCustomData("type");
		if ((string)tileType == "apple")
		{
			OpenMouth();
			EmitSignal(SignalName.EatApple, TilePos);
		}
	}
	private void OpenMouth()
	{
		HeadSprite.Texture = OpenMouthTexture;

		var timer = GetTree().CreateTimer(0.3f);
		timer.Timeout += () => HeadSprite.Texture = NormalTexture;
	}

	private static bool IsOrdinal(Vector2 v) => v.X != 0 && v.Y != 0 && Mathf.Abs(v.X) == Mathf.Abs(v.Y);
}
