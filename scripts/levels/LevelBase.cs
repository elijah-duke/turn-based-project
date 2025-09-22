using Godot;
using System;

public partial class LevelBase : Node2D
{
    private Player player = null;

    public override void _Ready()
    {
        player = GetNode<Player>("Player");
    }

    public override void _Input(InputEvent @event)
    {

        Direction? move = null;

        if (@event.IsActionPressed(Constants.Inputs.MoveUp))
            move = Direction.Up;
        else if (@event.IsActionPressed(Constants.Inputs.MoveDown))
            move = Direction.Down;
        else if (@event.IsActionPressed(Constants.Inputs.MoveLeft))
            move = Direction.Left;
        else if (@event.IsActionPressed(Constants.Inputs.MoveRight))
            move = Direction.Right;

        if (move != null)
        {
            player.Move((Direction)move);
        }
    }
}
