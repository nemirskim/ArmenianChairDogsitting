using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business;

public interface IAnimalsService
{
    int AddAnimal(Animal animal);
    Animal GetAnimalById(int id);
    List<Animal> GetAllAnimalsByClient(int id);
    void UpdateAnimal(Animal animal, int id);
    void RemoveOrRestoreAnimal(int id, bool isDeleted);
}
