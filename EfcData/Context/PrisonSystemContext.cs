using Entities;
using Microsoft.EntityFrameworkCore;

namespace EfcData.Context;

public class PrisonSystemContext: DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Guard> Guards { get; set; } = null!;
    public DbSet<Prisoner> Prisoners { get; set; } = null!;
    public DbSet<WorkShift> WorkShifts { get; set; } = null!;
    public DbSet<Sector> Sectors { get; set; } = null!;
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseSqlite(@"Data Source = C:\Users\asus\School\Ja-College\VIA\OneDrive - ViaUC\3rd_semester-S22\PrisonSystem\PrisonSystemDataAccess\EfcData\PrisonSystem.db");
    } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<Prisoner>().HasKey(prisoner => prisoner.Id);
        modelBuilder.Entity<WorkShift>().HasKey(shift => shift.Id);
        modelBuilder.Entity<Sector>().HasKey(sector => sector.Id);

    }
}