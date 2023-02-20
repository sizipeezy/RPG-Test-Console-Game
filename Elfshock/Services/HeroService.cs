namespace Elfshock.Services
{
    using Elfshock.Contracts;
    using Elfshock.Races;
    using System;


    public class HeroService : IHeroService
    {
        public void Attack(int playerRow, int playerCol, int enemyRow, int enemyCol, char[,] matrix, Monster monster, Race hero)
        {
            var monsterHealth = monster.Health;
            var heroDamage = hero.Damage;

            monsterHealth -= heroDamage;
            if (monsterHealth <= 0)
            {
                Console.WriteLine($"The monster has died.");
                matrix[enemyRow, enemyCol] = 'D';
                Environment.Exit(1);

            }
            else
            {
                Console.WriteLine($"Target with remaining health {monsterHealth}");
                monster.Health = monsterHealth;
            }

            //var heroHealth = hero.Health;
            //heroHealth -= monster.Damage;
            //hero.Health = heroHealth;
            //Console.WriteLine($"You got attacked and your remaining health is {heroHealth}");
            //if (heroHealth <= 0)
            //{
            //    Console.WriteLine("Game over!");
            //    Environment.Exit(0);
            //}
        }
        public void MoveHero(char direction, int range, char[,] matrix, ref int playerRow, ref int playerCol, Race hero)
        {
            int newRow = playerRow;
            int newCol = playerCol;

            var lastRowIndex = matrix.GetLength(0) - 1;
            var lastColIndex = matrix.GetLength(1) - 1;

            switch (direction)
            {
                case 'W': // Move up
                    newRow = playerRow - range;
                    break;
                case 'S': // Move down
                    newRow = playerRow + range;
                    break;
                case 'D': // Move right
                    newCol = playerCol + range;
                    break;
                case 'A': // Move left
                    newCol = playerCol - range;
                    break;
                case 'E': // Move diagonally up & right
                    newRow = playerRow - range;
                    newCol = playerCol + range;
                    break;
                case 'X': // Move diagonally down & right
                    newRow = playerRow + range;
                    newCol = playerCol + range;
                    break;
                case 'Q': // Move diagonally up & left
                    newRow = playerRow - range;
                    newCol = playerCol - range;
                    break;
                case 'Z': // Move diagonally down & left
                    newRow = playerRow + range;
                    newCol = playerCol - range;
                    break;
                default:
                    Console.WriteLine("Invalid direction!");
                    break;
            }

            // Check if the new row and new col are within the matrix bounds
            // If the newRow and newCol are not within the matrix bounds they will be set to the end of the indexes.

            if (newRow < 0)
                newRow = 0;

            if (newRow > lastRowIndex)
                newRow = lastRowIndex;

            if (newCol < 0)
                newCol = 0;

            if (newCol > lastColIndex)
                newCol = lastColIndex;

            // Update the player's position
            matrix[playerRow, playerCol] = '.';
            matrix[newRow, newCol] = hero.FieldSymbol;
            playerRow = newRow;
            playerCol = newCol;

        }

        public Race ReturnHero(int choice)
        {
            var hero = new Race();
            switch (choice)
            {
                case 1:
                    hero = new Warrior();
                    Console.WriteLine($"Warrior has been called.");
                    break;
                case 2:
                    hero = new Archer();
                    Console.WriteLine($"Archer has been called.");
                    break;
                case 3:
                    hero = new Mage();
                    Console.WriteLine("Mage has been called.");
                    break;
                default:
                    break;
            }
            hero.Setup();
            return hero;
        }
        public void BuffStats(Race player, int points)
        {
            int maxPoints = points;
            int pointsRemaining = maxPoints;
            int strBonus = 0;
            int agiBonus = 0;
            int intBonus = 0;

            Console.WriteLine("Add bonus points to your stats (you have a maximum of 3 points to distribute):");

            while (pointsRemaining > 0)
            {
                Console.WriteLine($"Remaining points: {pointsRemaining}");
                Console.Write("Add to Strength: ");
                int.TryParse(Console.ReadLine(), out int strPoints);

                if (strPoints > pointsRemaining)
                {
                    Console.WriteLine($"You can only add up to {pointsRemaining} points.");
                    continue;
                }

                pointsRemaining -= strPoints;
                strBonus += strPoints;

                if (pointsRemaining == 0)
                {
                    break;
                }

                Console.WriteLine($"Remaining points: {pointsRemaining}");
                Console.Write("Add to Agility: ");
                int.TryParse(Console.ReadLine(), out int agiPoints);

                if (agiPoints > pointsRemaining)
                {
                    Console.WriteLine($"You can only add up to {pointsRemaining} points.");
                    continue;
                }

                pointsRemaining -= agiPoints;
                agiBonus += agiPoints;

                if (pointsRemaining == 0)
                {
                    break;
                }

                Console.WriteLine($"Remaining points: {pointsRemaining}");
                Console.Write("Add to Intelligence: ");
                int.TryParse(Console.ReadLine(), out int intPoints);

                if (intPoints > pointsRemaining)
                {
                    Console.WriteLine($"You can only add up to {pointsRemaining} points.");
                    continue;
                }

                pointsRemaining -= intPoints;
                intBonus += intPoints;
            }

            player.Strength += strBonus;
            player.Agility += agiBonus;
            player.Intelligence += intBonus;

            Console.WriteLine($"You added {strBonus} points to Strength, {agiBonus} points to Agility, and {intBonus} points to Intelligence.");
        }

        public Dictionary<string, List<(int, int)>> CheckForMonstersInRange(char[,] field, int playerRow, int playerCol, int range)
        {
            var monsterLocations = new Dictionary<string, List<(int, int)>>();
            int numRows = field.GetLength(0);
            int numCols = field.GetLength(1);

            int startRow = Math.Max(0, playerRow - range);
            int endRow = Math.Min(numRows - 1, playerRow + range);

            int startCol = Math.Max(0, playerCol - range);
            int endCol = Math.Min(numCols - 1, playerCol + range);

            for (int row = startRow; row <= endRow; row++)
            {
                for (int col = startCol; col <= endCol; col++)
                {
                    // If the cell contains a monster, add it to the dictionary
                    if (field[row, col] == 'M')
                    {
                        string monsterName = "monster" + monsterLocations.Count + 1;
                        if (!monsterLocations.ContainsKey(monsterName))
                        {
                            monsterLocations.Add(monsterName, new List<(int, int)>());
                        }
                        monsterLocations[monsterName].Add((row, col));
                    }
                }
            }

            if (monsterLocations.Count == 0)
            {
                Console.WriteLine("No available monsters in range.");
            }

            return monsterLocations;
        }
    }
}
