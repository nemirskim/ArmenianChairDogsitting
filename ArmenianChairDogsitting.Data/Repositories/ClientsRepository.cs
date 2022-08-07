using ArmenianChairDogsitting.Data.Entities;

namespace ArmenianChairDogsitting.Data.Repositories;

public class ClientsRepository : IClientsRepository
{
    private readonly ArmenianChairDogsittingContext _context;
    public ClientsRepository(ArmenianChairDogsittingContext context)
    {
        _context = context;
    }

    public int AddClient(Client client)
    {
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
}