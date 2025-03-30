namespace AutoRepairService.Data.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public int ServiceId { get; set; }
        public Service? Service { get; set; }
        public string? Notes { get; set; }
    }
}