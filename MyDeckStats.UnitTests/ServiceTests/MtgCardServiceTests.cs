using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Mtg;
using MyDeckStats.UnitTests.MockBases;
using Shouldly;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class MtgCardServiceTests
    {
        private readonly IMtgCardService MtgCardService;

        public MtgCardServiceTests()
        {
            var MtgCardRepo = MockRepositoryBase.MockRepo<IMtgCardRepository, MtgCard>(new List<MtgCard>()
            {
                new MtgCard
                {
                    Id = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4"), OracleId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4"),
                    ScryfallUri = "https://scryfall.com/card/c15/233/steam-augury?utm_source=api", ColorIdentity = "R,U",
                    manaCost = "{2}{U}{R}", ConvertedManaCost = 4, Type = "Instant",
                    OracleText = "Reveal the top five cards of your library and separate them into two piles. An opponent chooses one of those piles. Put that pile into your hand and the other into your graveyard.",
                    Power = 0, Toughness = 0, Rarity = "rare", EdhrecRank = 11964, PennyRank = 9238, ProducesMana = false,
                    Slug = "STEAM AUGURY1", Name = "Steam Augury", Description = "FOR READING", Keywords = null
                },
                new MtgCard
                {
                    Id = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a3"), OracleId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a3"),
                    ScryfallUri = "https://scryfall.com/card/c15/233/steam-augury?utm_source=api", ColorIdentity = "R,U",
                    manaCost = "{2}{U}{R}", ConvertedManaCost = 4, Type = "Instant",
                    OracleText = "Reveal the top five cards of your library and separate them into two piles. An opponent chooses one of those piles. Put that pile into your hand and the other into your graveyard.",
                    Power = 0, Toughness = 0, Rarity = "rare", EdhrecRank = 11964, PennyRank = 9238, ProducesMana = false,
                    Slug = "STEAM AUGURY2", Name = "Steam Augury", Description = "FOR DELETING", Keywords = null
                },
                new MtgCard
                {
                    Id = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a2"), OracleId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a2"),
                    ScryfallUri = "https://scryfall.com/card/c15/233/steam-augury?utm_source=api",
                    ColorIdentity = "R,U", manaCost = "{2}{U}{R}", ConvertedManaCost = 4, Type = "Instant",
                    OracleText = "Reveal the top five cards of your library and separate them into two piles. An opponent chooses one of those piles. Put that pile into your hand and the other into your graveyard.",
                    Power = 0, Toughness = 0, Rarity = "rare", EdhrecRank = 11964, PennyRank = 9238, ProducesMana = false,
                    Slug = "STEAM AUGURY3", Name = "Steam Augury", Description = "FOR UPDATING", Keywords = null
                }
            });

            MtgCardService = new MtgCardService(MtgCardRepo.Object);
        }

        [Test]
        public void MtgCardService_Can_Add()
        {
            // Arrange
            var newcard = new MtgCard
            {
                Id = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a1"), OracleId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a1"),
                ScryfallUri = "https://scryfall.com/card/c15/233/steam-augury?utm_source=api",
                ColorIdentity = "R,U", manaCost = "{2}{U}{R}", ConvertedManaCost = 4, Type = "Instant",
                OracleText = "Reveal the top five cards of your library and separate them into two piles. An opponent chooses one of those piles. Put that pile into your hand and the other into your graveyard.",
                Power = 0, Toughness = 0, Rarity = "rare", EdhrecRank = 11964, PennyRank = 9238, ProducesMana = false,
                Slug = "STEAM AUGURY4", Name = "Steam Augury", Description = "FOR ADDING", Keywords = null
            };

            // Act
            var result = MtgCardService.Add(newcard);

            // Arrange 
            result.Id.ShouldBe(newcard.OracleId);
        }

        [Test]
        public void MtgCardService_Can_GetById()
        {
            // Arrange
            var id = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4");

            // Act
            var result = MtgCardService.GetById(id);

            // Assert
            result.ShouldNotBeNull();
            result.Slug.ShouldBe("STEAM AUGURY1");
        }

        [Test]
        public void MtgCardService_Can_GetAll()
        {
            // Act
            var result = MtgCardService.GetAll();

            // Assert
            result.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void MtgCardService_Can_Update()
        {
            // Arrange
            var id = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a2");
            var card = MtgCardService.GetById(id);
            card.ShouldNotBeNull();

            // Act
            card.Description = "UPDATED";
            var result = MtgCardService.Update(card);

            // Assert
            result.Description.ShouldBe("UPDATED");
        }

        [Test]
        public void MtgCardService_Can_Delete()
        {
            // Arrange
            var id = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a3");
            var card = MtgCardService.GetById(id);
            card.ShouldNotBeNull();

            // Act
            var result = MtgCardService.Delete(card);

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void MtgCardService_Can_Filter()
        {
            // Act
            var result = MtgCardService.Filter(x => x.OracleId == new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4"));

            // Assert
            result.Count().ShouldBe(1);
        }
    }
}
