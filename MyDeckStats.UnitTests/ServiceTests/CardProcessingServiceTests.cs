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
        private ICardTypeService CardTypeService;
        private IMasterTypeService MasterTypeService;

        public CardProcessingServiceTests()
        {
            var mtgCardRepo = MockRepositoryBase.MockRepo<IMtgCardRepository, MtgCard>(new List<MtgCard>() { });
            var mtgKeywordRepo = MockRepositoryBase.MockRepo<IKeywordRepository, MtgKeyword>(new List<MtgKeyword>() { });
            var colorIdentityRepo = MockRepositoryBase.MockRepo<IColorIdentityRepository, ColorIdentity>(new List<ColorIdentity>() { });
            var cardTypeRepo = MockRepositoryBase.MockRepo<ICardTypeRepository, CardType>(new List<CardType>() { });
            var masterTypeRepo = MockRepositoryBase.MockRepo<IMasterTypeRepository, MasterType>(new List<MasterType>() { 
                new MasterType { Id = Guid.NewGuid(), Description = "Artifact", Name = "Artifact" },
                new MasterType { Id = Guid.NewGuid(), Description = "Creature", Name = "Creature" },
                new MasterType { Id = Guid.NewGuid(), Description = "Enchantment", Name = "Enchantment" },
                new MasterType { Id = Guid.NewGuid(), Description = "Instant", Name = "Instant" },
                new MasterType { Id = Guid.NewGuid(), Description = "Sorcery", Name = "Sorcery" },
                new MasterType { Id = Guid.NewGuid(), Description = "Battle", Name = "Battle" },
                new MasterType { Id = Guid.NewGuid(), Description = "Planeswalker", Name = "Planeswalker" },
                new MasterType { Id = Guid.NewGuid(), Description = "Land", Name = "Land" },
            });

            MtgCardService = new MtgCardService(mtgCardRepo.Object);
            MtgKeywordService = new MtgKeywordService(mtgKeywordRepo.Object);
            ColorIdentityService = new ColorIdentityService(colorIdentityRepo.Object);
            CardTypeService = new CardTypeService(cardTypeRepo.Object);
            MasterTypeService = new MasterTypeService(masterTypeRepo.Object);

            ProcessingService = new CardProcessingService(
                MtgCardService,
                MtgKeywordService,
                ColorIdentityService,
                CardTypeService,
                MasterTypeService);
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

        [Test]
        [Order(11)]
        public void Can_Process_CardTypes_No_Type()
        {
            // Arrange
            EmptyAllRepos();
            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD" });

            // Act
            var result = ProcessingService.ProcessCardTypes();
            var cardTypes = CardTypeService.GetAll();

            // Assert
            result.ShouldBeTrue();
            cardTypes.Count().ShouldBe(0);
        }

        [Test]
        [Order(12)]
        public void Can_Process_CardTypes_One_Type()
        {
            // Arrange
            EmptyAllRepos();
            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", Type = "Artifact" });

            // Act
            var result = ProcessingService.ProcessCardTypes();
            var cardTypes = CardTypeService.GetAll();

            // Assert
            result.ShouldBeTrue();
            cardTypes.Count().ShouldBe(1);
        }

        [Test]
        [Order(13)]
        public void Can_Process_CardTypes_Multiple_Types()
        {
            // Arrange
            EmptyAllRepos();
            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", Type = "Artifact Creature" });

            // Act
            var result = ProcessingService.ProcessCardTypes();
            var cardTypes = CardTypeService.GetAll();

            // Assert
            result.ShouldBeTrue();
            cardTypes.Count().ShouldBe(2);
        }

        [Test]
        [Order(14)]
        public void Can_Process_CardTypes_With_Non_Master_Type()
        {
            // Arrange
            EmptyAllRepos();
            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", Type = "NonMasterType" });

            // Act
            var result = ProcessingService.ProcessCardTypes();
            var cardTypes = CardTypeService.GetAll();

            // Assert
            result.ShouldBeTrue();
            cardTypes.Count().ShouldBe(0);
        }

        [Test]
        [Order(15)]
        public void Can_Process_CardTypes_One_Type_Without_Existing_CardTypes()
        {
            // Arrange
            EmptyAllRepos();
            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", Type = "Artifact" });
            CardTypeService.Add(new CardType { Id = Guid.NewGuid(), Name = "test", Description = "test", MtgCardId = Guid.NewGuid() });

            // Act
            var result = ProcessingService.ProcessCardTypes();
            var cardTypes = CardTypeService.GetAll();

            // Assert
            result.ShouldBeTrue();
            cardTypes.Count().ShouldBe(2);
        }

        [Test]
        [Order(16)]
        public void Can_Process_CardTypes_One_Type_With_Existing_CardTypes()
        {
            // Arrange
            EmptyAllRepos();
            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", Type = "Artifact" });
            CardTypeService.Add(new CardType { Id = Guid.NewGuid(), Name = "test", Description = "test", MtgCardId = guid });

            // Act
            var result = ProcessingService.ProcessCardTypes();
            var cardTypes = CardTypeService.GetAll();

            // Assert
            result.ShouldBeTrue();
            cardTypes.Count().ShouldBe(1);
        }

        private void EmptyAllRepos()
        {
            RemoveAllFromCardRepo();
            RemoveAllFromKeywordRepo();
            RemoveAllFromColorIdentityRepo();
            RemoveAllFromCardTypeRepo();
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

        private void RemoveAllFromCardTypeRepo()
        {
            var all = CardTypeService.GetAll();
            all.ToList().ForEach(x => CardTypeService.Delete(x));
            ColorIdentityService.GetAll().Count().ShouldBe(0);
        }
    }
}