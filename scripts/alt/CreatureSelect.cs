using Godot;
using System.Collections.Generic;

public partial class CreatureSelect : Control
{
    private PlayerData _playerData;
    private Creature _selectedCreature;
    private VBoxContainer _container;
    private Button _confirmButton;

    public override void _Ready()
    {
        _playerData = GetNode<PlayerData>("/root/PlayerData");
        _container = GetNode<VBoxContainer>("VBoxContainer");
        _confirmButton = GetNode<Button>("ConfirmButton");

        _confirmButton.Text = "Start Battle";
        _confirmButton.Disabled = true;
        _confirmButton.Pressed += OnConfirmPressed;

        // dynamically create a button for each captured creature
        foreach (var creature in _playerData.CapturedCreatures)
        {
            var button = new Button();
            button.Text = $"{creature.Name} (Lv. {creature.Level})";
            button.Pressed += () => OnCreatureSelected(creature);
            _container.AddChild(button);
        }
    }

    private void OnCreatureSelected(Creature creature)
    {
        _selectedCreature = creature;
        _confirmButton.Disabled = false;
        GD.Print($"Selected {creature.Name}");
    }

    private void OnConfirmPressed()
    {
        if (_selectedCreature != null)
        {
            _playerData.SetCurrentCreature(_selectedCreature);
            GD.Print($"Starting battle with {_selectedCreature.Name}!");
            GetTree().ChangeSceneToFile("res://scenes/alt/battle_i.tscn");
        }
        else
        {
            GD.Print("No creature selected!");
        }
    }
}
