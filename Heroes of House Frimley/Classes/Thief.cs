using Heroes_of_House_Frimley.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_of_House_Frimley.Classes
{
    class Thief : Character, IHealable
    {
        public Thief(string name)
        {
            Name = name;
            MaxHP = 15;
            CurrentHP = MaxHP;
            MaxMP = 10;
            CurrentMP = MaxMP;
            Strength = 1;
            Intelligence = 1;
            Agility = 2;
            DamageRange = new int[] { 0, 1, 1, 2, 2, 2 };
            IsDead = false;
        }

        public int Heal()
        {
            // Steal Breath - if enemy is in critical condition, they die and Thief heals the amount left.
            return 3;
        }
    }
}
