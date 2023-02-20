namespace Elfshock.Races
{
    public class Race
    {
        public double Strength { get;  set; }
        public double Agility { get;  set; }
        public double Intelligence { get;  set; }
        public int Range { get;  set; }
        public double Health { get;  set; }
        public double Mana { get;  set; }
        public double Damage { get;  set; }
        public char FieldSymbol { get;  set; }

        public void Setup()
        {
            this.Health = this.Strength * 5;
            this.Mana = this.Intelligence * 3;
            this.Damage = this.Agility * 2;
        }
    }
}
