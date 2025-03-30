using AutoRepairService.Data.Models;
using System.Collections.Generic;

namespace AutoRepairService.Data.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client? GetClientById(int id);  // Добавлен nullable возвращаемый тип
        void AddClient(Client client);
        void UpdateClient(Client client);
        bool DeleteClient(int id);      // Изменен возвращаемый тип на bool
    }
}