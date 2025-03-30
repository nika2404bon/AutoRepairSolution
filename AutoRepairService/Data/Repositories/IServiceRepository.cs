using AutoRepairService.Data.Models;

namespace AutoRepairService.Data.Repositories
{
    public interface IServiceRepository
    {
        IEnumerable<Service> GetAllServices();
        Service GetServiceById(int id);
        void AddService(Service service);
        void UpdateService(Service service);
        void DeleteService(int id);
    }
}