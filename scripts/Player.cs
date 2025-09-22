using Godot;
using System;

public partial class Player : Node2D
{
	[Export]
	float HorzMoveTime = 1.0f / 3.0f;
	[Export]
	float VertMoveTime = 1.0f / 3.0f;


	private Sprite2D playerSprite;

	public override void _Ready()
	{
		playerSprite = GetNode<Sprite2D>("PlayerSprite");
	}


	public void Move(Direction direction) 
	{
		Vector2 offset = Vector2.Zero;
		float moveTime = 0.0f;

		switch (direction)
		{
			case Direction.Up:
				offset = Vector2.Up * Constants.TILE_HEIGHT;
				moveTime = VertMoveTime;
				break;
			case Direction.Down:
				offset = Vector2.Down * Constants.TILE_HEIGHT;
				moveTime = VertMoveTime;
				break;
			case Direction.Left:
				offset = Vector2.Left * Constants.TILE_WIDTH;
				moveTime = HorzMoveTime;
				break;
			case Direction.Right:
				offset = Vector2.Right * Constants.TILE_HEIGHT;
				moveTime = HorzMoveTime;
				break;
			default:
				offset = Vector2.Zero;
				break;
		}

		if (offset != Vector2.Zero) 
		{
			var moveTween = CreateTween();
			moveTween.TweenProperty(this, "position", Position + offset, moveTime);
		}





	}
}
