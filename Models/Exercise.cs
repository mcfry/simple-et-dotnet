using System.ComponentModel.DataAnnotations;

namespace ExerciseTimer.Models;

public class Exercise
{
  public int Id { get; set; }
  [Required]
  [MaxLength(50)]
  public string? Name { get; set; }

  // Relations
  public ICollection<SetRecord> SetRecords { get; set; }
  public ICollection<UserExercise> UserExercises { get; set; }

  public Exercise()
  {
    SetRecords = new List<SetRecord>();
    UserExercises = new List<UserExercise>();
  }
}