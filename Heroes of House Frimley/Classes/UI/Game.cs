using Heroes_of_House_Frimley.Classes.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes_of_House_Frimley.Classes
{
    public class Game
    {
        private string playerName;
        public static Character Player { get; set; }
        public static Enemy Enemy { get; set; }
        private int enemiesRemaining;
        private int totalScore = 0;

        public Game()
        {
            
        }

        

        

        /// <summary>
        /// Processes player commands while in combat.
        /// </summary>
        /// <param name="enemy"></param>
        public void Quest()
        {
            
            // Player inputs name and chosen difficulty.
            Display.PlayerChoiceYellow("What is your name, Hero?");
            SetPlayerName();

            Display.PlayerChoiceYellow("How many enemies do you want to fight?");
            SetDifficulty();

            ChooseHero();
            totalScore = 0;

            while ((enemiesRemaining > 0) && (!Player.IsDead))
            {
                Console.WriteLine($"Your current score is {totalScore}.");
            
                EnemyApproaches();

                FightOrRun:
                Display.PlayerChoiceYellow("Do you wish to (F)ight, or (R)un to live another day?");
                var fightChoice = Console.ReadLine().ToUpper();

                switch (fightChoice) 
                {
                    case "F":
                        Fight();
                        break;

                    case "R":
                        Run();
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        goto FightOrRun;
                }                       
            }

            Reset();
        }

        private void Reset()
        {
            if(!Player.IsDead) Display.PlayerSuccessWhite("You have beaten all the enemies!");
            Console.WriteLine($"Your final score is {totalScore}. Play again? Y/N");
            var playAgain = Console.ReadLine().ToUpper();
            if (playAgain == "Y") { 
                
                Quest(); }
        }

        private void SetDifficulty()
        {
            try
            {
                int enemies = Convert.ToInt32(Console.ReadLine());
                enemiesRemaining = enemies;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input - enter a whole integer.");
                SetDifficulty();
            }
            
        }

        private void SetPlayerName()
        {
            string name = Console.ReadLine();
            playerName = name;
        }

        private void ChooseHero()
        {
            // TODO - make Validating Hero Choices a seperate method
            var heroChoices = new string[] { "W", "M", "T" };
            Display.PlayerChoiceYellow($"{playerName}, be you a (W)arrior, (M)age, or (T)hief?");
            var input = Console.ReadLine().ToUpper();

            if (heroChoices.Contains(input)) SetHeroClass(input);

            else
            {
                Console.WriteLine("Invalid option.");
                ChooseHero();
            }

        }

        private void EnemyApproaches()
        {
            // Generates a new enemy object and reports it's Strength to the player.
            // Magic number - 2 for enemy strength being powerful.
            Enemy = new Enemy();
            Display.EnemyActionDarkYellow($"You see a {Enemy.Appearance} {Enemy.Name} approaching you!");
        }

        private string ShowHP(int currentHP)
        {
            StringBuilder sb = new StringBuilder();
            var heart = (char)3;
            return sb.Append(heart, currentHP).ToString();
        }

        private string ShowMP(int currentHP)
        {
            StringBuilder sb = new StringBuilder();
            var heart = (char)4;
            return sb.Append(heart, currentHP).ToString();
        }

        /// <summary>
        /// Runs a loop while both Player and Enemy are alive.
        /// </summary>
        private void Fight()
        {
            while ((!Player.IsDead) && (!Enemy.IsDead))
            {
                CombatSummary();
                ClearEffects(Enemy);
                RetryInput:
                Display.PlayerChoiceYellow("(A)ttack, (H)eal, or Use (S)kill?");               
                var input = Console.ReadLine().ToUpper();

                switch (input)
                {
                    case "A":
                        CombatRound(Player, Enemy);
                        if (!IsEnemyDead(Enemy))
                        {
                            if (!IsEnemyStunned(Enemy))
                            {
                                CombatRound(Enemy, Player);
                                IsPlayerDead(Player, Enemy);
                            }
                        }
                        break;

                    case "H":
                        Player.Heal();
                        break;

                    case "S":
                        // Requests a list of skills from the Player Character and prints them.
                        Display.PlayerChoiceYellow(Player.ShowSkills());
                        if (Player.Skills?.Any() != true) goto RetryInput;

                        CombatRoundSkill(Player, Enemy);
                        if (!IsEnemyDead(Enemy))
                        {
                            if (!IsEnemyStunned(Enemy))
                            {
                                CombatRound(Enemy, Player);
                                IsPlayerDead(Player, Enemy);
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option.");
                        goto RetryInput;
                }

             
            }
        }

        /// <summary>
        /// Reset any effects inflicted on an enemy last turn.
        /// </summary>
        private void ClearEffects(Enemy enemy)
        {
            enemy.IsStunned = false;
        }



        /// <summary>
        /// Set the hero's class according to player input.
        /// </summary>
        /// <param name="input"></param>
        private void SetHeroClass(string input)
        {
            switch (input)
            {
                case "W":
                    Player = new Warrior(playerName);
                    Display.PlayerSuccessWhite("You are a mighty Warrior!");
                    break;

                case "M":
                    Player = new Mage(playerName);
                    Display.PlayerSuccessWhite("You are a wise Mage!");
                    break;

                case "T":
                    Player = new Thief(playerName);
                    Display.PlayerSuccessWhite("You are a crafty Thief!");
                    break;
            }

        }

        private void Run()
        {
            var runDamage = 1;
            Display.PlayerActionGreen($"You run away from the {Enemy.Name}!");

            if (Player.RunSuccessful())
            {
                Display.DangerRedText($"It attacks you as you flee!");
                Player.TakeDamage(runDamage);
            }

            enemiesRemaining -= 1;
        }

        private void CombatSummary()
        {
            // Display current HP of enemy and player.
            // TODO: Change console colour when close to death?
            Display.EnemyActionDarkYellow($"{Enemy.Name} - {ShowHP(Enemy.CurrentHP)}");

            Console.WriteLine($"{Player.Name}");
            Display.HealthRed($"HP - {ShowHP(Player.CurrentHP)} "); 
            Display.MagicBlue($"MP - {ShowMP(Player.CurrentMP)}");
        }

        /// <summary>
        /// Takes damage from a Character, and sends it to another Character.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defender"></param>
        private void CombatRound(Character attacker, Character defender)
        {
            // Request damage from Character object that is attacking
            var damage = attacker.Attack();

            // Create string describing attacker and damage output
            var attackString = $"{attacker.Name} {attacker.AttackDescription}";
       
            // Determine if attacker parameter is a Character or Enemy Type
            var attackerType = attacker.GetType();

            // Change display colour depending on the attacker Type, then print string
            if (attackerType == typeof(Enemy)) Display.DangerRedText(attackString);
            else Display.PlayerActionGreen(attackString);

            // Send damage to Character object that is defending
            defender.TakeDamage(damage);

        }

        /// <summary>
        /// Takes a Skill object and applies its damage and effects to an Enemy.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="enemy"></param>
        private void CombatRoundSkill(Character player, Enemy enemy)
        {     
            var rnd = new Random();

            // Get input from player for which skill to use.
            var skillChoice = Console.ReadLine().ToUpper();

            // Send player's skill choice to the Player Character, which returns a Skill object.
            var skill = player.SelectSkill(skillChoice);

            // Calculate damage from Skill and send it to Enemy.
            var skillDamage = skill.DamageRange[rnd.Next(skill.DamageRange.Length)];
            Display.PlayerActionGreen($"{skill.Action}");
            enemy.TakeDamage(skillDamage);

            if (skill.AddedEffect != null)
            {
                Console.WriteLine(skill.AddedEffect(ref enemy));
            }
        }

        private bool IsEnemyDead(Enemy enemy)
        {
            if (enemy.IsDead)
            {
                Display.PlayerSuccessWhite($"You have slain the {enemy.Name}!");
                enemiesRemaining -= 1;
                totalScore += enemy.EnemyScore;
                return true;
            }

            else return false;
        }

        private bool IsEnemyStunned(Enemy enemy)
        {
            if (enemy.IsStunned)
            {
                Display.PlayerSuccessWhite($"The {enemy.Name} can't move!");
                return true;
            }

            else return false;
        }

        private bool IsPlayerDead(Character player, Enemy enemy)
        {
            if (Player.IsDead == true)
            {
                Display.DangerRedText($"You have been killed by the {enemy.Name}!");
                return true;
            }

            else return false;
        }


    }
}
