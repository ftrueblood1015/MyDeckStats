using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Mtg;
using MyDeckStats.UnitTests.MockBases;
using Shouldly;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class MtgKeywordServiceTests : ServiceBaseTests<MtgKeyword>
    {
        protected override MtgKeyword CreateRandomEntity()
        {
            var entity = base.CreateRandomEntity();
            entity.MtgCardId = Guid.NewGuid();
            return entity;
        }
    }
}
