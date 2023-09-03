using System.ComponentModel.DataAnnotations;

namespace ExerciseTimer.Models;

public class Set
{
  public int Id { get; set; }
  public int Time { get; set; }
  public int? TimeAfter { get; set; }
  public int SetNumber { get; set; }

  // Relations
  public int SetRecordId { get; set; }
  public SetRecord? SetRecord { get; set; }
}