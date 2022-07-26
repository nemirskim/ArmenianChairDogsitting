using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public interface IClientsRepository
{
    int AddClient(Client client);
    Client GetClientById(int id);
    List<Client> GetAllClients();
    void UpdateClient(Client client, int id);
    public void RemoveOrRestoreClient(int id, bool isDeleting);
}