using Godot;

public partial class SnakeHead : CharacterBody2D
{
	[Export]
	private Sprite2D HeadSprite;

	[ExportGroup("Landscape")]
	[Export] private TileMapLayer TerrainLayer;
	[Export] private TileMapLayer AppleLayer;

	[ExportGroup("Textures")]
	[Export] private Texture2D NormalTexture;
	[Export] private Texture2D OpenMouthTexture;


	[Signal]
	public delegate void EatAppleEventHandler(Vector2I pos);
	[Signal]
	public delegate void HeadMoveEventHandler(Vector2I pos);


	private float Angle;
	private bool IsMoving = false;
	private Vector2I Direction = Vector2I.Zero;
	public Vector2I TilePos { get; private set; } = new(3, 0);
	public Vector2I PrevPos { get; private set; } = new(2, 0);

	public void Move(Vector2 input, float duration)
	{
		if (input == Vector2.Zero && Direction == Vector2.Zero) return;

		if (input != Vector2.Zero && !IsOrdinal(input))
		{
			Direction = new Vector2I((int)input.X, (int)input.Y);
			Angle = input.Angle();
		}

		TryEatApple();
		if (!IsMoving) TryMove(Direction, Angle, duration);
	}

	public override void _Ready()
	{
		GlobalPosition = TerrainLayer.MapToLocal(TilePos);
	}

#nullable enable

	private void TryMove(Vector2I dir, float angle, float duration)
	{
		Vector2I targetTile = TilePos + dir;

		Vector2 startPos = GlobalPosition;
		Vector2 endPos = TerrainLayer.MapToLocal(targetTile);
		Rotation = angle;


		if (TerrainLayer.GetCellSourceId(targetTile) == -1) return;


		EmitSignal(SignalName.HeadMove, TilePos);

		IsMoving = true;
		PrevPos = TilePos;
		TilePos = targetTile;

		var tween = CreateTween();

		tween.TweenProperty(this, "global_position", endPos, duration).SetTrans(Tween.TransitionType.Linear);

		tween.Finished += () =>
		{
			GlobalPosition = endPos;
			IsMoving = false;
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
