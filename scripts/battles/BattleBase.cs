using Godot;
using System;
using System.Reflection.Metadata.Ecma335;

public partial class BattleBase : Node2D
{
	public enum Moves
	{
		Attack,
		Defend,
		Skills,
		Pass
	}

	struct Fighter
	{
		int health;
		int skillPoints;
		int defenseChance;

		public Fighter(int health, int skillPoints, int defenseChance)
		{
			this.health = health;
			this.skillPoints = skillPoints;
			this.defenseChance = defenseChance;
		}

		public int getHealth()
		{
			return this.health;
		}

		public int getSkillPoints()
		{
			return this.skillPoints;
		}

		public int getDefenseChance()
		{
			return this.defenseChance;
		}


		public void dealDamage() => --health;

		public void incSkillPoints() => ++skillPoints;

		public void decSkillPoints() => --skillPoints;

		public void decDefChance()
		{
			if (defenseChance == 0)
			{
				return;
			}
			defenseChance -= 20;
		}

		public void resetDef() => defenseChance = 100;

		public bool isBlocked()
		{
			Random rng = new Random();
			int r = rng.Next(100);
			if (r < defenseChance)
			{
				return true;
			}
			return false;
		}
	}

	private Fighter player;
	private Fighter rival;

	[Export]
	public Scenes loseScene;
	[Export]
	public Scenes winScene;

	private Button attackButton;
	private Button defendButton;
	private Button skillsButton;

	private Label playerHP;
	private Label playerSP;
	private Label playerDEF;
	private Label rivalHP;


	[Export]
	public int rivalPickAtkChance = 40;
	[Export]
	public int rivalPickDefChance = 30;
	[Export]
	public int rivalPickSkPChance = 30;

	[Export]
	public bool isGym = false;


	public override void _Ready()
	{
		player = new Fighter(3, 1, 100);
		rival = new Fighter(3, 1, 100);

		attackButton = GetNode<Button>("Battle GUI/Buttons/AttackButton");
		defendButton = GetNode<Button>("Battle GUI/Buttons/DefendButton");
		skillsButton = GetNode<Button>("Battle GUI/Buttons/SkillPointButton");

		playerHP = GetNode<Label>("Battle GUI/Info/PlayerHealth");
		playerSP = GetNode<Label>("Battle GUI/Info/PlayerSkillPoints");
		playerDEF = GetNode<Label>("Battle GUI/Info/PlayerDefenseChance");
		rivalHP = GetNode<Label>("Battle GUI/Info/RivalHealth");

		buttonVisibility();
		updateText();
	}

	private void buttonVisibility()
	{
		if (player.getSkillPoints() > 0)
		{
			attackButton.Visible = true;
		}
		else
		{
			attackButton.Visible = false;
		}


		if (player.getDefenseChance() > 0)
		{
			defendButton.Visible = true;
		}
		else
		{
			defendButton.Visible = false;
		}

		if (player.getSkillPoints() < 3)
		{
			skillsButton.Visible = true;
		}
		else
		{
			skillsButton.Visible = false;
		}
	}

	private void updateText()
	{
		playerHP.Text = player.getHealth() + "/3";
		playerSP.Text = player.getSkillPoints() + "/3";
		playerDEF.Text = player.getDefenseChance() + "%";
		rivalHP.Text = rival.getHealth() + "/3";
	}

	private void attackPressed()
	{
		player.decSkillPoints();
		player.resetDef();
		combatHandler(Moves.Attack);
	}

	private void defendPressed()
	{
		combatHandler(Moves.Defend);
	}

	private void skillsPressed()
	{
		player.incSkillPoints();
		player.resetDef();
		combatHandler(Moves.Skills);
	}

	private Moves generateRivalMove()
	{
		Moves rivalMove;

		Random rng = new Random();
		int r = rng.Next(100);
		if (r < rivalPickAtkChance)
		{
			rivalMove = Moves.Pass;
			rival.resetDef();
			GD.Print("Attack");
			if (rival.getSkillPoints() > 0)
			{
				rivalMove = Moves.Attack;
				rival.decSkillPoints();
			}

		}
		else if (r < rivalPickAtkChance + rivalPickDefChance)
		{
			rivalMove = Moves.Defend;
			GD.Print("Defend");
		}
		else
		{
			rivalMove = Moves.Pass;
			rival.resetDef();
			GD.Print("Skills");
			if (rival.getSkillPoints() < 3)
			{
				rivalMove = Moves.Skills;
				rival.incSkillPoints();
			}
		}

		return rivalMove;
	}

	private void combatHandler(Moves playerMove)
	{
		Moves rivalMove = generateRivalMove();

		if (playerMove == Moves.Attack &&
			(rivalMove != Moves.Defend || rivalMove == Moves.Defend && !rival.isBlocked()))
		{
			rival.dealDamage();
		}

		updateText();
		winCondition();

		if (rivalMove == Moves.Attack &&
			(playerMove != Moves.Defend || playerMove == Moves.Defend && !player.isBlocked()))
		{
			player.dealDamage();
		}

		if (playerMove == Moves.Defend)
		{
			player.decDefChance();
		}
		if (rivalMove == Moves.Defend)
		{
			rival.decDefChance();
		}

		updateText();
		buttonVisibility();
		winCondition();
	}

	private void winCondition()
	{
		if (player.getHealth() <= 0)
		{
			if (isGym)
			{
				SceneManager.instance.SetPlayerPos(Vector2I.Zero);
			}
			SceneManager.instance.ChangeScene(loseScene);
		}
		else if (rival.getHealth() <= 0)
		{
			if (isGym)
			{
				SceneManager.instance.SetPlayerPos(Vector2I.Zero);
			}
			SceneManager.instance.ChangeScene(winScene);
		}

	}
	
}
