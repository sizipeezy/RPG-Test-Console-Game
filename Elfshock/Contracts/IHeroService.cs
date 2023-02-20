namespace Elfshock.Contracts
{
    using Elfshock.Races;

    public interface IHeroService
    {
        public void BuffStats(Race player, int points);

        public Race ReturnHero(int choice);

        public void MoveHero(char direction, int range, char[,] matrix, ref int playerRow, ref int playerCol, Race hero);

        public void Attack(int playerRow, int playerCol, int enemyRow, int enemyCol, char[,] matrix, Monster monster, Race hero);

        public Dictionary<string, List<(int, int)>> CheckForMonstersInRange(char[,] field, int playerRow, int playerCol, int range);
    }
}
