using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyDeckStats.Data;
using MyDeckStats.Domain.Interfaces.Repositories.Users;

namespace MyDeckStats.Repositories.Users
{
    public class UserRoleRepository : IUserRoleRepository<IdentityUserRole<String>>
    {
        private ApplicationDbContext Context { get; set; }

        public UserRoleRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IdentityUserRole<string> Add(IdentityUserRole<string> entity)
        {
            Context.Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public bool Delete(IdentityUserRole<string> entity)
        {
            Context.Set<IdentityUserRole<string>>().Remove(entity);
            var results = Context.SaveChanges();

            return results > 0;
        }

        public IEnumerable<IdentityUserRole<string>> Filter(Func<IdentityUserRole<string>, bool> predicate)
        {
            return Context.Set<IdentityUserRole<string>>().AsNoTracking().Where(predicate);
        }

        public IEnumerable<IdentityUserRole<string>> GetAll()
        {
            return Context.Set<IdentityUserRole<string>>();
        }
    }
}
