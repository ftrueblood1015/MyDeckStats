using MyDeckStats.Domain.Interfaces.Services.Scryfall;
using MyDeckStats.Domain.Models;
using MyDeckStats.Services.Scryfall;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class ScryfallMtgSetServiceTests
    {
        private readonly IScryfallMtgSetService<ScryfallMtgSet> ScryfallMtgSetService;

        public ScryfallMtgSetServiceTests()
        {
            var HttpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7140/") };
            var Scryfall = new ScryfallApiServerClient(new HttpClient { BaseAddress = new Uri("https://api.scryfall.com/") });

            ScryfallMtgSetService = new ScryfallMtgSetService(Scryfall);
        }

        [Test]
        public async Task ScryfallMtgSetService_Can_GetAll()
        {
            var result = await ScryfallMtgSetService.GetAll();

            result.ShouldNotBeNull();
        }
    }
}
