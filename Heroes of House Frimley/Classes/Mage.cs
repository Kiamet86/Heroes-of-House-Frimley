using Heroes_of_House_Frimley.Classes.UI;
using Heroes_of_House_Frimley.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_of_House_Frimley.Classes
{
    class Mage : Character, IHealable
    {
        public Mage(string name)
        {
            Name = name;
            MaxHP = 6;
            CurrentHP = MaxHP;
            MaxMP = 8;
            CurrentMP = MaxMP;
            Strength = 1;
            Intelligence = 2;
            Agility = 1;
            DamageRange = new int[] { 0, 0, 0, 0, 1, 2 };
            AttackDescription = "swings their staff!";
            IsDead = false;
            HealPower = 4;
            Skills = new List<Skill> { Fireblast, IceSpear, LightningBall };
        }

        private Skill Fireblast = new Skill
        {
            Name = "(F)ireblast",
            Description = "Does random damage between 2 and 5.",
            Action = "Flames erupt from your hands! ",
            Cost = 2,
            DamageRange = new int[] { 2, 3, 3, 4, 4, 5 }
        };

        private Skill IceSpear = new Skill
        {
            Name = "(I)ce Spear",
            Description = "Always does 5 damage.",
            Action = "A sharp icicle bursts forth! ",
            Cost = 3,
            DamageRange = new int[] { 5 }
        };

        private Skill LightningBall = new Skill
        {
            Name = "(L)ightning Ball",
            Description = "Does random damage between 1 and 3, with a chance to stun the enemy.",
            Action = "A ball of lightning arcs toward the enemy! ",
            Cost = 1,
            DamageRange = new int[] { 1, 2, 2, 2, 3 },
            AddedEffect = Skill.Stun
        };

        override public void Heal()
        {
            // Shield - Mage can heal past his max health.
            base.Heal();
            Console.WriteLine("Mage Heals!");
        }

        /// <summary>
        /// The Mage absorbs a random amount of MP with his attack.
        /// </summary>
        /// <returns></returns>
        public override int Attack()
        {
            var damage = base.Attack();
            var randomMP = rnd.Next(1, 3);
            Display.PlayerActionGreen($"{Name}'s staff absorbs {randomMP} MP!");
            GainMP(randomMP);
            return damage;
        }

        override public Skill SelectSkill(string skillChoice)
        {
            var failedSkill = new Skill();
            switch (skillChoice)
            {
                case "F":
                    if (IsMPEnough(Fireblast.Cost))
                    {
                        LoseMP(Fireblast.Cost);
                        return Fireblast;
                    }
                    else return failedSkill;

                case "I":
                    if (IsMPEnough(IceSpear.Cost))
                    {
                        LoseMP(IceSpear.Cost);
                        return IceSpear;
                    }
                    else return failedSkill;

                case "L":
                    if (IsMPEnough(LightningBall.Cost))
                    {
                        LoseMP(LightningBall.Cost);
                        return LightningBall;
                    }
                    else return failedSkill;

                default:
                    Console.WriteLine("That skill does not exist.");
                    return failedSkill;
            }

        }

        
    }
}
