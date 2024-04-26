namespace WebApplication1.Animals;

public interface IAnimalService
{
    public IEnumerable<Animal> GetAllAnimals(string orderBy);
    public bool AddAnimal(CreateAnimalDTO dto);
    public bool UpdateAnimal(int id, UpdateAnimalDTO dto);
    public bool DeleteAnimal(int id);
}

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _AnimalRepository;
    public AnimalService(IAnimalRepository AnimalRepository)
    {
        _AnimalRepository = AnimalRepository;
    }
    
    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        return _AnimalRepository.FetchAllAnimals(orderBy);
    }

    public bool AddAnimal(CreateAnimalDTO dto)
    {
        return _AnimalRepository.CreateAnimal(dto.Name, dto.Description, dto.Category, dto.Area);
    }

    public bool UpdateAnimal(int id, UpdateAnimalDTO dto)
    {
        return _AnimalRepository.UpdateAnimal(id, dto.Name, dto.Description, dto.Category, dto.Area);
    }

    public bool DeleteAnimal(int id)
    {
        return _AnimalRepository.DeleteAnimal(id);
    }
}