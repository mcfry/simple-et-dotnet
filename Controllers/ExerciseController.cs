using ExerciseTimer.Services;
using ExerciseTimer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTimer.Controllers;

[ApiController]
[Route("[controller]")]
public class ExerciseController : ControllerBase
{
  readonly ExerciseService _service;

  public ExerciseController(ExerciseService service)
  {
    _service = service;
  }

  [HttpGet]
  public IEnumerable<Exercise> GetAll()
  {
    return _service.GetAll();
  }

  [HttpGet("{id}")]
  public ActionResult<Exercise> GetById(int id)
  {
    var Exercise = _service.GetById(id);

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

  // [HttpPut("{id}/addtopping")]
  // public IActionResult AddTopping(int id, int toppingId)
  // {
  //   var ExerciseToUpdate = _service.GetById(id);

  //   if (ExerciseToUpdate is not null)
  //   {
  //     _service.AddTopping(id, toppingId);
  //     return NoContent();
  //   }
  //   else
  //   {
  //     return NotFound();
  //   }
  // }

  // [HttpPut("{id}/updatesauce")]
  // public IActionResult UpdateSauce(int id, int sauceId)
  // {
  //   var ExerciseToUpdate = _service.GetById(id);

  //   if (ExerciseToUpdate is not null)
  //   {
  //     _service.UpdateSauce(id, sauceId);
  //     return NoContent();
  //   }
  //   else
  //   {
  //     return NotFound();
  //   }
  // }

  // [HttpDelete("{id}")]
  // public IActionResult Delete(int id)
  // {
  //   var Exercise = _service.GetById(id);

  //   if (Exercise is not null)
  //   {
  //     _service.DeleteById(id);
  //     return Ok();
  //   }
  //   else
  //   {
  //     return NotFound();
  //   }
  // }
}