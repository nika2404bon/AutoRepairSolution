using AutoRepairService.Data.Repositories;

namespace AutoRepairService.Data
{
    public class DataContext
    {
        public IClientRepository Clients { get; }
        public IServiceRepository Services { get; }
        public IAppointmentRepository Appointments { get; }

        public DataContext(IClientRepository clientRepository, 
                         IServiceRepository serviceRepository, 
                         IAppointmentRepository appointmentRepository)
        {
            Clients = clientRepository;
            Services = serviceRepository;
            Appointments = appointmentRepository;
        }
    }
}