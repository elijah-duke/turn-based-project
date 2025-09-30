using Godot;
using System;

public partial class TempGymTwo : Node2D
{

    private void ButtonPressed()
    {
        SceneManager.instance.changeScene(Scenes.LevelThree);
    }
}
