using FishDream.Sources;
using Microsoft.EntityFrameworkCore;

public class CookingContext: DbContext
{
    public DbSet<Recipe> Entries { get; set; }
    public CookingContext(DbContextOptions<CookingContext> optionsBuilder) : base(optionsBuilder)
    {
        Database.EnsureCreated();
    }
}