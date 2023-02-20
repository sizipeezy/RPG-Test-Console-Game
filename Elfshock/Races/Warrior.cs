namespace Elfshock.Races
{
    public class Warrior : Race
    {
        private readonly double intelligence = 0;
        private readonly int range = 2;
        private readonly double strength = 3;
        private readonly double agility = 3;
        private readonly char symbol = '@';

        public Warrior()
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
