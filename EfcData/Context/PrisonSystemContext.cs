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
    public DbSet<DaysOfWeek> DaysOfWeek { get; set; } = null!;
        
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { 
        optionsBuilder.UseSqlite(@"Data Source = C:\Users\Emmi\Skrivebord\OneDrive - ViaUC\Via College University - S3\SEP3\PrisonSystemDataAccess\EfcData\PrisonSystem.db");
    } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<Prisoner>().HasKey(prisoner => prisoner.Id);
        modelBuilder.Entity<WorkShift>().HasKey(shift => shift.Id);
        modelBuilder.Entity<Sector>().HasKey(sector => sector.Id);
        modelBuilder.Entity<DaysOfWeek>().HasKey(d => d.Id);

        modelBuilder.Entity<WorkShift>()
            .HasMany<DaysOfWeek>(ws => ws.DaysOfWeeks)
            .WithMany(ws => ws.WorkShifts);

    }
}