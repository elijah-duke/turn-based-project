using Godot;
using System;

public partial class LevelBase : Node2D
{
    private enum State { Idle, Moving };

    private State state = State.Idle;
    private Player player = null!;
    private TileMapLayer ground = null!;
    private TileMapLayer walls = null!;



    [Export]
    public Scenes encounterScene;
    [Export]
    public Scenes gymScene;
    
    public override void _Ready()
    {
        player = GetNode<Player>("Player");
        ground = GetNode<TileMapLayer>("Ground");
        walls = GetNode<TileMapLayer>("Walls");
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

        makeMove(move);
    }

    public void Encounter()
    {
        if (SceneManager.instance != null)
        {
            Random rng = new Random();
            int r = rng.Next(100);
            if (r < 5)
            {
                SceneManager.instance.changeScene(encounterScene);
            }
        }
    }

    public void GymChallenge()
    {
        if (SceneManager.instance != null)
        {
            SceneManager.instance.changeScene(gymScene);
        }
    }


    public void OnPlayerMoveFinished()
    {
        if ((bool)ground.GetCellTileData(player.Coords).GetCustomData("wildGrass"))
        {
            Encounter();
        }
        else if ((bool)ground.GetCellTileData(player.Coords).GetCustomData("target"))
        {
            GymChallenge();
        }
        makeMove(getMove());
    }

    private void makeMove(Direction? direction)
    {
        if (direction != null && playerCanMove((Direction)direction))
        {
            player.Move((Direction)direction);
            state = State.Moving;
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

    private bool playerCanMove(Direction direction)
    {
        return walls.GetCellTileData(player.Coords.Offset(direction)) == null;
    }

}
