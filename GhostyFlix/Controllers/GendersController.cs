using Application.App_Management.IServices;
using Application.App_Management.ViewModels;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GhostyFlix.Controllers
{
    public class GendersController : Controller
    {
        private readonly IGendersServices _gendersServices;

        public GendersController(IGendersServices gendersServices)
        {
            _gendersServices = gendersServices;
        }

        public IActionResult Index()
        {
            var gendersList = _gendersServices.GetAllGenres();

            var model = gendersList.Select(gender => new GendersViewModel
            {
                Id = gender.Id,
                Name = gender.Name
            }).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GendersViewModel genderViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(genderViewModel);
            }

            _gendersServices.CreateGenre(new Gender { Name = genderViewModel.Name });
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var gender = _gendersServices.GetGenreById(id);
            if (gender == null)
            {
                return NotFound();
            }

            var model = new GendersViewModel
            {
                Id = gender.Id,
                Name = gender.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, GendersViewModel genderViewModel)
        {
            if (id != genderViewModel.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(genderViewModel);
            }

            var gender = new Gender { Id = genderViewModel.Id, Name = genderViewModel.Name };
            _gendersServices.UpdateGenre(gender);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var gender = _gendersServices.GetGenreById(id);
            if (gender == null)
            {
                return NotFound();
            }

            var model = new GendersViewModel
            {
                Id = gender.Id,
                Name = gender.Name
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var genre = _gendersServices.GetGenreById(id);
            if (genre == null)
            {
                return NotFound();
            }

            var seriesAsPrimary = _gendersServices.GetSeriesByPrimaryGenre(id);
            var seriesAsSecondary = _gendersServices.GetSeriesBySecondaryGenre(id);

            if (seriesAsPrimary.Any() || seriesAsSecondary.Any())
            {
                TempData["ErrorMessage"] = "No se puede eliminar el genero porque tiene series asociadas.";
                return RedirectToAction("Delete", new { id = id });
            }

            _gendersServices.DeleteGenre(id);
            return RedirectToAction(nameof(Index));
        }
    }
}