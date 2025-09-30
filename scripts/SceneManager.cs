using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;


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
    TempBattleOne,
    TempGymOne,


    LevelTwo,
    TempBattleTwo,
    TempGymTwo,


    LevelThree,
    TempBattleThree,
    TempGymThree
}


public partial class SceneManager : Node
{

    public static SceneManager instance;
    public Dictionary<Scenes, SceneData> sceneDictionary = new Dictionary<Scenes, SceneData>()
    {
        { Scenes.LevelOne, new SceneData("res://scenes/levels/LevelOne.tscn", "Level One") },
        { Scenes.TempBattleOne, new SceneData("res://scenes/temp battles/wild/TempBattleOne.tscn", "Temp Battle One") },
        { Scenes.TempGymOne, new SceneData("res://scenes/temp battles/gym/TempGymOne.tscn", "Temp Gym One") },

        { Scenes.LevelTwo, new SceneData("res://scenes/levels/LevelTwo.tscn", "Level Two") },
        { Scenes.TempBattleTwo, new SceneData("res://scenes/temp battles/wild/TempBattleTwo.tscn", "Temp Battle Two") },
        { Scenes.TempGymTwo, new SceneData("res://scenes/temp battles/gym/TempGymTwo.tscn", "Temp Gym Two") },

        { Scenes.LevelThree, new SceneData("res://scenes/levels/LevelThree.tscn", "Level Three") },
        { Scenes.TempBattleThree, new SceneData("res://scenes/temp battles/wild/TempBattleThree.tscn", "Temp Battle Three") },
        { Scenes.TempGymThree, new SceneData("res://scenes/temp battles/gym/TempGymThree.tscn", "Temp Gym Three") }
    };

    public override void _Ready()
    {
        if (instance != null)
        {
            QueueFree();
            return;
        }
        instance = this;
    }

    public void changeScene(Scenes scene)
    {
        string scenePath = sceneDictionary[scene].path;
        GetTree().ChangeSceneToFile(scenePath);
    }

}
