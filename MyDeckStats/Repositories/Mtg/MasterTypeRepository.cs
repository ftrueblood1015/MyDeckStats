using Microsoft.EntityFrameworkCore;
using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class MasterTypeRepository : RepositoryBase<MasterType, ApplicationDbContext>, IMasterTypeRepository
    {
        public MasterTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
