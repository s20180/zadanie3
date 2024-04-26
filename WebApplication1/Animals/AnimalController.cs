using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Animals;

[ApiController]
[Route("/api/animals")]
public class AnimalController : ControllerBase
{
    private readonly IAnimalService _AnimalService;
    public AnimalController(IAnimalService AnimalService)
    {
        _AnimalService = AnimalService;
    }
    
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllAnimals([FromQuery] string orderBy = "")
    {
        var Animals = _AnimalService.GetAllAnimals(orderBy);
        return Ok(Animals);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal([FromRoute] int id, [FromBody] UpdateAnimalDTO dto)
    {
        var success = _AnimalService.UpdateAnimal(id, dto);
        return success ? Ok() : Conflict();
    }
    
    [HttpPost]
    public IActionResult CreateAnimal([FromBody] CreateAnimalDTO dto)
    {
        var success = _AnimalService.AddAnimal(dto);
        return success ? StatusCode(StatusCodes.Status201Created) : Conflict();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal([FromRoute] int id)
    {
        var success = _AnimalService.DeleteAnimal(id);
        return success ? Ok() : Conflict();
    }
}