using AutoRepairService.Data.Models;
using AutoRepairService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AutoRepairService.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IClientService _clientService;
        private readonly IServiceService _serviceService;

        public AppointmentsController(
            IAppointmentService appointmentService,
            IClientService clientService,
            IServiceService serviceService)
        {
            _appointmentService = appointmentService;
            _clientService = clientService;
            _serviceService = serviceService;
        }

        public IActionResult Index(DateTime? date, int? clientId, int? serviceId)
        {
            var appointments = _appointmentService.FilterAppointments(date, clientId, serviceId);
            ViewBag.Clients = new SelectList(_clientService.GetAllClients(), "Id", "FullName");
            ViewBag.Services = new SelectList(_serviceService.GetAllServices(), "Id", "Name");
            return View(appointments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Clients = new SelectList(_clientService.GetAllClients(), "Id", "FullName");
            ViewBag.Services = new SelectList(_serviceService.GetAllServices(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _appointmentService.CreateAppointment(appointment);
                return RedirectToAction("Index");
            }

            ViewBag.Clients = new SelectList(_clientService.GetAllClients(), "Id", "FullName");
            ViewBag.Services = new SelectList(_serviceService.GetAllServices(), "Id", "Name");
            return View(appointment);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var appointment = _appointmentService.GetAppointment(id);
            if (appointment == null)
            {
                return NotFound();
            }

            ViewBag.Clients = new SelectList(_clientService.GetAllClients(), "Id", "FullName");
            ViewBag.Services = new SelectList(_serviceService.GetAllServices(), "Id", "Name");
            return View(appointment);
        }

        [HttpPost]
        public IActionResult Edit(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _appointmentService.UpdateAppointment(appointment);
                return RedirectToAction("Index");
            }

            ViewBag.Clients = new SelectList(_clientService.GetAllClients(), "Id", "FullName");
            ViewBag.Services = new SelectList(_serviceService.GetAllServices(), "Id", "Name");
            return View(appointment);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _appointmentService.DeleteAppointment(id);
            return RedirectToAction("Index");
        }
    }
}