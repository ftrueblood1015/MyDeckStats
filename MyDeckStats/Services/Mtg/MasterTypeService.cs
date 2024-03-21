using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Users;

namespace MyDeckStats.Services.Mtg
{
    public class MasterTypeService : ServiceBase<MasterType, IMasterTypeRepository>, IMasterTypeService
    {
        public MasterTypeService(IMasterTypeRepository repo) : base(repo)
        {
        }
    }
}
