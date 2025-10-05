using Godot;
using System;
using TurnBasedProject;

public partial class Battle : Node2D
{
    private enum BattleState
    {
        WaitingForPlayer,
        EnemyTurn,
        BattleOver
    }

    private Label PlayerHealth;
    private Label EnemyHealth;

    private BattleState _state = BattleState.WaitingForPlayer;

    [Export] private PackedScene[] creatureScenes;

    private Fighter _player;
    private Fighter _enemy;

    private Random _rng = new Random();

    private bool canSpecialAttack = true;

    public override void _Ready()
    {
        var playerData = GetNode<PlayerData>("/root/PlayerData");

        // Get the creature the player selected
        var currentCreature = playerData.CurrentCreature;
        _player = new Fighter(currentCreature.Name, 100, 15);

        var playerSpawn = GetNode<Marker2D>("PlayerCreatureSpawnPoint");

        var playerScenePath = $"res://scenes/Entities/{currentCreature.Name}Creature.tscn";
        var playerScene = GD.Load<PackedScene>(playerScenePath);

        if (playerScene != null)
        {
            var playerCreatureNode = playerScene.Instantiate<Node2D>();
            playerCreatureNode.Position = playerSpawn.Position;
            AddChild(playerCreatureNode);
        }

        int index = _rng.Next(creatureScenes.Length);
        var chosenScene = creatureScenes[index];
        var enemyNode = chosenScene.Instantiate<CreatureData>();

        var enemySpawn = GetNode<Marker2D>("EnemySpawnPoint");
        enemyNode.Position = enemySpawn.Position;

        AddChild(enemyNode);
        _enemy = enemyNode.ToFighter();



        PlayerHealth = GetNode<Label>("AttackUI/Info/PlayerHealth");
        EnemyHealth = GetNode<Label>("AttackUI/Info/EnemyHealth");
        UpdateHealthLabels();



        GD.Print($"A {_enemy.Name} appeared!");
    }

    private void UpdateHealthLabels()
    {
        if (PlayerHealth != null && _player != null)
        {
            PlayerHealth.Text = $"{_player.CurrentHealth}/{_player.MaxHealth}";
        }

        if (EnemyHealth != null && _enemy != null)
        {
            EnemyHealth.Text = $"{_enemy.CurrentHealth}/{_enemy.MaxHealth}";
        }
    }

    private void PlayerTurn()
    {
        int damage = _player.AttackPower;
        //20% chance of critical hit
        if (_rng.NextDouble() < 0.2)
        {
            damage *= 2;
            GD.Print("Critical hit!");
        }

        _enemy.CurrentHealth -= damage;
        UpdateHealthLabels();

        GD.Print($"{_enemy.Name} HP: {_enemy.CurrentHealth}");
        GD.Print($"Player HP: {_player.CurrentHealth}");

        if (!_enemy.IsAlive)
        {
            GD.Print("Battle won!");
            _state = BattleState.BattleOver;
            OnPlayerWin();
            return;
        }

        canSpecialAttack = true;


        _state = BattleState.EnemyTurn;
        //EnemyTurn();
        CallDeferred(nameof(StartEnemyTurn));
    }

    private async void StartEnemyTurn()
    {
        await ToSignal(GetTree().CreateTimer(1.0), "timeout");
        EnemyTurn();
    }

    // can only be used every other player turn
    private void SpecialAttack()
    {
        if (canSpecialAttack == true)
        {
            int damage = _player.AttackPower + 10;
            _enemy.CurrentHealth -= damage;
            UpdateHealthLabels();

            GD.Print($"{_enemy.Name} HP: {_enemy.CurrentHealth}");
            GD.Print($"Player HP: {_player.CurrentHealth}");

            if (!_enemy.IsAlive)
            {
                GD.Print("Battle won!");
                _state = BattleState.BattleOver;
                OnPlayerWin();
                return;
            }

            canSpecialAttack = false;

            _state = BattleState.EnemyTurn;
            //EnemyTurn();
            CallDeferred(nameof(StartEnemyTurn));
        }
        else
        {
            GD.Print("Cannot Special Attack right now!");
        }
        
    }

    private void Escape()
    {
        GD.Print("You escaped the battle! Fight lost!");
        OnPlayerLose();
    }

    private void EnemyTurn()
    {
        int damage = _enemy.AttackPower;

        _player.CurrentHealth -= damage;
        UpdateHealthLabels();
        //print something
        GD.Print($"{_enemy.Name} HP: {_enemy.CurrentHealth}");
        GD.Print($"Player HP: {_player.CurrentHealth}");

        if (!_player.IsAlive)
        {
            GD.Print("Battle lost!");
            _state = BattleState.BattleOver;
            OnPlayerLose();
            return;
        }

        _state = BattleState.WaitingForPlayer;
        //PlayerTurn();
    }

    private void OnPlayerWin()
    {
        //display win scene

        var capturedCreature = new Creature(_enemy.Name, 1);
        var playerData = GetNode<PlayerData>("/root/PlayerData");
        playerData.CaptureCreature(capturedCreature);

        GD.Print($"{_enemy.Name} captured!");
        ReturnToLevelOne();
    }

    private void OnPlayerLose()
    {
        //display lose scene
        ReturnToLevelOne();
    }

    private void ReturnToLevelOne()
    {
        GetTree().ChangeSceneToFile("res://scenes/levels/LevelOne.tscn");
    }

    private void _on_AttackButton_pressed()
    {
        if (_state != BattleState.WaitingForPlayer)
            return;

        PlayerTurn();
    }

    private void _on_SpecialAttackButton_pressed()
    {
        if (_state != BattleState.WaitingForPlayer)
            return;

        SpecialAttack();
    }

    private void _on_EscapeButton_pressed()
    {
        Escape();
    }
}
