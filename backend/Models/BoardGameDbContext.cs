using Microsoft.EntityFrameworkCore;
namespace RankingApp.Models;


public class BoardGameDbContext : DbContext
{
    public DbSet<BoardGameModel> BoardGames { get; set; }

    public BoardGameDbContext(DbContextOptions<BoardGameDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoardGameModel>().HasKey(bg => bg.Id);
    }
}
