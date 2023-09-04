using ExerciseTimer.Models;
using ExerciseTimer.Data;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTimer.Services;

public class ExerciseService
{
  private readonly ExerciseContext _context;

  public ExerciseService(ExerciseContext context)
  {
    _context = context;
  }

  public IEnumerable<Exercise> GetAll()
  {
    return _context.Exercises
      // .Include(exer => exer.SetRecords)
      //   .ThenInclude(sr => sr.Sets)
      .AsNoTracking()
      .OrderBy(exercise => exercise.Name)
      .ToList();
  }

  public Exercise? GetById(int id)
  {
    return _context.Exercises
      .Include(exer => exer.SetRecords)
        .ThenInclude(sr => sr.Sets)
      .AsNoTracking()
      .SingleOrDefault(p => p.Id == id);
  }

  public Exercise? GetByIdSimple(int id)
  {
    return _context.Exercises
      .AsNoTracking()
      .SingleOrDefault(p => p.Id == id);
  }

  public Exercise Create(Exercise newExercise)
  {
    _context.Exercises.Add(newExercise);
    _context.SaveChanges();

    return newExercise;
  }
}