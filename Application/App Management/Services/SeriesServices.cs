using Application.App_Management.IRepository;
using Application.App_Management.IServices;
using Application.App_Management.ViewModels;
using Data.Context;
using Data.Entities;

namespace Application.App_Management.Services
{
    public class SeriesServices : ISeriesServices
    {
        private readonly ISeriesRepository _seriesRepository;
        private readonly AppDbContext _context;

        public SeriesServices(ISeriesRepository seriesRepository, AppDbContext application)
        {
            _seriesRepository = seriesRepository;
            _context = application;
        }

        public void Add(SeriesViewModel seriesViewModel)
        {
            var s = new Series
            {
                Id = 0,
                Name = seriesViewModel.Name,
                ProducerId = seriesViewModel.ProducerId,
                GenderPrimaryId = seriesViewModel.PrimaryGenreId,
                GenderSecondaryId = seriesViewModel.SecondaryGenreId,
                LinkVideo = seriesViewModel.LinkVideo,
                CoverImage = seriesViewModel.CoverImage
            };
            _seriesRepository.Create(s);
        }

        public void Delete(int id)
        {
            var series = _context.Series.Find(id);
            if (series != null)
            {
                _context.Series.Remove(series);
                _context.SaveChanges();
            }
        }

        public IEnumerable<SeriesViewModel> GetAllSeries()
        {
            return _seriesRepository.GetAll().Select(s => new SeriesViewModel
            {
                Id = s.Id,
                Name = s.Name,
                CoverImage = s.CoverImage,
                LinkVideo = s.LinkVideo,
                ProducerId = s.ProducerId,
                ProducerName = s.Producer?.Name,
                PrimaryGenreId = s.GenderPrimaryId,
                PrimaryGenreName = s.GenderPrimary?.Name,
                SecondaryGenreId = s.GenderSecondaryId,
                SecondaryGenreName = s.GenderSecondary?.Name
            });
        }

        public async Task<IEnumerable<SeriesViewModel>> GetAllSeriesAsync()
        {
            return await _seriesRepository.GetAllSeriesAsync();
        }

        public SeriesViewModel? GetById(int id)
        {
            var s = _seriesRepository.GetById(id);
            if (s == null)
            {
                return null;
            }

            var producer = _context.Producers.Find(s.ProducerId);
            var primaryGenre = _context.Genders.Find(s.GenderPrimaryId);
            var secondaryGenre = _context.Genders.Find(s.GenderSecondaryId);

            return new SeriesViewModel
            {
                Id = s.Id,
                Name = s.Name,
                ProducerId = s.ProducerId,
                ProducerName = producer?.Name,
                PrimaryGenreId = s.GenderPrimaryId,
                PrimaryGenreName = primaryGenre?.Name,
                SecondaryGenreId = s.GenderSecondaryId,
                SecondaryGenreName = secondaryGenre?.Name,
                LinkVideo = s.LinkVideo,
                CoverImage = s.CoverImage
            };
        }

        public async Task<IEnumerable<SeriesViewModel>> GetSeriesByGenreAsync(int genreID)
        {
            return await _seriesRepository.GetSeriesByGenre(genreID);
        }

        public Series GetSeriesById(int id)
        {
            return _context.Series.FirstOrDefault(s => s.Id == id);
        }

        public async Task<IEnumerable<SeriesViewModel>> GetSeriesByProducerAsync(int producerId)
        {
            return await _seriesRepository.GetSeriesByProducer(producerId);
        }

        public async Task<IEnumerable<SeriesViewModel>> SearchByNameAsync(string name)
        {
            return await _seriesRepository.SearchByName(name);
        }

        public void Update(SeriesViewModel seriesViewModel)
        {
            var s = new Series
            {
                Id = seriesViewModel.Id,
                Name = seriesViewModel.Name,
                ProducerId = seriesViewModel.ProducerId,
                GenderPrimaryId = seriesViewModel.PrimaryGenreId,
                GenderSecondaryId = seriesViewModel.SecondaryGenreId,
                LinkVideo = seriesViewModel.LinkVideo,
                CoverImage = seriesViewModel.CoverImage,
            };
            _seriesRepository.Update(s);
        }
    }
}