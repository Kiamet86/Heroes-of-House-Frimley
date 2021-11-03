using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_of_House_Frimley.Classes
{
    public class Enemy : Character
    {
        public int EnemyScore { get; }
        public string Appearance { get; }
        private string[] enemyNames = new string[] { "Grimb", "Boogin", "Ratto" };

        public Enemy()
        {
            Name = enemyNames[rnd.Next(enemyNames.Length)];
            MaxHP = rnd.Next(2, 6);
            CurrentHP = MaxHP;
            Strength = rnd.Next(3);
            Appearance = GenerateAppearance(MaxHP, Strength);
            EnemyScore = CalculateEnemyScore(MaxHP, Strength);
            IsDead = false;
        }

        private int CalculateEnemyScore(int maxHP, int strength)
        {
            return strength + MaxHP;
        }

        private string GenerateAppearance(int maxHp, int strength)
        {
            var hpString = (maxHp >= 4 ? "large" : "common");
            var strengthString = (strength >= 1 ? "strong" : "weak");
            return ($"{hpString}, {strengthString}");
        }
    }
}
