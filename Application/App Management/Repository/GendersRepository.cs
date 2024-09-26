using Application.App_Management.IRepository;
using Data.Context;
using Data.Entities;

namespace Application.App_Management.Repository
{
    public class GendersRepository : IGendersRepostory
    {
        private readonly AppDbContext _context;

        public GendersRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Gender entity)
        {
            _context.Genders.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Gender entity)
        {
            _context.Genders.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Gender> GetAll()
        {
            return _context.Genders.ToList();
        }

        public Gender GetById(int id)
        {
            return _context.Genders.Find(id);
        }

        public IEnumerable<Series> GetSeriesByPrimaryGenre(int genreId)
        {
            return _context.Series.Where(s => s.GenderPrimaryId == genreId).ToList();
        }

        public IEnumerable<Series> GetSeriesBySecondaryGenre(int genreId)
        {
            return _context.Series.Where(s => s.GenderSecondaryId == genreId).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Gender entity)
        {
            _context.Genders.Update(entity);
            _context.SaveChanges();
        }
    }
}