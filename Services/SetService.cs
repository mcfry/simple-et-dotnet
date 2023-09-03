using ExerciseTimer.Models;
using ExerciseTimer.Data;
using Microsoft.EntityFrameworkCore;

namespace ExerciseTimer.Services;

public class SetService
{
  private readonly ExerciseContext _context;

  public SetService(ExerciseContext context)
  {
    _context = context;
  }

  public Set Create(Set newSet)
  {
    _context.Sets.Add(newSet);
    _context.SaveChanges();

    return newSet;
  }
}