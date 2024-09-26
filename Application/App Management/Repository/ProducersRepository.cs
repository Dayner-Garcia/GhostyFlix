using Application.App_Management.IRepository;
using Data.Context;
using Data.Entities;

namespace Application.App_Management.Repository
{
    public class ProducersRepository : IProducersRepostory
    {
        private readonly AppDbContext _context;

        public ProducersRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Producer entity)
        {
            _context.Producers.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Producer entity)
        {
            var producer = _context.Producers.Find(entity.Id);
            if (producer != null)
            {
                _context.Producers.Remove(producer);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Producer> GetAll()
        {
            return _context.Producers.ToList();
        }

        public Producer GetById(int id)
        {
            return _context.Producers.Find(id);
        }

        public IEnumerable<Series> GetSeriesByProducer(int producerId)
        {
            return _context.Series.Where(s => s.ProducerId == producerId).ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Producer entity)
        {
            _context.Producers.Update(entity);
            _context.SaveChanges();
        }
    }
}