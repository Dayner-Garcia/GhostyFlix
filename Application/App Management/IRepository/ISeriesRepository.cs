using Application.App_Management.ViewModels;
using Data.Entities;

namespace Application.App_Management.IRepository;

public interface ISeriesRepository : IBaseRepository<Series>
{
    Task<IEnumerable<SeriesViewModel>> GetDetails();
    Task<IEnumerable<SeriesViewModel>> GetAllSeriesAsync();
    Task<IEnumerable<SeriesViewModel>> SearchByName(string name);
    Task<IEnumerable<SeriesViewModel>> GetSeriesByProducer(int producerId);
    Task<IEnumerable<SeriesViewModel>> GetSeriesByGenre(int genreId);
}