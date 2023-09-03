using ExerciseTimer.Services;
using ExerciseTimer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTimer.Controllers;

[ApiController]
[Route("[controller]")]
public class SetRecordController : ControllerBase
{
  SetRecordService _service;

  public SetRecordController(SetRecordService service)
  {
    _service = service;
  }

  [HttpGet]
  public IEnumerable<SetRecord> GetAll()
  {
    return _service.GetAll();
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
    var SetRecord = _service.Create(newSetRecord);
    return CreatedAtAction(nameof(GetById), new { id = SetRecord!.Id }, SetRecord);
  }
}