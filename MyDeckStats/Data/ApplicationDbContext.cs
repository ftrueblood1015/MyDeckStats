using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyDeckStats.Domain.Entities.Mtg.Cards;

namespace MyDeckStats.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        DbSet<MtgSet> MtgSets => Set<MtgSet>();
    }
}
