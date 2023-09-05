using System.ComponentModel.DataAnnotations;

namespace ExerciseTimer.Models;

public class SetRecord
{
  public int Id { get; set; }
  public string Uid { get; set; }
  public DateTime CreatedAt { get; set; }

  // Relations
  public int ExerciseId { get; set; }
  public Exercise? Exercise { get; set; }
  public ICollection<Set> Sets { get; set; }

  public SetRecord(string uid)
  {
    Uid = uid;
    CreatedAt = DateTime.UtcNow;
    Sets = new List<Set>();
  }
}