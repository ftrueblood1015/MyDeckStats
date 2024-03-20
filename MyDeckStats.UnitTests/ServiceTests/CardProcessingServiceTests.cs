using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Mtg;
using MyDeckStats.UnitTests.MockBases;
using Shouldly;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class CardProcessingServiceTests
    {
        private ICardProcessingService ProcessingService;
        private IMtgCardService MtgCardService;
        private IMtgKeywordService MtgKeywordService;

        public CardProcessingServiceTests()
        {
            var mtgCardRepo = MockRepositoryBase.MockRepo<IMtgCardRepository, MtgCard>(new List<MtgCard>() { });
            var mtgKeywordRepo = MockRepositoryBase.MockRepo<IKeywordRepository, MtgKeyword>(new List<MtgKeyword>() { });

            MtgCardService = new MtgCardService(mtgCardRepo.Object);
            MtgKeywordService = new MtgKeywordService(mtgKeywordRepo.Object);
            ProcessingService = new CardProcessingService(MtgCardService, MtgKeywordService);
        }

        [Test]
        [Order(1)]
        public void Can_Process_Keywords_When_No_Keyword_Entities_Exist()
        {
            // Arrange
            RemoveAllFromCardRepo();
            RemoveAllFromKeywordRepo();

            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", Keywords = "First Strike, Haste" });

            // Act
            var result = ProcessingService.ProcessCardKeywords();
            var keywords = MtgKeywordService.GetAll();

            // Assert
            result.ShouldBeTrue();
            keywords.Count().ShouldBe(2);
        }

        [Test]
        [Order(2)]
        public void Can_Process_Keywords_When_No_Keyword_Entities_Exist_For_Card()
        {
            // Arrange
            RemoveAllFromCardRepo();
            RemoveAllFromKeywordRepo();

            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", Keywords = "First Strike, Haste" });
            MtgKeywordService.Add(new MtgKeyword() { Id = Guid.NewGuid(), Name = "Test", Description = "Test", MtgCardId = Guid.NewGuid() });

            // Act
            var result = ProcessingService.ProcessCardKeywords();
            var keywords = MtgKeywordService.GetAll();

            // Assert
            result.ShouldBeTrue();
            keywords.Count().ShouldBe(3);
        }

        [Test]
        [Order(3)]
        public void Can_Process_Keywords_When_Keyword_Entities_Exist_For_Card()
        {
            // Arrange
            RemoveAllFromCardRepo();
            RemoveAllFromKeywordRepo();

            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", Keywords = "First Strike, Haste" });
            MtgKeywordService.Add(new MtgKeyword() { Id = Guid.NewGuid(), Name = "Test", Description = "Test", MtgCardId = guid });

            // Act
            var result = ProcessingService.ProcessCardKeywords();
            var keywords = MtgKeywordService.GetAll();

            // Assert
            result.ShouldBeTrue();
            keywords.Count().ShouldBe(2);
        }

        [Test]
        [Order(4)]
        public void Can_Process_Keywords_With_Duplicate_Keyword()
        {
            // Arrange
            RemoveAllFromCardRepo();
            RemoveAllFromKeywordRepo();

            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", Keywords = "First Strike, Haste, Haste" });

            // Act
            var result = ProcessingService.ProcessCardKeywords();
            var keywords = MtgKeywordService.GetAll();

            // Assert
            result.ShouldBeTrue();
            keywords.Count().ShouldBe(2);
        }

        [Test]
        [Order(5)]
        public void Can_Process_Keywords_With_Empty_Or_Null_Keyword()
        {
            // Arrange
            RemoveAllFromCardRepo();
            RemoveAllFromKeywordRepo();

            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", Keywords = "First Strike, Haste," });

            // Act
            var result = ProcessingService.ProcessCardKeywords();
            var keywords = MtgKeywordService.GetAll();

            // Assert
            result.ShouldBeTrue();
            keywords.Count().ShouldBe(2);
        }

        private void RemoveAllFromCardRepo()
        {
            var all = MtgCardService.GetAll();
            all.ToList().ForEach(x => MtgCardService.Delete(x));
            MtgCardService.GetAll().Count().ShouldBe(0);
        }

        private void RemoveAllFromKeywordRepo()
        {
            var all = MtgKeywordService.GetAll();
            all.ToList().ForEach(x => MtgKeywordService.Delete(x));
            MtgKeywordService.GetAll().Count().ShouldBe(0);
        }
    }
}