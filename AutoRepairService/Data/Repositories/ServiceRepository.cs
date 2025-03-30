using AutoRepairService.Data.Models;
using System.Text.Json;

namespace AutoRepairService.Data.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly string _filePath;
        private List<Service> _services = new List<Service>(); // Инициализация поля

        public ServiceRepository(string filePath)
        {
            _filePath = filePath;
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _services = JsonSerializer.Deserialize<List<Service>>(json) ?? new List<Service>();
            }
            else
            {
                _services = new List<Service>();
            }
        }

        private void SaveData()
        {
            var json = JsonSerializer.Serialize(_services);
            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<Service> GetAllServices() => _services;

        public Service GetServiceById(int id) => _services.FirstOrDefault(s => s.Id == id);

        public void AddService(Service service)
        {
            service.Id = _services.Any() ? _services.Max(s => s.Id) + 1 : 1;
            _services.Add(service);
            SaveData();
        }

        public void UpdateService(Service service)
        {
            var index = _services.FindIndex(s => s.Id == service.Id);
            if (index != -1)
            {
                _services[index] = service;
                SaveData();
            }
        }

        public void DeleteService(int id)
        {
            var service = GetServiceById(id);
            if (service != null)
            {
                _services.Remove(service);
                SaveData();
            }
        }
    }
}