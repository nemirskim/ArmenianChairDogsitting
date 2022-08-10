using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public interface IClientsRepository
{
    int AddClient(Client client);
    Client GetClientById(int id);
    List<Client> GetAllClients();
    void UpdateClient(Client client);
    Client? GetClientByEmail(string email);
    public void RemoveOrRestoreClient(Client client);
    public void UpdatePassword(Client newPassword);
}
