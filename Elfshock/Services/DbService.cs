namespace Elfshock.Services
{
    using Elfshock.Models;
    using Elfshock.Models.Entity;
    using Elfshock.Races;

    public class DbService
    {
        private ApplicationDbContext dbContext;

        public DbService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void AddHero(Race hero)
        {
            var newHero = new Hero()
            {
                Agility = hero.Agility,
                Intelligence = hero.Intelligence,
                Range = hero.Range,
                Strength = hero.Strength,
                CreatedAt = DateTime.UtcNow.Date
            };

            dbContext.Heroes.Add(newHero);
            dbContext.SaveChanges();
        }
    }
}
