using Microsoft.EntityFrameworkCore;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Repositories;

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

    public List<Client> GetAllClients() => _context.Clients.ToList();

    public void UpdateClient(Client client)
    {
        client.Name = client.Name;
        client.LastName = client.LastName;
        _context.Clients.Update(client);
        _context.SaveChanges();
    }

    public void RemoveClient(int id)
    {
        var client = GetClientById(id);
        client.IsDeleted = true;
        _context.SaveChanges();
    }

    public void RestoreClient(int id)
    {
        var client = GetClientById(id);
        client.IsDeleted = false;
        _context.SaveChanges();
    }
}
