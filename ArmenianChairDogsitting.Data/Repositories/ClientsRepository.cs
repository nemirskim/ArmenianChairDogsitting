using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public class ClientsRepository : IClientsService
{
    private readonly ArmenianChairDogsittingContext _context;
    public ClientsRepository(ArmenianChairDogsittingContext context)
    {
        _context = context;
    }

    public int AddClient(Client client)
    {
        client.RegistrationDate = new DateTime(2022, 08, 18);
        _context.Clients.Add(client);
        _context.SaveChanges();

        return client.Id;
    }

    public Client? GetClientById(int id) => _context.Clients.FirstOrDefault(c => c.Id == id);

    public List<Client> GetAllClients()
        => _context.Clients.Where(c => !c.IsDeleted).ToList();

    public void UpdateClient(Client newClient)
    {
        _context.Clients.Update(newClient);
        _context.SaveChanges();
    }

    public Client? GetClientByEmail(string email) => _context.Clients.FirstOrDefault(c => c.Email == email);

    public void RemoveOrRestoreClient(Client client)
    {
        _context.Clients.Update(client);
        _context.SaveChanges();
    }

    public void UpdatePassword(Client ClientPasswordForUpdate)
    {
        _context.Clients.Update(ClientPasswordForUpdate);
        _context.SaveChanges();
    }
}