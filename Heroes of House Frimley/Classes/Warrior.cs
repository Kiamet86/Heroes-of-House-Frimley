using Heroes_of_House_Frimley.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_of_House_Frimley.Classes
{
    public class Warrior : Character, IHealable
    {
        private const int STRENGTH_BONUS = 1;

        public Warrior(string name)
        {
            Name = name;
            MaxHP = 8;
            CurrentHP = MaxHP;
            MaxMP = 5;
            CurrentMP = MaxMP;
            
            Strength = 2;
            Intelligence = 1;
            Agility = 1;
            DamageRange = new int[] { 0, 0, 1, 1, 2, 3 };
            HealPower = 3;
            AttackDescription = "slashes wildly!";
            IsDead = false;
        }

        public override void Heal()
        {
            // TODO Rage - Heals half his HP and increases Strength of next Attack().

            base.Heal();


            Console.WriteLine("Your warrior instincts are honed as you enter a Second Wind!");
        }


    }
}
