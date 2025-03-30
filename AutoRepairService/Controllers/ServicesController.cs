// Controllers/ServicesController.cs
using AutoRepairService.Data.Models;
using AutoRepairService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairService.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public IActionResult Index()
        {
            var services = _serviceService.GetAllServices();
            return View(services);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (ModelState.IsValid)
            {
                _serviceService.AddService(service);
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var service = _serviceService.GetServiceById(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        public IActionResult Edit(Service service)
        {
            if (ModelState.IsValid)
            {
                _serviceService.UpdateService(service);
                return RedirectToAction("Index");
            }
            return View(service);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var result = _serviceService.DeleteService(id);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}