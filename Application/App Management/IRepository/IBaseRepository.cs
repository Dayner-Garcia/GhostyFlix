namespace Application.App_Management.IRepository;

public interface IBaseRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void SaveChanges();
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
}