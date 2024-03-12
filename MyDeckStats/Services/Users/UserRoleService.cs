using Microsoft.AspNetCore.Identity;
using MyDeckStats.Domain.Interfaces.Repositories.Users;
using MyDeckStats.Domain.Interfaces.Services.Users;

namespace MyDeckStats.Services.Users
{
    public class UserRoleService : IUserRoleService<IdentityUserRole<String>>
    {
        private IUserRoleRepository<IdentityUserRole<String>> Repo;

        public UserRoleService(IUserRoleRepository<IdentityUserRole<String>> repo)
        {
            Repo = repo;
        }

        public IdentityUserRole<string> Add(IdentityUserRole<string> entity)
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

        public bool Delete(IdentityUserRole<string> entity)
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

        public IEnumerable<IdentityUserRole<string>> Filter(Func<IdentityUserRole<string>, bool> predicate)
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

        public IEnumerable<IdentityUserRole<string>> GetAll()
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
    }
}
