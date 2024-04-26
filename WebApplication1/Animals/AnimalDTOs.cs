using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Animals;

public class CreateAnimalDTO
{
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    public string Category { get; set; }
    [Required]
    public string Area { get; set; }
}

public class UpdateAnimalDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Area { get; set; }
}

