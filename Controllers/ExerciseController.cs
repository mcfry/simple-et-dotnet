using ExerciseTimer.Services;
using ExerciseTimer.Models;
using ExerciseTimer.Attributes;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTimer.Controllers;

[ApiController]
[Route("[controller]")]
public class ExerciseController : ControllerBase
{
  readonly ExerciseService _service;
  readonly UserExerciseService _userExerciseService;
  readonly FirebaseService _firebaseService;

  public ExerciseController(ExerciseService service, UserExerciseService userExerciseService, FirebaseService firebaseService)
  {
    _service = service;
    _userExerciseService = userExerciseService;
    _firebaseService = firebaseService;
  }

  [HttpGet]
  [FirebaseTokenVerification]
  public IActionResult GetAll()
  {
    if (HttpContext.Items.TryGetValue("uid", out var uidObject) && uidObject is string uid) {
      _userExerciseService.CheckAndCreateDefaultExercises(uid);

      var exercises = _service.GetAll(uid);

      var settings = new JsonSerializerSettings {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
      };

      var jsonResponse = JsonConvert.SerializeObject(exercises, settings);
      return Ok(jsonResponse);
    }

    return BadRequest();
  }

  [HttpGet("{id}")]
  [FirebaseTokenVerification]
  public ActionResult<Exercise> GetById(int id)
  {
    var Exercise = _service.GetByIdSimple(id);

    if (Exercise is not null) {
      return Exercise;
    } else {
      return NotFound();
    }
  }

  [HttpPost]
  [FirebaseTokenVerification]
  public IActionResult Create(Exercise newExercise)
  {
    if (HttpContext.Items.TryGetValue("uid", out var uidObject) && uidObject is string uid) {
      _userExerciseService.CheckAndCreateDefaultExercises(uid);

      var exercise = _service.Create(newExercise, uid);

      var settings = new JsonSerializerSettings {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
      };

      var jsonResponse = JsonConvert.SerializeObject(exercise, settings);
      return Ok(jsonResponse);
    }

    return BadRequest();
  }
}