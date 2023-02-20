namespace Elfshock.Contracts
{
    using Elfshock.Races;
    public interface IMonsterService
    {
        public bool CheckIfPlayerIsInRange(int row1, int col1, int row2, int col2);
        public List<(int, int)> GetMonsterRowAndCol(char[,] matrix);

        public void AttackPlayer(char[,] matrix, int playerRow, int playerCol, Race hero, Monster monster);

        public void MoveMonsterToPlayer(char[,] matrix, ref int monsterRow, ref int monsterCol, int playerRow, int playerCol);
    }
}
