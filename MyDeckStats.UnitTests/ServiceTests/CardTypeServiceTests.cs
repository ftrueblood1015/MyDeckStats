using MyDeckStats.Domain.Entities.Mtg.Cards;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class CardTypeServiceTests : ServiceBaseTests<CardType>
    {
        protected override CardType CreateRandomEntity()
        {
            var entity = base.CreateRandomEntity();
            entity.MtgCardId = Guid.NewGuid();
            return entity;
        }
    }
}
