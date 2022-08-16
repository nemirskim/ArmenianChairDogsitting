using ArmenianChairDogsitting.Data.Repositories;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;
using ArmenianChairDogsitting.Business.Hashing;

namespace ArmenianChairDogsitting.Business;

public class AnimalsService : IAnimalsService
{
    private readonly IAnimalsRepository _animalsRepository;

    public AnimalsService(IAnimalsRepository animalsRepository)
    {
        _animalsRepository = animalsRepository;
    }

    public int AddAnimal(Animal animal) => _animalsRepository.AddAnimal(animal);

    public Animal GetAnimalById(int id) => _animalsRepository.GetAnimalById(id);

    public List<Animal> GetAllAnimalsByClient(int id) => _animalsRepository.GetAllAnimalsByClient(id);

    public void UpdateAnimal(Animal newAnimal, int id)
    {
        var animal = _animalsRepository.GetAnimalById(id);

        if (animal is null)
        {
            throw new NotFoundException("Dog wasn't found!");
        }
        animal.Name = newAnimal.Name;
        animal.Size = newAnimal.Size;
        animal.Age = newAnimal.Age;
        animal.Breed = newAnimal.Breed;
        animal.RecommendationsForCare = newAnimal.RecommendationsForCare;

        _animalsRepository.UpdateAnimal(animal);
    }

    public void RemoveOrRestoreAnimal(int id, bool isDeleted)
    {
        var animal = _animalsRepository.GetAnimalById(id);

        if (animal is null)
        {
            throw new NotFoundException("Dog wasn't found!");
        }

        animal.IsDeleted = isDeleted;
        _animalsRepository.RemoveOrRestoreAnimal(animal);
    }
}
