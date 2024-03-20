using MyDeckStats.Domain.Entities.Mtg.Cards;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class MtgCardServiceTests : ServiceBaseTests<MtgCard>
    {
        protected override MtgCard CreateRandomEntity()
        {
            var entity = base.CreateRandomEntity();

            entity.OracleId = Guid.NewGuid();
            entity.ScryfallUri = Guid.NewGuid().ToString();
            entity.ColorIdentity = Guid.NewGuid().ToString();
            entity.manaCost = Guid.NewGuid().ToString();
            entity.Type = Guid.NewGuid().ToString();
            entity.OracleText = Guid.NewGuid().ToString();
            entity.Rarity = Guid.NewGuid().ToString();
            entity.Slug = Guid.NewGuid().ToString().ToUpper();
            entity.Keywords = Guid.NewGuid().ToString();

            return entity;
        }
    }
}