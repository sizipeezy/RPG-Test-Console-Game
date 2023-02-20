namespace Elfshock.Models
{
    using Elfshock.Models.Entity;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        public DbSet<Hero> Heroes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=ElfShock;Trusted_Connection=True;");
            }
         
            base.OnConfiguring(optionsBuilder);
        }
    }
}
