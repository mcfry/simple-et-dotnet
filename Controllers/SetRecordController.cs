using ExerciseTimer.Services;
using ExerciseTimer.Models;
using ExerciseTimer.Attributes;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTimer.Controllers;

[ApiController]
[Route("[controller]")]
public class SetRecordController : ControllerBase
{
  readonly SetRecordService _service;

  public SetRecordController(SetRecordService service)
  {
    _service = service;
  }

  [HttpGet]
  [FirebaseTokenVerification]
  public IEnumerable<SetRecord> GetAll()
  {
    return _service.GetAll();
  }

  [HttpGet("byExercise/{exerciseId}")]
  [FirebaseTokenVerification]
  public IActionResult GetAllByExercise(int exerciseId)
  {
    if (HttpContext.Items.TryGetValue("uid", out var uidObject) && uidObject is string uid) {
      var records = _service.GetAllByExercise(exerciseId, uid);

      var settings = new JsonSerializerSettings {
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
      };

      var jsonResponse = JsonConvert.SerializeObject(records, settings);
      return Ok(jsonResponse);
    }

    return BadRequest();
  }

  [HttpGet("{id}")]
  [FirebaseTokenVerification]
  public ActionResult<SetRecord> GetById(int id)
  {
    var SetRecord = _service.GetById(id);

    if (SetRecord is not null)
    {
      return SetRecord;
    }
    else
    {
      return NotFound();
    }
  }

  [HttpPost]
  [FirebaseTokenVerification]
  public IActionResult Create(SetRecord newSetRecord)
  {    
    var createdSetRecord = _service.Create(newSetRecord);

    // handle reference loops
    var settings = new JsonSerializerSettings
    {
      ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    };

    var jsonResponse = JsonConvert.SerializeObject(createdSetRecord, settings);
    return CreatedAtAction(nameof(GetById), new { id = createdSetRecord!.Id }, jsonResponse);
  }
}