using app_server.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace app_server.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    
    public DbSet<User> Users { get; set; }
    public DbSet<Space> Spaces { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Space>().ToTable("spaces");
        modelBuilder.Entity<Reservation>().ToTable("reservations");
        modelBuilder.Entity<Reservation>().HasOne(r => r.user)
            .WithMany(u => u.reservations)
            .HasForeignKey(r => r.userId);
        modelBuilder.Entity<Reservation>().HasOne(r => r.space)
            .WithMany(s => s.reservations)
            .HasForeignKey(r => r.spaceId);
    }
}