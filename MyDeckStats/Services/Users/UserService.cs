using Microsoft.AspNetCore.Identity;
using MyDeckStats.Domain.Interfaces.Repositories.Users;
using MyDeckStats.Domain.Interfaces.Services.Users;

namespace MyDeckStats.Services.Users
{
    public class UserService : IUserService<IdentityUser>
    {
        private IUserRepository<IdentityUser> Repo {  get; set; }

        public UserService(IUserRepository<IdentityUser> repo)
        {
            Repo = repo;
        }

        public IdentityUser Add(IdentityUser entity)
        {
            try
            {
                return Repo.Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(IdentityUser entity)
        {
            try
            {
                return Repo.Delete(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteById(string entityId)
        {
            try
            {
                return Repo.DeleteById(entityId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<IdentityUser> Filter(Func<IdentityUser, bool> predicate)
        {
            try
            {
                return Repo.Filter(predicate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<IdentityUser> GetAll()
        {
            try
            {
                return Repo.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IdentityUser? GetById(string id)
        {
            try
            {
                return Repo.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IdentityUser Update(IdentityUser entity)
        {
            try
            {
                return Repo.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
