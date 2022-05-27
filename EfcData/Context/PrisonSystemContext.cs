using Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EfcData.Context;

public class PrisonSystemContext: DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Guard> Guards { get; set; } = null!;
    public DbSet<Prisoner> Prisoners { get; set; } = null!;
    public DbSet<WorkShift> WorkShifts { get; set; } = null!;
    public DbSet<Sector> Sectors { get; set; } = null!;
    public DbSet<Visit> Visits { get; set; } = null!;
    public DbSet<Note> Notes { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=C:\Programming\SEP3\PrisonSystemDataAccess\EfcData\PrisonSystem.db");
    } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(user => user.Id);
        modelBuilder.Entity<Prisoner>().Property(p => p.EntryDate).HasConversion(v => v,
            v => new DateTime(v.Ticks, DateTimeKind.Utc));
        modelBuilder.Entity<Prisoner>().Property(p => p.ReleaseDate).HasConversion(v => v,
            v => new DateTime(v.Ticks, DateTimeKind.Utc));
        modelBuilder.Entity<Prisoner>().HasKey(prisoner => prisoner.Id);
        modelBuilder.Entity<Prisoner>().HasIndex(p=>p.Ssn).IsUnique();
        modelBuilder.Entity<WorkShift>().HasKey(shift => shift.Id);
        modelBuilder.Entity<Sector>().HasKey(sector => sector.Id);
        modelBuilder.Entity<Visit>().Property(e => e.VisitDate).HasConversion(v => v,
            v => new DateTime(v!.Ticks, DateTimeKind.Utc));
        modelBuilder.Entity<Visit>().HasKey(visit => visit.Id);
        modelBuilder.Entity<Note>().HasKey(note => note.Id);
    }
}