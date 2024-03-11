using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyDeckStats.Data;
using MyDeckStats.Domain.Interfaces.Repositories.Users;

namespace MyDeckStats.Repositories.Users
{
    public class RoleRepository : IRoleRepository<IdentityRole>
    {
        private ApplicationDbContext Context { get; set; }

        public RoleRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public IdentityRole Add(IdentityRole entity)
        {
            Context.Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public bool Delete(IdentityRole entity)
        {
            Context.Set<IdentityRole>().Remove(entity);
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

            Context.Set<IdentityRole>().Remove(entity);
            var results = Context.SaveChanges();

            return results > 0;
        }

        public IEnumerable<IdentityRole> Filter(Func<IdentityRole, bool> predicate)
        {
            return Context.Set<IdentityRole>().AsNoTracking().Where(predicate);
        }

        public IEnumerable<IdentityRole> GetAll()
        {
            return Context.Set<IdentityRole>();
        }

        public IdentityRole? GetById(string id)
        {
            return Context.Set<IdentityRole>().FirstOrDefault(x => x.Id == id);
        }

        public IdentityRole Update(IdentityRole entity)
        {
            Context.Set<IdentityRole>().Update(entity);
            Context.SaveChanges();

            return GetById(entity.Id)!;
        }
    }
}
