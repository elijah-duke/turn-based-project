using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;


public class SceneData
{
    public SceneData(string path, string name)
    {
        this.path = path;
        this.name = name;
    }
    public string path { set; get; }
    public string name { set; get; }
}

public enum Scenes
{
    LevelOne,
    BattleOne,
    GymOne,


    LevelTwo,
    BattleTwo,
    GymTwo,


    LevelThree,
    BattleThree,
    GymThree,

    Win
}


public partial class SceneManager : Node
{

    private Vector2I _player_position;
    public static SceneManager instance;
    public Dictionary<Scenes, SceneData> sceneDictionary = new Dictionary<Scenes, SceneData>()
    {
        { Scenes.LevelOne, new SceneData("res://scenes/levels/LevelOne.tscn", "Level One") },
        { Scenes.BattleOne, new SceneData("res://scenes/battles/wild/WildOne.tscn", "Battle One") },
        { Scenes.GymOne, new SceneData("res://scenes/battles/gym/GymOne.tscn", "Gym One") },

        { Scenes.LevelTwo, new SceneData("res://scenes/levels/LevelTwo.tscn", "Level Two") },
        { Scenes.BattleTwo, new SceneData("res://scenes/battles/wild/WildTwo.tscn", "Battle Two") },
        { Scenes.GymTwo, new SceneData("res://scenes/battles/gym/GymTwo.tscn", "Gym Two") },

        { Scenes.LevelThree, new SceneData("res://scenes/levels/LevelThree.tscn", "Level Three") },
        { Scenes.BattleThree, new SceneData("res://scenes/battles/wild/WildThree.tscn", "Battle Three") },
        { Scenes.GymThree, new SceneData("res://scenes/battles/gym/GymThree.tscn", "Gym Three") },

        { Scenes.Win, new SceneData("res://scenes/Win.tscn", "Win Screen")}
    };

    public override void _Ready()
    {
        if (instance != null)
        {
            QueueFree();
            return;
        }
        instance = this;

        _player_position = Vector2I.Zero;
    }

    public void ChangeScene(Scenes scene)
    {
        string scenePath = sceneDictionary[scene].path;
        GetTree().ChangeSceneToFile(scenePath);
    }

    public Vector2I GetPlayerPos()
    {
        return _player_position;
    }

    public void SetPlayerPos(Vector2I pos)
    {
        _player_position = pos;
        GD.Print("The new position is " + _player_position);
    }
 
}
