using Application.App_Management.IRepository;
using Application.App_Management.IServices;
using Data.Entities;

namespace Application.App_Management.Services
{
    public class GendersServices : IGendersServices
    {
        private readonly IGendersRepostory _gendersRepository;

        public GendersServices(IGendersRepostory gendersRepostory)
        {
            _gendersRepository = gendersRepostory;
        }

        public void CreateGenre(Gender genre)
        {
            _gendersRepository.Create(genre);
        }

        public void DeleteGenre(int id)
        {
            var genre = _gendersRepository.GetById(id);
            if (genre != null)
            {
                _gendersRepository.Delete(genre);
            }
        }

        public IEnumerable<Gender> GetAllGenres()
        {
            return _gendersRepository.GetAll();
        }

        public Gender GetGenreById(int id)
        {
            return _gendersRepository.GetById(id);
        }

        public IEnumerable<Series> GetSeriesByPrimaryGenre(int genreId)
        {
            return _gendersRepository.GetSeriesByPrimaryGenre(genreId);
        }

        public IEnumerable<Series> GetSeriesBySecondaryGenre(int genreId)
        {
            return _gendersRepository.GetSeriesBySecondaryGenre(genreId);
        }

        public void UpdateSeriesRange(IEnumerable<Series> seriesUsingGenre)
        {
            throw new NotImplementedException();
        }

        public void UpdateGenre(Gender genre)
        {
            _gendersRepository.Update(genre);
        }
    }
}