using AutoRepairService.Data.Models;
using AutoRepairService.Data.Repositories;
using System.Collections.Generic;

namespace AutoRepairService.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Client> GetAllClients() => _repository.GetAllClients();
        
        public Client? GetClientById(int id) => _repository.GetClientById(id);
        
        public void AddClient(Client client) => _repository.AddClient(client);
        
        public void UpdateClient(Client client) => _repository.UpdateClient(client);
        
        public bool DeleteClient(int id) => _repository.DeleteClient(id);
    }
}