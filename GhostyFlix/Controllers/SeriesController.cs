using Application.App_Management.IServices;
using Application.App_Management.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GhostyFlix.Controllers
{
    public class SeriesController : Controller
    {
        private readonly ISeriesServices _seriesService;
        private readonly IProducersServices _producerService;
        private readonly IGendersServices _genreService;

        public SeriesController(ISeriesServices seriesService, IProducersServices producerService,
            IGendersServices genreService)
        {
            _seriesService = seriesService;
            _producerService = producerService;
            _genreService = genreService;
        }

        public IActionResult Index(string searchString, int? producerId, int? genreId)
        {
            var seriesList = _seriesService.GetAllSeries();

            if (!string.IsNullOrEmpty(searchString))
            {
                seriesList = seriesList.Where(s => s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            var model = seriesList.Select(series => new SeriesViewModel
            {
                Id = series.Id,
                Name = series.Name,
                CoverImage = series.CoverImage,
                LinkVideo = series.LinkVideo,
                ProducerId = series.ProducerId,
                PrimaryGenreId = series.PrimaryGenreId,
                SecondaryGenreId = series.SecondaryGenreId,
                ProducerName = series.ProducerName,
                PrimaryGenreName = series.PrimaryGenreName,
                SecondaryGenreName = series.SecondaryGenreName
            }).ToList();
            
            ViewBag.SearchString = searchString;

            ViewBag.Producers = _producerService.GetAllProducers().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.Genres = _genreService.GetAllGenres().Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var series = _seriesService.GetById(id);
            if (series == null)
            {
                return NotFound();
            }

            var model = new SeriesViewModel
            {
                Id = series.Id,
                Name = series.Name,
                LinkVideo = series.LinkVideo,
                CoverImage = series.CoverImage,
                ProducerId = series.ProducerId,
                ProducerName = series.ProducerName,
                PrimaryGenreId = series.PrimaryGenreId,
                PrimaryGenreName = series.PrimaryGenreName,
                SecondaryGenreId = series.SecondaryGenreId,
                SecondaryGenreName = series.SecondaryGenreName
            };
            return View(model);
        }

        public IActionResult Create()
        {
            var model = new SeriesViewModel
            {
                Producers = GetProducers(),
                PrimaryGenres = GetPrimaryGenres(),
                SecondaryGenres = GetSecondaryGenres()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SeriesViewModel seriesViewModel)
        {
            if (!ModelState.IsValid)
            {
                seriesViewModel.Producers = GetProducers();
                seriesViewModel.PrimaryGenres = GetPrimaryGenres();
                seriesViewModel.SecondaryGenres = GetSecondaryGenres();
                return View(seriesViewModel);
            }

            _seriesService.Add(seriesViewModel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var series = _seriesService.GetById(id);
            if (series == null)
            {
                return NotFound();
            }

            var model = new SeriesViewModel
            {
                Id = series.Id,
                Name = series.Name,
                LinkVideo = series.LinkVideo,
                CoverImage = series.CoverImage,
                ProducerId = series.ProducerId,
                PrimaryGenreId = series.PrimaryGenreId,
                SecondaryGenreId = series.SecondaryGenreId,
                Producers = GetProducers(),
                PrimaryGenres = GetPrimaryGenres(),
                SecondaryGenres = GetSecondaryGenres()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SeriesViewModel seriesViewModel)
        {
            if (!ModelState.IsValid)
            {
                seriesViewModel.Producers = GetProducers();
                seriesViewModel.PrimaryGenres = GetPrimaryGenres();
                seriesViewModel.SecondaryGenres = GetSecondaryGenres();
                return View(seriesViewModel);
            }

            _seriesService.Update(seriesViewModel);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var series = _seriesService.GetById(id);
            if (series == null)
            {
                return NotFound();
            }

            return View(series);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _seriesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        private List<SelectListItem> GetProducers()
        {
            var producers = _producerService.GetAllProducers();
            return producers.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();
        }

        private List<SelectListItem> GetPrimaryGenres()
        {
            var genres = _genreService.GetAllGenres();
            return genres.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();
        }

        private List<SelectListItem> GetSecondaryGenres()
        {
            var genres = _genreService.GetAllGenres();
            return genres.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();
        }
    }
}