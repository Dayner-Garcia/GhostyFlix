using Application.App_Management.IRepository;
using Application.App_Management.ViewModels;
using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.App_Management.Repository
{
    public class SeriesRepository : ISeriesRepository
    {
        private readonly AppDbContext _context;

        public SeriesRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Series entity)
        {
            _context.Series.Add(entity);
            SaveChanges();
        }

        public void Delete(Series entity)
        {
            _context.Series.Remove(entity);
            SaveChanges();
        }

        public IEnumerable<Series> GetAll()
        {
            return _context.Series.Include(s => s.Producer)
                .Include(s => s.GenderPrimary)
                .Include(s => s.GenderSecondary)
                .ToList();
        }

        public async Task<IEnumerable<SeriesViewModel>> GetAllSeriesAsync()
        {
            return await _context.Series
                .Include(s => s.Producer)
                .Include(s => s.GenderPrimary)
                .Include(s => s.GenderSecondary)
                .Select(s => new SeriesViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    LinkVideo = s.LinkVideo,
                    CoverImage = s.CoverImage,
                    ProducerId = s.ProducerId,
                    ProducerName = s.Producer.Name,
                    PrimaryGenreId = s.GenderPrimaryId,
                    PrimaryGenreName = s.GenderPrimary.Name,
                    SecondaryGenreId = s.GenderSecondaryId,
                    SecondaryGenreName = s.GenderSecondary != null ? s.GenderSecondary.Name : null
                }).ToListAsync();
        }

        public Series GetById(int id)
        {
            return _context.Series.Include(s => s.Producer)
                .Include(s => s.GenderPrimary)
                .Include(s => s.GenderSecondary)
                .FirstOrDefault(s => s.Id == id);
        }

        // completar
        public Task<IEnumerable<SeriesViewModel>> GetDetails()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<SeriesViewModel>> GetSeriesByGenre(int genreId)
        {
            return await _context.Series
                .Where(s => s.GenderPrimaryId == genreId || s.GenderSecondaryId == genreId)
                .Select(s => new SeriesViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    LinkVideo = s.LinkVideo,
                    CoverImage = s.CoverImage,
                    ProducerId = s.ProducerId,
                    PrimaryGenreId = s.GenderPrimaryId,
                    SecondaryGenreId = s.GenderSecondaryId,
                }).ToListAsync();
        }

        public async Task<IEnumerable<SeriesViewModel>> GetSeriesByProducer(int producerId)
        {
            return await _context.Series
                .Where(s => s.ProducerId == producerId)
                .Select(s => new SeriesViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    LinkVideo = s.LinkVideo,
                    CoverImage = s.CoverImage,
                    ProducerId = s.ProducerId,
                    PrimaryGenreId = s.GenderPrimaryId,
                    SecondaryGenreId = s.GenderSecondaryId,
                }).ToListAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<IEnumerable<SeriesViewModel>> SearchByName(string name)
        {
            return await _context.Series
                .Where(s => s.Name.Contains(name))
                .Select(s => new SeriesViewModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    LinkVideo = s.LinkVideo,
                    CoverImage = s.CoverImage,
                    ProducerId = s.ProducerId,
                    PrimaryGenreId = s.GenderPrimaryId,
                    SecondaryGenreId = s.GenderSecondaryId,
                }).ToListAsync();
        }

        public void Update(Series entity)
        {
            _context.Update(entity);
            SaveChanges();
        }

        public void UpdateRange(IEnumerable<Series> series)
        {
            _context.Series.UpdateRange(series);
            _context.SaveChanges();
        }
    }
}