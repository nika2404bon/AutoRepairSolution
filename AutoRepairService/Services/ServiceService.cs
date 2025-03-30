// Services/ServiceService.cs
using AutoRepairService.Data.Models;
using AutoRepairService.Data.Repositories;
using System.Collections.Generic;

namespace AutoRepairService.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Service> GetAllServices() => _repository.GetAllServices();

        public Service? GetServiceById(int id) => _repository.GetServiceById(id);

        public void AddService(Service service) => _repository.AddService(service);

        public void UpdateService(Service service) => _repository.UpdateService(service);

        public bool DeleteService(int id) 
{
    try
    {
        var service = _repository.GetServiceById(id);
        if (service != null)
        {
            _repository.DeleteService(id);
            return true;
        }
        return false;
    }
    catch
    {
        return false;
    }
}
    }
}