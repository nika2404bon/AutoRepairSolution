using AutoRepairService.Data.Models;
using System.Collections.Generic;

namespace AutoRepairService.Services
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAllAppointments();
        Appointment? GetAppointment(int id);
        void CreateAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(int id);
        IEnumerable<Appointment> FilterAppointments(DateTime? date, int? clientId, int? serviceId);
    }
}