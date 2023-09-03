using Microsoft.EntityFrameworkCore;
using ExerciseTimer.Models;

namespace ExerciseTimer.Data;

public class ExerciseContext : DbContext
{
  protected readonly IConfiguration _configuration;

  public ExerciseContext(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public DbSet<Exercise> Exercises => Set<Exercise>();
  public DbSet<SetRecord> SetRecords => Set<SetRecord>();
  public DbSet<Set> Sets => Set<Set>();

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseNpgsql(_configuration.GetConnectionString("ExercisePsql"));
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<SetRecord>()
      .HasMany(sr => sr.Sets)
      .WithOne(set => set.SetRecord)
      .HasForeignKey(set => set.SetRecordId);

    modelBuilder.Entity<Exercise>()
      .HasMany(srs => srs.SetRecords)
      .WithOne(ex => ex.Exercise)
      .HasForeignKey(ex => ex.ExerciseId);
  }
}