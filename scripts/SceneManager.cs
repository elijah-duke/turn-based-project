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
    TempBattle,
    TempGym
}


public partial class SceneManager : Node
{

    public static SceneManager instance;
    public Dictionary<Scenes, SceneData> sceneDictionary = new Dictionary<Scenes, SceneData>()
    {
        { Scenes.LevelOne, new SceneData("res://scenes/levels/LevelOne.tscn", "Level One") },
        { Scenes.TempBattle, new SceneData("res://scenes/TempBattle.tscn", "Temp Battle") },
        { Scenes.TempGym, new SceneData("res://scenes/TempGym.tscn", "Temp Gym") }
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
