using Godot;
using System;

public partial class TempBattleTwo : Node2D
{
    
    private void ButtonPressed()
    {
        SceneManager.instance.changeScene(Scenes.LevelTwo);
    }
}
