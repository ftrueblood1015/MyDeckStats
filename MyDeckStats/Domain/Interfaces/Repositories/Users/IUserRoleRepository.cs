namespace MyDeckStats.Domain.Interfaces.Repositories.Users
{
    public interface IUserRoleRepository<T> where T : class
    {
        T Add(T entity);

        bool Delete(T entity);

        IEnumerable<T> Filter(Func<T, bool> predicate);

        IEnumerable<T> GetAll();
    }
}
