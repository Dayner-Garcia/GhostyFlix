using Data.Entities;

namespace Application.App_Management.IServices
{
    public interface IProducersServices
    {
        void CreateProducer(Producer producer);
        void UpdateProducer(Producer producer);
        void DeleteProducer(int id);
        IEnumerable<Producer> GetAllProducers();
        Producer GetProducerById(int id);
        IEnumerable<Series> GetSeriesByProducer(int producerId);
    }
}