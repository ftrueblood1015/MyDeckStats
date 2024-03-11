namespace MyDeckStats.Domain.Interfaces.Services.Users
{
    public interface IRoleService<T> where T : class
    {
        T Add(T entity);

        bool Delete(T entity);

        bool DeleteById(string entityId);

        IEnumerable<T> Filter(Func<T, bool> predicate);

        IEnumerable<T> GetAll();

        T? GetById(string id);

        T Update(T entity);
    }
}
