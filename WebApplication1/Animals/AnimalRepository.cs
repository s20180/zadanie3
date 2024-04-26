using System.Data.SqlClient;

namespace WebApplication1.Animals;

public interface IAnimalRepository
{
    public IEnumerable<Animal> FetchAllAnimals(string orderBy);
    public bool CreateAnimal(string name, string description, string category, string area);
    public bool UpdateAnimal(int id, string name, string description, string category, string area);
    public bool DeleteAnimal(int id);
}

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;
    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IEnumerable<Animal> FetchAllAnimals(string orderBy)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        var safeOrderBy = new string[] { "Name", "Description", "Category", "Area" }.Contains(orderBy) ? orderBy : "Name";
        var command = new SqlCommand($"SELECT * FROM Animal ORDER BY {safeOrderBy}", connection);
        using var reader = command.ExecuteReader();

        var Animals = new List<Animal>();
        while (reader.Read())
        {
            var Animal = new Animal()
            {
                Id = (int)reader["IdAnimal"],
                Name = reader["Name"].ToString()!,
                Description = reader["Description"].ToString()!,
                Category = reader["Category"].ToString()!,
                Area = reader["Area"].ToString()!,
            };
            Animals.Add(Animal);
        }

        return Animals;
    }

    public bool CreateAnimal(string name, string description, string category, string area)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        
        using var command = new SqlCommand("INSERT INTO Animal (Name, Description, Category, Area) VALUES (@name, @description, @category, @area)", connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@category", category);
        command.Parameters.AddWithValue("@area", area);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }
    
    public bool UpdateAnimal(int id, string name, string description, string category, string area)
    {
        if (name.Length == 0 && description.Length == 0 && category.Length == 0 && area.Length == 0)
        {
            return false;
        }
        
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        var updateString = "SET ";
        updateString += name.Length > 0 ? "Name = @name," : "";
        updateString += name.Length > 0 ? "Description = @description," : "";
        updateString += name.Length > 0 ? "Category = @category," : "";
        updateString += name.Length > 0 ? "Area = @area," : "";
        updateString = updateString.Substring(0, updateString.Length - 1);
        
        using var command = new SqlCommand("UPDATE Animal " + updateString + " WHERE IdAnimal = @id", connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@description", description);
        command.Parameters.AddWithValue("@category", category);
        command.Parameters.AddWithValue("@area", area);
        command.Parameters.AddWithValue("@id", id);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }

    public bool DeleteAnimal(int id)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        
        using var command = new SqlCommand("DELETE FROM Animal WHERE IdAnimal = @id", connection);
        command.Parameters.AddWithValue("@id", id);
        var affectedRows = command.ExecuteNonQuery();
        return affectedRows == 1;
    }
}