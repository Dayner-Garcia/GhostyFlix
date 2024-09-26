using Application.App_Management.IServices;
using Application.App_Management.ViewModels;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GhostyFlix.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersServices _producersServices;

        public ProducersController(IProducersServices producersServices)
        {
            _producersServices = producersServices;
        }

        public IActionResult Index()
        {
            var producersList = _producersServices.GetAllProducers();

            var model = producersList.Select(producer => new ProducersViewModel
            {
                Id = producer.Id,
                Name = producer.Name
            }).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProducersViewModel producerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(producerViewModel);
            }

            _producersServices.CreateProducer(new Producer { Name = producerViewModel.Name });
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var producer = _producersServices.GetProducerById(id);
            if (producer == null)
            {
                return NotFound();
            }

            var model = new ProducersViewModel
            {
                Id = producer.Id,
                Name = producer.Name
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProducersViewModel producerViewModel)
        {
            if (id != producerViewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(producerViewModel);
            }

            var producer = new Producer { Id = producerViewModel.Id, Name = producerViewModel.Name };
            _producersServices.UpdateProducer(producer);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var producer = _producersServices.GetProducerById(id);
            if (producer == null)
            {
                return NotFound();
            }

            var model = new ProducersViewModel
            {
                Id = producer.Id,
                Name = producer.Name
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var producer = _producersServices.GetProducerById(id);
            if (producer == null)
            {
                return NotFound();
            }

            var seriesByProducer = _producersServices.GetSeriesByProducer(id);
            if (seriesByProducer.Any())
            {
                TempData["ErrorMessage"] = "No se puede eliminar el productor porque tiene series asociadas.";
                return RedirectToAction("Delete", new { id = id });
            }

            _producersServices.DeleteProducer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}