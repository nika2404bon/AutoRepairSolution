using AutoRepairService.Data.Models;
using System.Collections.Generic;

namespace AutoRepairService.Data.Repositories
{
    public interface IAppointmentRepository
    {
        IEnumerable<Appointment> GetAllAppointments();
        Appointment? GetAppointmentById(int id);
        void AddAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
        void DeleteAppointment(int id);
        IEnumerable<Appointment> GetAppointmentsByDate(DateTime date);
        IEnumerable<Appointment> GetAppointmentsByClient(int clientId);
        IEnumerable<Appointment> GetAppointmentsByService(int serviceId);
    }
}