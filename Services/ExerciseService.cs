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

  public IEnumerable<Exercise> GetAll(string uid)
  {
    return _context.Exercises
      .Include(ex => ex.UserExercises)
      .AsNoTracking()
      .Where(ex => ex.UserExercises.Any(ue => ue.Uid == uid))
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

  public Exercise Create(Exercise newExercise, string uid)
  {
    UserExercise userExercise;
    var exerciseRecord = _context.Exercises.FirstOrDefault(ex => ex.Name == newExercise.Name);
    if (exerciseRecord == null) {
      _context.Exercises.Add(newExercise);
      _context.SaveChanges(); // save to generate Id

      userExercise = new UserExercise(uid) {
        ExerciseId = newExercise.Id
      };
    } else {
      userExercise = new UserExercise(uid) {
        ExerciseId = exerciseRecord.Id
      };
    }

    _context.UserExercises.Add(userExercise);
    _context.SaveChanges();

    return newExercise;
  }
}