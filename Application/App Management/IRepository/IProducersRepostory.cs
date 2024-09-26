using Data.Entities;

namespace Application.App_Management.IRepository
{
    public interface IProducersRepostory : IBaseRepository<Producer>
    {
        IEnumerable<Series> GetSeriesByProducer(int producerId);
    }
}