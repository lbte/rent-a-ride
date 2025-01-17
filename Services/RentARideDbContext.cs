using Microsoft.EntityFrameworkCore;
using RentARide.Models;

namespace RentARide.Services;

public class RentARideDbContext(
    DbContextOptions<RentARideDbContext> options) 
    : DbContext(options)
{
    public DbSet<Car> Cars { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Car>();
        modelBuilder.Entity<Booking>();
    }
}