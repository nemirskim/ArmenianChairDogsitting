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
        var client = GetClientById(newClient.Id);
        client!.Name = newClient.Name;
        client.LastName = newClient.LastName;
        _context.Clients.Update(client);
        _context.SaveChanges();
    }

    public void RemoveOrRestoreClient(int id, bool isDeleting)
    {
        var client = GetClientById(id);
        client!.IsDeleted = isDeleting;
        _context.SaveChanges();
    }

    public Client? GetClientByEmail(string email) => _context.Clients.FirstOrDefault(client => client.Email == email);
}
