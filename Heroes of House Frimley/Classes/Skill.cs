using Heroes_of_House_Frimley.Classes.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_of_House_Frimley.Classes
{
    public class Skill
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Action { get; set; }
        public int Cost { get; set; }
        public int[] DamageRange { get; set; }
        public delegate string AddEff(ref Enemy enemy);
        public AddEff AddedEffect { get; set; }


        public static string Stun(ref Enemy enemy)
        {
            var rnd = new Random();
            var success = rnd.Next(2);
            if (success == 1)
            {
                enemy.IsStunned = true;
                return $"\nThe {enemy.Name} is Stunned!";
            }
            else
            {
                enemy.IsStunned = false;
                return $"\nThe {enemy.Name} resists Stun!";
            }
        }

        

    }


}
