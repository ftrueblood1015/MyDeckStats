using MyDeckStats.Domain.Entities;
using MyDeckStats.Domain.Interfaces.Repositories;
using MyDeckStats.Domain.Interfaces.Services;

namespace MyDeckStats.Services
{
    public class TrackableServiceBase<T, TRepo> : ITrackableServiceBase<T>
        where T : TrackableEntityBase
        where TRepo : IRepositoryBase<T>
    {

        protected IRepositoryBase<T> Repo { get; }

        public TrackableServiceBase(IRepositoryBase<T> repo)
        {
            Repo = repo;
        }

        public virtual T Add(T entity, string username)
        {
            try
            {
                entity.Created = DateTime.Now;
                entity.LastUpdated = DateTime.Now;
                entity.CreatedBy = username;
                entity.UpdatedBy = username;
                return Repo.Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(T entity, string username)
        {
            try
            {
                return entity.CreatedBy == username ? Repo.Delete(entity) : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteById(Guid entityId, string username)
        {
            try
            {
                var entity = GetById(entityId, username);

                if (entity == null)
                {
                    return false;
                }

                return Delete(entity, username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> Filter(Func<T, bool> predicate, string username)
        {
            try
            {
                return Repo.Filter(predicate).Where(x => x.CreatedBy == username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> GetAll(string username)
        {
            try
            {
                return Repo.GetAll().Where(x => x.CreatedBy == username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T? GetById(Guid id, string username)
        {
            try
            {
                var result = Repo.GetById(id);
                return result!.CreatedBy == username ? result : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public virtual T Update(T entity, string username)
        {
            try
            {
                if (entity.CreatedBy != username) 
                {
                    return entity;
                }

                entity.LastUpdated = DateTime.Now;
                entity.UpdatedBy = username;
                return Repo.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
