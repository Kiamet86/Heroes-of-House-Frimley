using Heroes_of_House_Frimley.Classes.UI;
using Heroes_of_House_Frimley.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_of_House_Frimley.Classes
{
    public class Character : IHealable
    {
        public string Name { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public int MaxMP { get; set; }
        public int CurrentMP { get; set; }
        public int Strength { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        
        public int HealPower { get; set; }
        public List<Skill> Skills { get; set; }
        public string AttackDescription
        {
            get { return attackDescription; }
            set { attackDescription = value; }
        }

        public bool IsDead { get; set; }
        public bool IsStunned { get; set; }

        protected static readonly Random rnd = new();
        protected int[] DamageRange = new int[] { 0, 0, 0, 1, 1, 2 };
        protected string attackDescription = "attacks!";

        public Character()
        {

        }

        /// <summary>
        /// Returns a random integer calculated from an int[] + the Character.Strength property
        /// </summary>
        /// <returns></returns>
        public virtual int Attack() => (DamageRange[rnd.Next(DamageRange.Length)] + Strength);

        /// <summary>
        /// Return a random integer based on a parameter range.
        /// </summary>
        /// <param name="damageRange"></param>
        /// <returns></returns>
        public int Attack(int[] damageRange) => damageRange[rnd.Next(damageRange.Length)];
        
        /// <summary>
        /// Return a boolean with a 50/50 chance of success.
        /// </summary>
        /// <returns></returns>
        public bool RunSuccessful()
        {
            // TODO: Thief run always succeeds.
          
            var IsHitWhileRunning = (rnd.Next(2) == 0);
            return IsHitWhileRunning;
        }

        public virtual void Heal()
        {
            var healMPCost = 1;
            if (IsMPEnough(healMPCost))
            {
                LoseMP(healMPCost);
                Display.PlayerActionGreen($"{Name} heals {HealPower} HP!");
                GainHP(HealPower);
            }
           
        }

        public string ShowSkills()
        {
            StringBuilder sb = new();
            // iterates over List<Skill>, showing Name, Description and MP Cost on seperate lines.
            if (Skills != null)
            {
                foreach (var skill in Skills) {
                    sb.Append($"{skill.Name} ({skill.Cost} MP): {skill.Description}\n");
                }

                return sb.ToString();
            }

            return "No skills learned.";
        }

        public virtual Skill SelectSkill(string skillChoice)
        {
            return new Skill();
        }

        private void GainHP(int healing)
        {
            CurrentHP = CurrentHP + healing >= MaxHP ? MaxHP : CurrentHP + healing;
        }

        protected void GainMP(int healing)
        {
            CurrentMP = CurrentMP + healing >= MaxMP ? MaxMP : CurrentMP + healing;
        }

        private void LoseHP(int damage)
        {
            // Changes HP according to damage received, and returns remaining HP.
            CurrentHP -= damage;
            if (CurrentHP <= 0) IsDead = true;
        }

        protected void LoseMP(int mpLoss)
        {
            CurrentMP = CurrentMP - mpLoss <= 0 ? 0 : CurrentMP - mpLoss;
        }

        public int GetHP()
        {
            return CurrentHP;
        }

        public int GetMP()
        {
            return CurrentMP;
        }

        /// <summary>
        /// Report damage done by an opponent, then use LoseHP() method.
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="attackerName"></param>
        public void TakeDamage(int damage)
        {
            Console.WriteLine($"{Name} takes {damage} damage!");
            System.Threading.Thread.Sleep(400);
            LoseHP(damage);
        }


        protected bool IsMPEnough(int spellMP)
        {
            if (CurrentMP - spellMP < 0)
            {
                Console.WriteLine("Not enough MP!");
                return false;
            }

            else return true;
        }
    }
}
