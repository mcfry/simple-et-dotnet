using ExerciseTimer.Services;
using ExerciseTimer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTimer.Controllers;

[ApiController]
[Route("[controller]")]
public class ExerciseController : ControllerBase
{
  readonly ExerciseService _service;
  readonly FirebaseService _firebaseService;

  public ExerciseController(ExerciseService service, FirebaseService firebaseService)
  {
    _service = service;
    _firebaseService = firebaseService;
  }

  [HttpGet]
  public async Task<IActionResult> GetAll()
  {
    string? idToken = Request.Headers["Authorization"];

    if (string.IsNullOrWhiteSpace(idToken))
    {
      return Unauthorized("Missing or invalid token");
    }

    // Strip Bearer
    idToken = idToken.StartsWith("Bearer ") ? idToken.Substring(7) : idToken;

    // Verify the Firebase ID token
    string? uid = await _firebaseService.VerifyFirebaseIdTokenAsync(idToken);
    if (uid == null)
    {
      return Unauthorized("Invalid token");
    }

    // Token is valid; proceed with fetching exercises
    var exercises = _service.GetAll();
    return Ok(exercises);
  }

  [HttpGet("{id}")]
  public ActionResult<Exercise> GetById(int id)
  {
    var Exercise = _service.GetByIdSimple(id);

    if (Exercise is not null)
    {
      return Exercise;
    }
    else
    {
      return NotFound();
    }
  }

  [HttpPost]
  public IActionResult Create(Exercise newExercise)
  {
    var Exercise = _service.Create(newExercise);
    return CreatedAtAction(nameof(GetById), new { id = Exercise!.Id }, Exercise);
  }
}