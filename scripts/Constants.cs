public static class Constants
{
    public const float TILE_WIDTH = 16;
    public const float TILE_HEIGHT = 16;

    public static class Inputs
    {
        public const string MoveUp = "move_up";
        public const string MoveDown = "move_down";
        public const string MoveLeft = "move_left";
        public const string MoveRight = "move_right";
    }



}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}


namespace Godot
{
    public static class Vector2IExtensions
    {
        public static Vector2I Offset(this Vector2I coords, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return coords + Vector2I.Up;
                case Direction.Down:
                    return coords + Vector2I.Down;
                case Direction.Left:
                    return coords + Vector2I.Left;
                case Direction.Right:
                    return coords + Vector2I.Right;
                default:
                    return coords;
            }
        }
    }
}