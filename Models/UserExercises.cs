using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExerciseTimer.Models;

public class UserExercise {

  public int Id { get; set; }
  public string Uid { get; private set; }
  public int ExerciseId { get; set; }

  // Relations
  public Exercise? Exercise { get; set; }

  public UserExercise(string uid) {
    Uid = uid;
  }
}