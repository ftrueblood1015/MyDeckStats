using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Scryfall;
using MyDeckStats.Domain.Models;
using MyDeckStats.Services.Mtg;
using MyDeckStats.Services.Scryfall;
using MyDeckStats.UnitTests.MockBases;
using Shouldly;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class ScryfallMtgCardServiceTests
    {
        private readonly IMtgCardService MtgCardService;
        private readonly IScryfallMtgCardService<ScryfallMtgCard> ScryfallMtgCardService;

        public ScryfallMtgCardServiceTests()
        {
            var MtgCardRepo = MockRepositoryBase.MockRepo<IMtgCardRepository, MtgCard>(new List<MtgCard>());
            MtgCardService = new MtgCardService(MtgCardRepo.Object);
            var HttpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7140/") };
            var Scryfall = new ScryfallApiServerClient(new HttpClient { BaseAddress = new Uri("https://api.scryfall.com/") });

            ScryfallMtgCardService = new ScryfallMtgCardService(Scryfall, MtgCardService);
        }

        [Test]
        public async Task ScryfallMtgCardService_Can_Get_Download_Info()
        {
            // Act
            var result = await ScryfallMtgCardService.GetDownloadUri();

            // Assert
            result.download_uri.ShouldNotBeNull();
        }

        [Test]
        public async Task ScryfallMtgCardService_Can_ImportFromFile()
        {
            // Act
            var import = await ScryfallMtgCardService.ImportFromFile();

            var cards = MtgCardService.GetAll();

            // Assert
            cards.Count().ShouldBeGreaterThan(0);
        }
    }
}
