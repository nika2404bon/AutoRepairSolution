using AutoRepairService.Data.Models;
using System.Collections.Generic;

namespace AutoRepairService.Services
{
    public interface IClientService
    {
        IEnumerable<Client> GetAllClients();
        Client? GetClientById(int id);
        void AddClient(Client client);
        void UpdateClient(Client client);
        bool DeleteClient(int id);
    }
}