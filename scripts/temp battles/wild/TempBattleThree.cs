using Godot;
using System;

public partial class TempBattleThree : Node2D
{
    
    private void ButtonPressed()
    {
        SceneManager.instance.changeScene(Scenes.LevelThree);
    }
}
