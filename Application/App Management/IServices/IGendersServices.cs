using Data.Entities;

namespace Application.App_Management.IServices
{
    public interface IGendersServices
    {
        void CreateGenre(Gender genre);
        void UpdateGenre(Gender genre);
        void DeleteGenre(int id);
        IEnumerable<Gender> GetAllGenres();
        Gender GetGenreById(int id);
        IEnumerable<Series> GetSeriesByPrimaryGenre(int genreId);
        IEnumerable<Series> GetSeriesBySecondaryGenre(int genreId);
        void UpdateSeriesRange(IEnumerable<Series> seriesUsingGenre);
    }
}