namespace Elfshock.Races
{
    public class Archer : Race
    {
        private readonly double intelligence = 0;
        private readonly int range = 2;
        private readonly double strength = 2;
        private readonly double agility = 4;
        private readonly char symbol = '#';

        public Archer()
        {
            this.Range = range;
            this.FieldSymbol = symbol;
            this.Agility = agility;
            this.Range = range;
            this.Intelligence = intelligence;
            this.Strength = strength;

        }

    }
}
