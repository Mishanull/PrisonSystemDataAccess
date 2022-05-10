using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcData.Context;

public class PrisonSystemContext: DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Guard> Guards { get; set; } = null!;
    public DbSet<Prisoner> Prisoners { get; set; } = null!;
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseSqlite(@"Data Source =C:\Programming\SEP3\PrisonSystemDataAccess\EfcData\PrisonSystem.db");
    } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<Guard>().HasKey(guard => guard.Id);
        modelBuilder.Entity<Prisoner>().HasKey(prisoner => prisoner.Id);
    }
}