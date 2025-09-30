using Godot;
using System;

public partial class TempBattleOne : Node2D
{
    
    private void ButtonPressed()
    {
        SceneManager.instance.changeScene(Scenes.LevelOne);
    }
}
