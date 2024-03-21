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
        private IColorIdentityService ColorIdentityService;

        public CardProcessingServiceTests()
        {
            var mtgCardRepo = MockRepositoryBase.MockRepo<IMtgCardRepository, MtgCard>(new List<MtgCard>() { });
            var mtgKeywordRepo = MockRepositoryBase.MockRepo<IKeywordRepository, MtgKeyword>(new List<MtgKeyword>() { });
            var colorIdentityRepo = MockRepositoryBase.MockRepo<IColorIdentityRepository, ColorIdentity>(new List<ColorIdentity>() { });

            MtgCardService = new MtgCardService(mtgCardRepo.Object);
            MtgKeywordService = new MtgKeywordService(mtgKeywordRepo.Object);
            ColorIdentityService = new ColorIdentityService(colorIdentityRepo.Object);

            ProcessingService = new CardProcessingService(MtgCardService, MtgKeywordService, ColorIdentityService);
        }

        [Test]
        [Order(1)]
        public void Can_Process_Keywords_When_No_Keyword_Entities_Exist()
        {
            // Arrange
            EmptyAllRepos();

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
            EmptyAllRepos();

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
            EmptyAllRepos();

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
            EmptyAllRepos();

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
            EmptyAllRepos();

            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", Keywords = "First Strike, Haste," });

            // Act
            var result = ProcessingService.ProcessCardKeywords();
            var keywords = MtgKeywordService.GetAll();

            // Assert
            result.ShouldBeTrue();
            keywords.Count().ShouldBe(2);
        }

        [Test]
        [Order(6)]
        public void Can_Process_ColorIdentities_When_No_ColorIdentities_Exist()
        {
            // Arrange
            EmptyAllRepos();

            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", ColorIdentity = "U,B" });

            // Act
            var result = ProcessingService.ProcessCardColorIdentities();
            var colorIdentities = ColorIdentityService.GetAll();

            // Assert
            result.ShouldBeTrue();
            colorIdentities.Count().ShouldBe(2);
        }

        [Test]
        [Order(7)]
        public void Can_Process_ColorIdentities_When_No_ColorIdentities_Entities_Exist_For_Card()
        {
            // Arrange
            EmptyAllRepos();

            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", ColorIdentity = "U,B" });
            ColorIdentityService.Add(new ColorIdentity() { Id = Guid.NewGuid(), Name = "G", Description = "g", MtgCardId = Guid.NewGuid() });

            // Act
            var result = ProcessingService.ProcessCardColorIdentities();
            var colorIdentities = ColorIdentityService.GetAll();

            // Assert
            result.ShouldBeTrue();
            colorIdentities.Count().ShouldBe(3);
        }

        [Test]
        [Order(8)]
        public void Can_Process_ColorIdentities_When_ColorIdentities_Entities_Exist_For_Card()
        {
            // Arrange
            EmptyAllRepos();

            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", ColorIdentity = "U,B" });
            ColorIdentityService.Add(new ColorIdentity() { Id = Guid.NewGuid(), Name = "Test", Description = "Test", MtgCardId = guid });

            // Act
            var result = ProcessingService.ProcessCardColorIdentities();
            var colorIdentities = ColorIdentityService.GetAll();

            // Assert
            result.ShouldBeTrue();
            colorIdentities.Count().ShouldBe(2);
        }

        [Test]
        [Order(9)]
        public void Can_Process_ColorIdentities_With_Duplicate_ColorIdentity()
        {
            // Arrange
            EmptyAllRepos();

            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", ColorIdentity = "U,B,B" });

            // Act
            var result = ProcessingService.ProcessCardColorIdentities();
            var colorIdentities = ColorIdentityService.GetAll();

            // Assert
            result.ShouldBeTrue();
            colorIdentities.Count().ShouldBe(2);
        }

        [Test]
        [Order(10)]
        public void Can_Process_ColorIdentities_With_Empty_Or_Null_ColorIdentity()
        {
            // Arrange
            EmptyAllRepos();

            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", ColorIdentity = "U,B," });

            // Act
            var result = ProcessingService.ProcessCardColorIdentities();
            var colorIdentities = ColorIdentityService.GetAll();

            // Assert
            result.ShouldBeTrue();
            colorIdentities.Count().ShouldBe(2);
        }

        private void EmptyAllRepos()
        {
            RemoveAllFromCardRepo();
            RemoveAllFromKeywordRepo();
            RemoveAllFromColorIdentityRepo();
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

        private void RemoveAllFromColorIdentityRepo()
        {
            var all = ColorIdentityService.GetAll();
            all.ToList().ForEach(x => ColorIdentityService.Delete(x));
            ColorIdentityService.GetAll().Count().ShouldBe(0);
        }
    }
}