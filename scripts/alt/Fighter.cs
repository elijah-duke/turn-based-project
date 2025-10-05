using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurnBasedProject
{
    public class Fighter
    {
        public string Name { get; set; }
        public int MaxHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int AttackPower { get; set; }

        public Fighter (string name, int maxHealth, int attackPower)
        {
            Name = name;
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            AttackPower = attackPower;
        }

        public bool IsAlive => CurrentHealth > 0;
    }
}
