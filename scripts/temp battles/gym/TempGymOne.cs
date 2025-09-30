using Godot;
using System;

public partial class TempGymOne : Node2D
{
    private void ButtonPressed()
    {
        SceneManager.instance.changeScene(Scenes.LevelTwo);
    }
}
