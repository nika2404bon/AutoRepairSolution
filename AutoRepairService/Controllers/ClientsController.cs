using AutoRepairService.Data.Models;
using AutoRepairService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoRepairService.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public IActionResult Index()
        {
            var clients = _clientService.GetAllClients();
            return View(clients);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientService.AddClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var client = _clientService.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                _clientService.UpdateClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (_clientService.DeleteClient(id))
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}