using MyDeckStats.Data;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;

namespace MyDeckStats.Repositories.Mtg
{
    public class MasterPurposeRepository : RepositoryBase<MasterPurpose, ApplicationDbContext>, IMasterPurposeRepository
    {
        public MasterPurposeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
