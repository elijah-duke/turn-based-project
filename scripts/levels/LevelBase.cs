using Godot;
using System;

public partial class LevelBase : Node2D
{
    private enum State { Idle, Moving };

    private State state = State.Idle;
    private Player player = null;

    public override void _Ready()
    {
        player = GetNode<Player>("Player");
    }

    public override void _Input(InputEvent @event)
    {

        if (state != State.Idle) { return; }

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
            state = State.Moving;
        }
    }


    public void OnPlayerMoveFinished()
    {
        var direction = getMove();
        if (direction != null)
        {
            player.Move((Direction)direction);
        }
        else
        {
           state = State.Idle; 
        }
    }

    private Direction? getMove()
    {
        if (Input.IsActionPressed(Constants.Inputs.MoveUp))
            return Direction.Up;
        else if (Input.IsActionPressed(Constants.Inputs.MoveDown))
            return Direction.Down;
        else if (Input.IsActionPressed(Constants.Inputs.MoveLeft))
            return Direction.Left;
        else if (Input.IsActionPressed(Constants.Inputs.MoveRight))
            return Direction.Right;

        return null;
    }
}
