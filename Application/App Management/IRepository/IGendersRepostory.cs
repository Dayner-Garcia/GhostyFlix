using Data.Entities;

namespace Application.App_Management.IRepository
{
    public interface IGendersRepostory : IBaseRepository<Gender>
    {
        IEnumerable<Series> GetSeriesByPrimaryGenre(int genreId);
        IEnumerable<Series> GetSeriesBySecondaryGenre(int genreId);
    }
}