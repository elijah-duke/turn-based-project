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