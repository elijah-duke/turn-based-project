using Godot;
using System;

public enum Direction 
{
    Up,
    Down,
    Left,
    Right;
}

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
        Vector2 offset = Vecttor2.Zero;
        float moveTime = 0.0f;

        switch (Direction) 
        {
            case Direction.Up:
                offset = new Vector2(0.0f, TILE_HEIGHT);
                moveTime = VertMoveTime
                break;
            case Direction.Down:
                offset = new Vector2(0.0f, -TILE_HEIGHT);
                moveTime = VertMoveTime;
                break;
            case Direction.Left:
                offset = new Vector2(-TILE_WIDTH, 0.0f);
                moveTIme = HorzMoveTime;
                break;
            case Direction.Right:
                offset = new Vector2(TILE_WIDTH, 0.0f);
                moveTime = HorzMoveTime
                break;
            default:
                offset = new Vector2.Zero;
        }

        if (offset != Vector2.Zero) 
        {
            var moveTween = CreateTween();
            moveTween.TweenProperty(this, "position", Position + offset, moveTime);
        }





    }
}
