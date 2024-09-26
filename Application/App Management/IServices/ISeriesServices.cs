using Application.App_Management.ViewModels;
using Data.Entities;

namespace Application.App_Management.IServices
{
    public interface ISeriesServices
    {
        public Series GetSeriesById(int id);
        void Add(SeriesViewModel seriesViewModel);
        void Update(SeriesViewModel seriesViewModel);
        void Delete(int id);
        SeriesViewModel? GetById(int id);
        IEnumerable<SeriesViewModel> GetAllSeries();
        Task<IEnumerable<SeriesViewModel>> GetAllSeriesAsync();
        Task<IEnumerable<SeriesViewModel>> GetSeriesByProducerAsync(int producerId);
        Task<IEnumerable<SeriesViewModel>> SearchByNameAsync(string name);
        Task<IEnumerable<SeriesViewModel>> GetSeriesByGenreAsync(int genreID);
    }
}