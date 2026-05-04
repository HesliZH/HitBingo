using Microsoft.EntityFrameworkCore;
using BingoAPI.Models;

namespace BingoAPI.DAL;

public class BingoDbContext : DbContext
{
    public BingoDbContext(DbContextOptions<BingoDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<DrawnNumber> DrawnNumbers { get; set; }
    public DbSet<Sala> Salas { get; set; }
    public DbSet<SalaCartela> SalasCartelas { get; set; }

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

        modelBuilder.Entity<DrawnNumber>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Number).IsRequired();
            entity.Property(e => e.DrawnAt).HasDefaultValueSql("NOW()");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.ToTable("SALAS");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Uuid).IsRequired();
            entity.HasIndex(e => e.Uuid).IsUnique();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
            entity.HasMany(e => e.SalasCartelas)
                  .WithOne(c => c.Sala)
                  .HasForeignKey(c => c.SalaId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(e => e.DrawnNumbers)
                  .WithOne(d => d.Sala)
                  .HasForeignKey(d => d.SalaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SalaCartela>(entity =>
        {
            entity.ToTable("SALAS_CARTELAS");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SalaUuid).IsRequired();
            entity.Property(e => e.PlayerUuid).IsRequired();
            entity.Property(e => e.CardJson).IsRequired();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
        });
    }
}