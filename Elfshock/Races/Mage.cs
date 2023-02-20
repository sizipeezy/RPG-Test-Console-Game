namespace Elfshock.Races
{
    public class Mage : Race
    {
        private readonly double intelligence = 3;
        private readonly int range = 3;
        private readonly double strength = 2;
        private readonly double agility = 1;
        private readonly char symbol = '*';

        public Mage()
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
