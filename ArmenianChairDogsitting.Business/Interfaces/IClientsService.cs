using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Business;

public interface IClientsService
{
    int AddClient(Client clientModel);
    Client GetClientById(int id);
    List<Client> GetAllClients();
    void UpdateClient(Client clientModel, int id);
    void RemoveOrRestoreClient(int id, bool isDeleted);
}
