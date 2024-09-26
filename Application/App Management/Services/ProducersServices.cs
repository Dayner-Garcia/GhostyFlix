using Application.App_Management.IRepository;
using Application.App_Management.IServices;
using Data.Entities;

namespace Application.App_Management.Services
{
    public class ProducersServices : IProducersServices
    {
        private readonly IProducersRepostory _producersRepository;

        public ProducersServices(IProducersRepostory producersRepostory)
        {
            _producersRepository = producersRepostory;
        }

        public void CreateProducer(Producer producer)
        {
            _producersRepository.Create(producer);
        }

        public void DeleteProducer(int id)
        {
            var producer = _producersRepository.GetById(id);
            if (producer != null)
            {
                _producersRepository.Delete(producer);
            }
        }

        public IEnumerable<Producer> GetAllProducers()
        {
            return _producersRepository.GetAll();
        }

        public Producer GetProducerById(int id)
        {
            return _producersRepository.GetById(id);
        }

        public IEnumerable<Series> GetSeriesByProducer(int producerId)
        {
            return _producersRepository.GetSeriesByProducer(producerId);
        }

        public void UpdateProducer(Producer producer)
        {
            _producersRepository.Update(producer);
        }
    }
}