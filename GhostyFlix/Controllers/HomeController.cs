using Application.App_Management.IServices;
using Application.App_Management.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GhostyFlix.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISeriesServices _seriesServices;
        private readonly IProducersServices _producersService;
        private readonly IGendersServices _gendersService;

        public HomeController(ISeriesServices seriesServices, IProducersServices producersService,
            IGendersServices gendersService)
        {
            _seriesServices = seriesServices;
            _producersService = producersService;
            _gendersService = gendersService;
        }

        public IActionResult Index(string searchString, int? producerId, int? genreId)
        {
            var seriesList = _seriesServices.GetAllSeries();

            if (!string.IsNullOrEmpty(searchString))
            {
                seriesList = seriesList.Where(s => s.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (producerId.HasValue)
            {
                seriesList = seriesList.Where(s => s.ProducerId == producerId.Value).ToList();
            }

            if (genreId.HasValue)
            {
                seriesList = seriesList
                    .Where(s => s.PrimaryGenreId == genreId.Value || s.SecondaryGenreId == genreId.Value).ToList();
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
            ViewBag.SelectedProducerId = producerId;
            ViewBag.SelectedGenreId = genreId;
            ViewBag.IsFiltered = seriesList.Count() != model.Count;

            ViewBag.Producers = _producersService.GetAllProducers().Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.Genres = _gendersService.GetAllGenres().Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var series = _seriesServices.GetById(id);
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
    }
}