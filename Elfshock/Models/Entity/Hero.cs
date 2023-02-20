namespace Elfshock.Models.Entity
{
    using System.ComponentModel.DataAnnotations;

    public class Hero
    {
        [Key]
        public int Id { get; set; }
        public double Strength { get; set; }
        public double Agility { get; set; }
        public double Intelligence { get; set; }
        public double Range { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
