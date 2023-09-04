using ExerciseTimer.Models;
using ExerciseTimer.Data;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTimer.Services;

public class SetRecordService
{
  private readonly ExerciseContext _context;

  public SetRecordService(ExerciseContext context)
  {
    _context = context;
  }

  public IEnumerable<SetRecord> GetAll()
  {
    return _context.SetRecords
      .Include(sr => sr.Sets)
      .AsNoTracking()
      .ToList();
  }

  public IEnumerable<SetRecord> GetAllByExercise(int exerciseId)
  {
    return _context.SetRecords
      .Where(sr => sr.ExerciseId == exerciseId)
      .Include(sr => sr.Sets)
      .AsNoTracking()
      .ToList();
  }

  public SetRecord? GetById(int id)
  {
    return _context.SetRecords
      .Include(sr => sr.Exercise)
      .Include(sr => sr.Sets)
      .AsNoTracking()
      .SingleOrDefault(p => p.Id == id);
  }

  public SetRecord Create(SetRecord newSetRecord)
  {
    _context.SetRecords.Add(newSetRecord);
    _context.SaveChanges();

    return newSetRecord;
  }
}