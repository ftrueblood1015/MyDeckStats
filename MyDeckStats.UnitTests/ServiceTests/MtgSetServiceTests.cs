using MyDeckStats.Domain.Entities.Mtg.Cards;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class MtgSetServiceTests : ServiceBaseTests<MtgSet>
    {
        protected override MtgSet CreateRandomEntity()
        {
            var entity = base.CreateRandomEntity();

            entity.Code = Guid.NewGuid().ToString();
            entity.Uri = Guid.NewGuid().ToString();
            entity.ScryfallUri = Guid.NewGuid().ToString();
            entity.SearchUri = Guid.NewGuid().ToString();

            return entity;
        }
    }
}
