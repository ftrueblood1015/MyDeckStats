using Microsoft.AspNetCore.Identity;
using MyDeckStats.Domain.Interfaces.Repositories.Users;
using MyDeckStats.Domain.Interfaces.Services.Users;

namespace MyDeckStats.Services.Users
{
    public class RoleService : IRoleService<IdentityRole>
    {
        private IRoleRepository<IdentityRole> Repo;

        public RoleService(IRoleRepository<IdentityRole> repo)
        {
            Repo = repo;
        }

        public IdentityRole Add(IdentityRole entity)
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

        public bool Delete(IdentityRole entity)
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

        public IEnumerable<IdentityRole> Filter(Func<IdentityRole, bool> predicate)
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

        public IEnumerable<IdentityRole> GetAll()
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

        public IdentityRole? GetById(string id)
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

        public IdentityRole Update(IdentityRole entity)
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
