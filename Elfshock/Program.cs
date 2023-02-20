using Elfshock;
using Elfshock.Models;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        using (var context = new ApplicationDbContext(optionsBuilder.Options))
        {
            context.Database.EnsureCreated();
        }

        var engine = new Engine();

        engine.Start();

    }
}