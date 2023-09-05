using ExerciseTimer.Models;
using ExerciseTimer.Data;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTimer.Services;

public class UserExerciseService
{
  private readonly ExerciseContext _context;

  public UserExerciseService(ExerciseContext context)
  {
    _context = context;
  }

  public void CheckAndCreateDefaultExercises(string uid) {
    bool hasExercises = _context.UserExercises.Any(ue => ue.Uid == uid);

    if (!hasExercises) {
      string[] defaultExercises = { "Push ups", "Pull ups", "Dips", "Plank", "Handstand" };

      foreach (string exercise in defaultExercises) {
        var exerciseRecord = _context.Exercises.SingleOrDefault(ex => ex.Name == exercise);
        if (exerciseRecord != null) {
          var userExercise = new UserExercise(uid) {
            ExerciseId = exerciseRecord.Id
          };

          _context.UserExercises.Add(userExercise);
        }
      }

      _context.SaveChanges();
    }
  }

  public UserExercise Create(UserExercise newUserExercise)
  {
    _context.UserExercises.Add(newUserExercise);
    _context.SaveChanges();

    return newUserExercise;
  }
}