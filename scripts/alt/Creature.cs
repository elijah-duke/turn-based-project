using Godot;
using System;

public partial class Creature : Node2D
{
    public string Name { get; set; }
    public int Level { get; set; }

    public Creature (string name, int level)
    {
        Name = name;
        Level = level;
    }
}
