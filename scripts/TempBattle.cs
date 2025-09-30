using Godot;
using System;

public partial class TempBattle : Node2D
{
    
    private void ButtonPressed()
    {
        SceneManager.instance.changeScene(Scenes.LevelOne);
    }
}
