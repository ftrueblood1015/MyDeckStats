namespace MyDeckStats.Domain.Interfaces.Services.Users
{
    public interface IUserRoleService<T> where T : class
    {
        T Add(T entity);

        bool Delete(T entity);

        IEnumerable<T> Filter(Func<T, bool> predicate);

        IEnumerable<T> GetAll();
    }
}
