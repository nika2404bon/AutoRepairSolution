using AutoRepairService.Data.Models;
using AutoRepairService.Data.Repositories;
using System.Collections.Generic;

namespace AutoRepairService.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IClientRepository _clientRepository;
        private readonly IServiceRepository _serviceRepository;

        public AppointmentService(
            IAppointmentRepository repository,
            IClientRepository clientRepository,
            IServiceRepository serviceRepository)
        {
            _repository = repository;
            _clientRepository = clientRepository;
            _serviceRepository = serviceRepository;
        }

        public IEnumerable<Appointment> GetAllAppointments() => _repository.GetAllAppointments();

        public Appointment? GetAppointment(int id) => _repository.GetAppointmentById(id);

        public void CreateAppointment(Appointment appointment)
        {
            appointment.Client = _clientRepository.GetClientById(appointment.ClientId);
            appointment.Service = _serviceRepository.GetServiceById(appointment.ServiceId);
            _repository.AddAppointment(appointment);
        }

        public void UpdateAppointment(Appointment appointment)
        {
            appointment.Client = _clientRepository.GetClientById(appointment.ClientId);
            appointment.Service = _serviceRepository.GetServiceById(appointment.ServiceId);
            _repository.UpdateAppointment(appointment);
        }

        public void DeleteAppointment(int id) => _repository.DeleteAppointment(id);

        public IEnumerable<Appointment> FilterAppointments(DateTime? date, int? clientId, int? serviceId)
        {
            var appointments = _repository.GetAllAppointments();
            
            if (date.HasValue)
                appointments = appointments.Where(a => a.AppointmentDate.Date == date.Value.Date);
            
            if (clientId.HasValue)
                appointments = appointments.Where(a => a.ClientId == clientId.Value);
            
            if (serviceId.HasValue)
                appointments = appointments.Where(a => a.ServiceId == serviceId.Value);
            
            return appointments;
        }
    }
}