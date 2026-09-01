using Microsoft.EntityFrameworkCore;
using WaracleTask.Data.Models;

public class WaracleTaskContext(DbContextOptions<WaracleTaskContext> options) : DbContext(options)
{
    public DbSet<Hotel> Hotel { get; set; } = default!;
    public DbSet<Booking> Booking { get; set; } = default!;
    public DbSet<Room> Room { get; set; } = default!;
}
