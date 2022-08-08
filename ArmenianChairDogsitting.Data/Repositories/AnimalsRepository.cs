using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public class AnimalsRepository : IAnimalsRepository
{
    private readonly ArmenianChairDogsittingContext _context;
    public AnimalsRepository(ArmenianChairDogsittingContext context)
    {
        _context = context;
    }

    public int AddAnimal(Animal animal)
    {
        _context.Animals.Add(animal);
        _context.SaveChanges();

        return animal.Id;
    }

    public Animal? GetAnimalById(int id) => _context.Animals.FirstOrDefault(a => a.Id == id);

    public List<Animal> GetAllAnimalsByClient(int id) => _context.Animals.Where(a => a.ClientId == id).ToList();

    public void UpdateAnimal(Animal newAnimal, int id)
    {
        var animal = _context.Animals.FirstOrDefault(a => a.Id == id);
        animal = newAnimal;
        _context.Animals.Update(animal);
        _context.SaveChanges();
    }

    public void RemoveOrRestoreAnimal(Animal animal)
    {
        _context.Animals.Update(animal);
        _context.SaveChanges();
    }
}
