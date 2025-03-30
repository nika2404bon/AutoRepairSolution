using AutoRepairService.Data.Models;
using System.Text.Json;

namespace AutoRepairService.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _filePath;
        private List<Client> _clients = new List<Client>();

        public ClientRepository(string filePath)
        {
            _filePath = filePath;
            EnsureFileExists();
            LoadClients();
        }

        private void EnsureFileExists()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        private void LoadClients()
        {
            var json = File.ReadAllText(_filePath);
            _clients = JsonSerializer.Deserialize<List<Client>>(json) ?? new List<Client>();
        }

        private void SaveClients()
        {
            var json = JsonSerializer.Serialize(_clients);
            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<Client> GetAllClients() => _clients;

        public Client? GetClientById(int id) => _clients.FirstOrDefault(c => c.Id == id);

        public void AddClient(Client client)
        {
            client.Id = _clients.Any() ? _clients.Max(c => c.Id) + 1 : 1;
            _clients.Add(client);
            SaveClients();
        }

        public void UpdateClient(Client client)
        {
            var existingClient = GetClientById(client.Id);
            if (existingClient != null)
            {
                existingClient.FirstName = client.FirstName;
                existingClient.LastName = client.LastName;
                existingClient.Phone = client.Phone;
                existingClient.Email = client.Email;
                SaveClients();
            }
        }

        public bool DeleteClient(int id)
        {
            var client = GetClientById(id);
            if (client != null)
            {
                _clients.Remove(client);
                SaveClients();
                return true;
            }
            return false;
        }
    }
}