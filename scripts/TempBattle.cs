using Godot;
using System;

public partial class TempBattle : Node2D
{
    [Export]
    public PackedScene LevelScene = null;
    
    private void ButtonPressed()
    {
        if (LevelScene != null)
        {
            GetTree().ChangeSceneToPacked(LevelScene);
        }
    }
}
