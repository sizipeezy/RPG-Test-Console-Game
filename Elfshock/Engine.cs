namespace Elfshock
{
    using Elfshock.Models;
    using Elfshock.Races;
    using Elfshock.Services;

    public class Engine : IEngine
    {
        public void Start()
        {
            var option = Menu.MainMenu;
            Race hero = null;
            var gameService = new GameService();
            var heroService = new HeroService();
            var data = new ApplicationDbContext();


            try
            {
                while (option != Menu.Exit)
                {
                    switch (option)
                    {
                        case Menu.MainMenu:
                            Console.WriteLine("Main Menu");
                            Console.WriteLine("1. Character Select");
                            Console.WriteLine("2. Start Game");
                            Console.WriteLine("3. Exit");
                            int mainMenuChoice = int.Parse(Console.ReadLine());
                            if (mainMenuChoice == 1)
                            {
                                option = Menu.CharacterSelect;
                            }
                            else if (mainMenuChoice == 2)
                            {
                                option = Menu.InGame;
                            }
                            else if (mainMenuChoice == 3)
                            {
                                option = Menu.Exit;
                            }
                            break;

                        case Menu.CharacterSelect:
                            Console.WriteLine("Character Select");
                            int choice = gameService.CharacterSelect();
                            hero = heroService.ReturnHero(choice);
                            option = Menu.MainMenu;
                            break;

                        case Menu.InGame:
                            Console.WriteLine("In Game");
                            InGame(hero, data);
                            option = Menu.MainMenu;
                            break;

                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void InGame(Race hero, ApplicationDbContext data)
        {
            var heroService = new HeroService();
            var monsterService = new MonsterService();
            var DbService = new DbService(data);

            Console.WriteLine("Welcome!");
            Console.WriteLine("Press any key to play.");
            Console.ReadKey(true);

            var monster = new Monster();
            monster.Setup();
            int remainingPoints = 3;
            int playerRow = 1;
            int playerCol = 1;
            int monsterRow = monster.Row;
            int monsterCol = monster.Col;

            Console.Write("Would you like to buff up your stats before starting? (Limit: 3 points total)");
            Console.Write("Response (Y/N): ");

            if (Console.ReadLine()?.ToLower() == "y")
            {
                heroService.BuffStats(hero, remainingPoints);
            }

            //adding the hero to the db
            DbService.AddHero(hero);

            Console.Clear();

            var matrix = InitiliazeMatrix();

            while (hero.Health > 0)
            {
                Console.WriteLine($"Health: {hero.Health}   Mana: {hero.Mana}");
                Console.WriteLine();

                PrintMatrix(playerRow, playerCol, monsterRow, monsterCol, hero.FieldSymbol, monster.FieldSymbol, matrix);


                Console.WriteLine();
                Console.WriteLine("Options:");

                if (CanMove(playerRow, playerCol, matrix))
                {
                    Console.WriteLine("1- Move");
                }
                Console.WriteLine("2- Attack");
              

                Console.WriteLine();
                Console.Write("Enter option: ");
                var option = int.Parse(Console.ReadLine());

                if (option == 1)
                {
                    Console.WriteLine("Enter direction: ");
                    var direction = char.Parse(Console.ReadLine());

                    heroService.MoveHero(direction, hero.Range, matrix, ref playerRow, ref playerCol, hero);

                }
                else if (option == 2)
                {
                    var result = heroService.CheckForMonstersInRange(matrix, playerRow, playerCol, hero.Range);
                    if (result != null)
                    {
                        foreach (var pair in result)
                        {
                            string monsterName = pair.Key;
                            var monsterRowCol = pair.Value;

                            foreach (var box in monsterRowCol)
                            {
                                int enemyRow = box.Item1;
                                int enemyCol = box.Item2;

                                heroService.Attack(playerRow, playerCol, enemyRow, enemyCol, matrix, monster, hero);
                            }
                        }
                    }
                }

                //monster's behavior
                var enemyTarget = monsterService.GetMonsterRowAndCol(matrix);
                foreach (var enemy in enemyTarget)
                {
                    int enemyRow = enemy.Item1;
                    int enemyCol = enemy.Item2;

                    if (monsterService.CheckIfPlayerIsInRange(monsterRow, monsterCol, playerRow, playerCol))
                    {
                        monsterService.AttackPlayer(matrix, playerRow, playerCol, hero, monster);
                    }
                    else
                    {
                        monsterService.MoveMonsterToPlayer(matrix, ref monsterRow, ref monsterCol, playerRow, playerCol);
                    }
                }
            }
        }
        private static char[,] InitiliazeMatrix(int row = 10, int col = 10)
        {
            char[,] matrix = new char[row, col];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = '▒';
                }
            }
            return matrix;
        }

        //The method is to checking wheter player can move but it's not necessary the program checks everytime
        //if player is out of bounds it' will be set on the matrix's end
        public static bool CanMove(int playerRow, int playerCol, char[,] field)
        {
            int rows = field.GetLength(0);
            int cols = field.GetLength(1);

            if (playerRow < 0 || playerRow >= rows || playerCol < 0 || playerCol >= cols)
            {
                Console.WriteLine("Player is out of bounds.");
                return false;
            }

            return true;
        }

        //Method was used to check whether there is a monster within player's range
        public static bool CanAttack(int playerRow, int playerCol, int range, char[,] matrix, char monsterSymbol)
        {
            for (int i = playerRow - range; i <= playerRow + range; i++)
            {
                for (int j = playerCol - range; j <= playerCol + range; j++)
                {
                    if (i >= 0 && i < matrix.GetLength(0) && j >= 0 && j < matrix.GetLength(1))
                    {
                        if (matrix[i, j] == monsterSymbol && Math.Abs(playerRow - i) + Math.Abs(playerCol - j) <= range)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static void PrintMatrix(int playerRow, int playerCol, int monsterRow, int monsterCol, char heroSymbol, char monsterSymbol, char[,] matrix)
        {

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == playerRow && j == playerCol)
                    {
                        matrix[i, j] = heroSymbol;
                        Console.Write($"{heroSymbol}");
                    }
                    else if (i == monsterRow && j == monsterCol)
                    {
                        matrix[i, j] = monsterSymbol;
                        Console.Write($"{monsterSymbol}");
                    }
                    else
                    {
                        Console.Write(matrix[i, j]);
                    }
                }

                Console.WriteLine();
            }
        }
    }
}