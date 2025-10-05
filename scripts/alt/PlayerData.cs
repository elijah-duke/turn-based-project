using Godot;
using System.Collections.Generic;

public partial class PlayerData : Node
{
    public List<Creature> CapturedCreatures = new List<Creature>();
    public Creature CurrentCreature { get; private set; }
    public int CurrentLevel { get; private set; } = 1;

    //private readonly string[] AllCreatureNames = { "Insect", "Mouse", "Golem", "Pitchfork" };
    //private readonly string[] AllCreatureNames2 = { "Bow", "Mushroom", "Log" };
    private readonly Dictionary<int, string[]> RequiredCreaturesPerLevel = new()
    {
        {1, new string[] {"Insect", "Mouse", "Golem", "Pitchfork"} },
        {2, new string[] {"Bow", "Mushroom", "Log"} },
        {3, new string[] {"Sign", "Plant", "Well"} }
    };

    public override void _Ready()
    {
        // Give player a starter if none exists
        if (CapturedCreatures.Count == 0)
        {
            var starter = new Creature("Pitchfork", 1);
            CaptureCreature(starter);
            SetCurrentCreature(starter);
            GD.Print("Starter creature assigned: Pitchfork");
        }
    }

    public void CaptureCreature(Creature newCreature)
    {
        if(!CapturedCreatures.Exists(c => c.Name == newCreature.Name))
        {
            CapturedCreatures.Add(newCreature);
            GD.Print($"{newCreature.Name} was added to the player's collection!");
        }

        if (CurrentCreature == null)
            CurrentCreature = newCreature;

        CheckForLevelCompletion();
    }

    public void SetCurrentCreature(Creature creature)
    {
        if (CapturedCreatures.Contains(creature))
        {
            CurrentCreature = creature;
            GD.Print($"Active creature set to {creature.Name}");
        }
        else
        {
            GD.PrintErr("Creature not found in player’s collection.");
        }
    }
     
    private void CheckForLevelCompletion()
    {
        if (!RequiredCreaturesPerLevel.ContainsKey(CurrentLevel))
            return;

        var required = RequiredCreaturesPerLevel[CurrentLevel];
        var capturedNames = new HashSet<string>();
        foreach (var c in CapturedCreatures)
            capturedNames.Add(c.Name);

        foreach (var name in required)
        {
            if (!capturedNames.Contains(name)) // not all creatures captured yet
                return; 
        }

        GD.Print($"Level {CurrentLevel} completed!");
        AdvanceToNextLevel();
    }

    private void AdvanceToNextLevel()
    {
        //GetTree().ChangeSceneToFile("res://scenes/levels/LevelTwo.tscn");
        CurrentLevel++;

        CurrentCreature = null;

        string nextLevelScene = CurrentLevel switch
        {
            2 => "res://scenes/levels/LevelTwo.tscn",
            3 => "res://scenes/levels/LevelThree.tscn",
            _ => null
        };

        if(nextLevelScene != null)
        {
            GD.Print($"Advancing to level {CurrentLevel}");
            GetTree().ChangeSceneToFile(nextLevelScene);
        }
        else
        {
            GD.Print("No more levels defined.");
        }
    }
}
