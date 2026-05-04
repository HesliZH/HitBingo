using Microsoft.EntityFrameworkCore;
using BingoAPI.Models;

namespace BingoAPI.DAL;

public class BingoDbContext : DbContext
{
    public BingoDbContext(DbContextOptions<BingoDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<GameRoom> GameRooms { get; set; }
    public DbSet<DrawnNumber> DrawnNumbers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();
        });

        modelBuilder.Entity<GameRoom>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
            entity.HasMany(e => e.DrawnNumbers)
                  .WithOne(d => d.GameRoom)
                  .HasForeignKey(d => d.GameRoomId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DrawnNumber>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Number).IsRequired();
            entity.Property(e => e.DrawnAt).HasDefaultValueSql("NOW()");
        });
    }
}