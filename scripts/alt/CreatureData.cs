using Godot;
using System;
using TurnBasedProject;

public partial class CreatureData : Node2D
{
    [Export] public string CreatureName { get; set; } = "Unnamed";
    [Export] public int MaxHealth { get; set; } = 80;
    [Export] public int AttackPower { get; set; } = 20;

    public Fighter ToFighter()
    {
        return new Fighter(CreatureName, MaxHealth, AttackPower);
    }
}
