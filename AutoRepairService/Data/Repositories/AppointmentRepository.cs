using AutoRepairService.Data.Models;
using System.Text.Json;

namespace AutoRepairService.Data.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly string _filePath;
        private List<Appointment> _appointments = new();
        private readonly IClientRepository _clientRepository;
        private readonly IServiceRepository _serviceRepository;

        public AppointmentRepository(string filePath, IClientRepository clientRepository, IServiceRepository serviceRepository)
        {
            _filePath = filePath;
            _clientRepository = clientRepository;
            _serviceRepository = serviceRepository;
            EnsureFileExists();
            LoadAppointments();
        }

        private void EnsureFileExists()
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }

        private void LoadAppointments()
        {
            var json = File.ReadAllText(_filePath);
            _appointments = JsonSerializer.Deserialize<List<Appointment>>(json) ?? new();
            
            foreach (var app in _appointments)
            {
                app.Client = _clientRepository.GetClientById(app.ClientId);
                app.Service = _serviceRepository.GetServiceById(app.ServiceId);
            }
        }

        private void SaveAppointments()
        {
            var json = JsonSerializer.Serialize(_appointments);
            File.WriteAllText(_filePath, json);
        }

        public IEnumerable<Appointment> GetAllAppointments() => _appointments;

        public Appointment? GetAppointmentById(int id) => _appointments.FirstOrDefault(a => a.Id == id);

        public void AddAppointment(Appointment appointment)
        {
            appointment.Id = _appointments.Any() ? _appointments.Max(a => a.Id) + 1 : 1;
            appointment.Client = _clientRepository.GetClientById(appointment.ClientId);
            appointment.Service = _serviceRepository.GetServiceById(appointment.ServiceId);
            _appointments.Add(appointment);
            SaveAppointments();
        }

        public void UpdateAppointment(Appointment appointment)
        {
            var existing = GetAppointmentById(appointment.Id);
            if (existing != null)
            {
                existing.AppointmentDate = appointment.AppointmentDate;
                existing.ClientId = appointment.ClientId;
                existing.ServiceId = appointment.ServiceId;
                existing.Notes = appointment.Notes;
                existing.Client = _clientRepository.GetClientById(appointment.ClientId);
                existing.Service = _serviceRepository.GetServiceById(appointment.ServiceId);
                SaveAppointments();
            }
        }

        public void DeleteAppointment(int id)
        {
            var appointment = GetAppointmentById(id);
            if (appointment != null)
            {
                _appointments.Remove(appointment);
                SaveAppointments();
            }
        }

        public IEnumerable<Appointment> GetAppointmentsByDate(DateTime date) =>
            _appointments.Where(a => a.AppointmentDate.Date == date.Date);

        public IEnumerable<Appointment> GetAppointmentsByClient(int clientId) =>
            _appointments.Where(a => a.ClientId == clientId);

        public IEnumerable<Appointment> GetAppointmentsByService(int serviceId) =>
            _appointments.Where(a => a.ServiceId == serviceId);
    }
}