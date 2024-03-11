using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyDeckStats.Data;
using MyDeckStats.Domain.Interfaces.Repositories.Users;

namespace MyDeckStats.Repositories.Users
{
    public class UserRepository : IUserRepository<IdentityUser>
    {
        private ApplicationDbContext Context { get; set; }

        public UserRepository(ApplicationDbContext context)
        {
            Context = context;
        }


        public IdentityUser Add(IdentityUser entity)
        {
            Context.Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public bool Delete(IdentityUser entity)
        {
            Context.Set<IdentityUser>().Remove(entity);
            var results = Context.SaveChanges();

            return results > 0;
        }

        public bool DeleteById(string entityId)
        {
            var entity = GetById(entityId);

            if (entity == null)
            {
                return false;
            }

            Context.Set<IdentityUser>().Remove(entity);
            var results = Context.SaveChanges();

            return results > 0;
        }

        public IEnumerable<IdentityUser> Filter(Func<IdentityUser, bool> predicate)
        {
            return Context.Set<IdentityUser>().AsNoTracking().Where(predicate);
        }

        public IEnumerable<IdentityUser> GetAll()
        {
            return Context.Set<IdentityUser>();
        }

        public IdentityUser? GetById(string id)
        {
            return Context.Set<IdentityUser>().FirstOrDefault(x => x.Id == id);
        }

        public IdentityUser Update(IdentityUser entity)
        {
            Context.Set<IdentityUser>().Update(entity);
            Context.SaveChanges();

            return GetById(entity.Id)!;
        }
    }
}
