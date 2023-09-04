using ExerciseTimer.Services;
using ExerciseTimer.Models;
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
  public IEnumerable<SetRecord> GetAll()
  {
    return _service.GetAll();
  }

  [HttpGet("byExercise/{exerciseId}")]
  public IActionResult GetAllByExercise(int exerciseId)
  {
    var records = _service.GetAllByExercise(exerciseId);
    
    // handle reference loops
    var settings = new JsonSerializerSettings
    {
      ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    };

    var jsonResponse = JsonConvert.SerializeObject(records, settings);

    return Ok(jsonResponse);
  }

  [HttpGet("{id}")]
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