using Heroes_of_House_Frimley.Classes;
using System;
using Xunit;

namespace HeroesOfHouseFrimleyTests
{
    public class CharacterTests
    {
        [Fact]
        public void Attack_ValidInput_ReturnsDamage()
        {
            // Arrange - create a new Character object.
            // Create an array of expected damage range output.
            var hero = new Character();
            hero.Strength = 1;
            var expectedRange = new int[] { 1, 1, 2, 2, 2, 3 };

            // Act - perform the Attack() method three times and store results.
            var attack1 = hero.Attack();

            // Assert - check all returned ints from Attack() are within expected range.
            Assert.InRange(attack1, expectedRange[0], expectedRange[expectedRange.Length - 1]);
        }

        [Fact]
        public void Run_ValidInput_ReturnsNewEnemyNumber()
        {
            // Arrange - create a Warrior.

            var totalEnemies = 3;
            Enemy enemy = new();
            var hero = new Warrior("Hero");

            // Act - invoke the Warrior's Run() method.

            var expected = hero.Run(totalEnemies, enemy);

            // Assert - there is one less enemy in the totalEnemies.
            Assert.True(totalEnemies - 1 == expected);
        }

        [Fact]
        public void Heal_SetHealPowerTo3_CurrentHPIncreasesBy3()
        {
            // Arrange
            Character character = new() { 
                CurrentHP = 10, 
                MaxHP = 20, 
                CurrentMP = 1, 
                HealPower = 3 };

            var expectedHP = 13;

            // Act
            character.Heal();

            // Assert - there is one less enemy in the totalEnemies.
            Assert.True(expectedHP == character.CurrentHP);
        }

        [Fact]
        public void ReceiveDamage_IntegerInput_CurrentHPIsReduced()
        {
            // Arrange
            Character character = new()
            {
                CurrentHP = 10,
            };

            var damage = 3;
            var expectedHP = 7;

            // Act
            character.ReceiveDamage(damage);

            // Assert - there is one less enemy in the totalEnemies.
            Assert.True(expectedHP == character.CurrentHP);
        }



    }
}
