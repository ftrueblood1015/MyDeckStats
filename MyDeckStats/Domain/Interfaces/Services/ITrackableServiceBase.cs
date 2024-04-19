using MyDeckStats.Domain.Entities;

namespace MyDeckStats.Domain.Interfaces.Services
{
    public interface ITrackableServiceBase<T> 
        where T : TrackableEntityBase
    {
        T Add(T entity, string username);

        bool Delete(T entity, string username);

        bool DeleteById(Guid entityId, string username);

        IEnumerable<T> Filter(Func<T, bool> predicate, string username);

        IEnumerable<T> GetAll(string username);

        T? GetById(Guid id, string username);

        T Update(T entity, string username);
    }
}
