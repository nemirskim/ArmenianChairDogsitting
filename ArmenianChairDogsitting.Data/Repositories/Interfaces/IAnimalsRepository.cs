using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public interface IAnimalsRepository
{
    int AddAnimal(Animal animal);
    Animal GetAnimalById(int id);
    List<Animal> GetAllAnimalsByClient(int id);
    void UpdateAnimal(Animal animal);
    void RemoveOrRestoreAnimal(Animal animal);
}
