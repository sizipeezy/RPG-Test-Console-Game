namespace Elfshock.Races
{
    public class Monster : Race
    {
        private readonly int range = 1;
        private readonly char symbol = 'M';
        public Monster()
        {
            var rand = new Random();

            Range = range;
            FieldSymbol = symbol;
            Agility = rand.Next(1, 4);
            Intelligence = rand.Next(1, 4);
            Strength = rand.Next(1, 4);

        }

        public int Row { get; set; } = new Random().Next(10);

        public int Col { get; set; } = new Random().Next(10);

    }
}
