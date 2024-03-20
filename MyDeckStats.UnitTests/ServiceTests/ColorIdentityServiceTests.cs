using MyDeckStats.Domain.Entities.Mtg.Cards;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class ColorIdentityServiceTests : ServiceBaseTests<ColorIdentity>
    {
        protected override ColorIdentity CreateRandomEntity()
        {
            var entity = base.CreateRandomEntity();
            entity.MtgCardId = Guid.NewGuid();
            return entity;
        }
    }
}
