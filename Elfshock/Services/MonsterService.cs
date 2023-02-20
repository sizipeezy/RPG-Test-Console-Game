namespace Elfshock.Services
{
    using Elfshock.Contracts;
    using Elfshock.Races;

    public class MonsterService : IMonsterService
    {
        public bool CheckIfPlayerIsInRange(int row1, int col1, int row2, int col2)
        {
            //Checks the difference between the Monster and Player <= 1;
            return Math.Abs(row1 - row2) <= 1 && Math.Abs(col1 - col2) <= 1;
        }

        public void AttackPlayer(char[,] matrix, int playerRow, int playerCol, Race hero, Monster monster)
        {
            Console.WriteLine($"Monster attacked the player and dealt {monster.Damage} damage!");
            hero.Health -= monster.Damage;

            if (hero.Health <= 0)
            {
                Console.WriteLine("Player has been defeated!");
                matrix[playerRow, playerCol] = '.';
                return;
            }
            Console.WriteLine($"Player health: {hero.Health}");


        }
        public List<(int, int)> GetMonsterRowAndCol(char[,] matrix)
        {
            List<(int, int)> monstersCoords = new List<(int, int)>();

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if (matrix[row, col] == 'M')
                    {
                        monstersCoords.Add((row, col));
                    }
                }
            }

            return monstersCoords;
        }

        public void MoveMonsterToPlayer(char[,] matrix, ref int monsterRow, ref int monsterCol, int playerRow, int playerCol)
        {
            int newRow = monsterRow;
            int newCol = monsterCol;
            // Calculate the distance to the player
            int rowDistance = Math.Abs(playerRow - monsterRow);
            int colDistance = Math.Abs(playerCol - monsterCol);

            //  Direction to move the monster
            if (rowDistance >= colDistance)
            {
                if (playerRow > monsterRow)
                {
                    newRow++;
                }
                else if (playerRow < monsterRow)
                {
                    newRow--;
                }
            }
            else
            {
                if (playerCol > monsterCol)
                {
                    newCol++;
                }
                else if (playerCol < monsterCol)
                {
                    newCol--;
                }
            }

            // Move the monster to the new position
            if (matrix[newRow, newCol] == '▒')
            {
                matrix[newRow, newCol] = 'M';
                matrix[monsterRow, monsterCol] = '▒';
            }

            monsterRow = newRow;
            monsterCol = newCol;
        }
    }
}
