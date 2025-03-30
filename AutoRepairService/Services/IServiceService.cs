// Services/IServiceService.cs
using AutoRepairService.Data.Models;
using System.Collections.Generic;

namespace AutoRepairService.Services
{
    public interface IServiceService
    {
        IEnumerable<Service> GetAllServices();
        Service? GetServiceById(int id);
        void AddService(Service service);
        void UpdateService(Service service);
        bool DeleteService(int id);
    }
}