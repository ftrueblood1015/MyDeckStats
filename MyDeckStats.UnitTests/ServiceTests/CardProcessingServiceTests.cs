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
        private IMasterPurposeService MasterPurposeService;
        private ICardPurposeService CardPurposeService;

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
            var masterPurposeRepo = MockRepositoryBase.MockRepo<IMasterPurposeRepository, MasterPurpose>(new List<MasterPurpose>() { 
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Land Destruction", Name = "Land Destruction", ExcludeTerms = null, IncludeTerms = "Destroy,Target,Land",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Creature Destruction", Name = "Creature Destruction", ExcludeTerms = null, IncludeTerms = "Destroy,Target,Creature",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Artifact Destruction", Name = "Artifact Destruction", ExcludeTerms = null, IncludeTerms = "Destroy,Target,Artifact",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Enchantment Destruction", Name = "Enchantment Destruction", ExcludeTerms = null, IncludeTerms = "Destroy,Target,Enchantment",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Planeswalker Destruction", Name = "Planeswalker Destruction", ExcludeTerms = null, IncludeTerms = "Destroy,Target,Planeswalker",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Planeswalker Destruction", Name = "Planeswalker Destruction", ExcludeTerms = null, IncludeTerms = "Destroy,Target,Permanent",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Land Wipe", Name = "Land Wipe", ExcludeTerms = null, IncludeTerms = "Destroy,All,Land",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Creature Wipe", Name = "Creature Wipe", ExcludeTerms = null, IncludeTerms = "Destroy,All,Creature",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Enchantment Wipe", Name = "Enchantment Wipe", ExcludeTerms = null, IncludeTerms = "Destroy,All,Enchantment",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Artifact Wipe", Name = "Artifact Wipe", ExcludeTerms = null, IncludeTerms = "Destroy,All,Artifact",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Planeswalker Wipe", Name = "Planeswalker Wipe", ExcludeTerms = null, IncludeTerms = "Destroy,All,Planeswalker",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Board Wipe", Name = "Board Wipe", ExcludeTerms = "Non", IncludeTerms = "Destroy,All,Permanent",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Card Draw", Name = "Card Draw", ExcludeTerms = null, IncludeTerms = "Draw,Card",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Discard", Name = "Discard", ExcludeTerms = null, IncludeTerms = "Discard,Card",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Counter Spell", Name = "Counter Spell", ExcludeTerms = "Can't,Can not,Cannot", IncludeTerms = "Counter,Spell",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Token Generation", Name = "Token Generation", ExcludeTerms = null, IncludeTerms = "Put,Token",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Token Generation", Name = "Token Generation", ExcludeTerms = null, IncludeTerms = "Create,Token",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Graveyard Removal", Name = "Graveyard Removal", ExcludeTerms = "Flashback", IncludeTerms = "Exile,graveyard",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Creature Buff", Name = "Creature Buff", ExcludeTerms = null, IncludeTerms = "Creature,+,/",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Creature Buff", Name = "Creature Buff", ExcludeTerms = "1,2,3,4,5,6,7,8,9,0", IncludeTerms = "Creature,gain",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Permanent Exile", Name = "Permanent Exile", ExcludeTerms = null, IncludeTerms = "Exile,Permanent",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Creature Exile", Name = "Creature Exile", ExcludeTerms = "Gain", IncludeTerms = "Exile,Creature",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Life Gain", Name = "Life Gain", ExcludeTerms = "LifeLink", IncludeTerms = "Gain,Life",IsActive = true },
                new MasterPurpose { Id = Guid.NewGuid(), Description = "Life Drain", Name = "Life Drain", ExcludeTerms = null, IncludeTerms = "Life,Lose",IsActive = true },
            });
            var cardPurposeRepo = MockRepositoryBase.MockRepo<ICardPurposeRepository, CardPurpose>(new List<CardPurpose>() { });

            MtgCardService = new MtgCardService(mtgCardRepo.Object);
            MtgKeywordService = new MtgKeywordService(mtgKeywordRepo.Object);
            ColorIdentityService = new ColorIdentityService(colorIdentityRepo.Object);
            CardTypeService = new CardTypeService(cardTypeRepo.Object);
            MasterTypeService = new MasterTypeService(masterTypeRepo.Object);
            MasterPurposeService = new MasterPurposeService(masterPurposeRepo.Object);
            CardPurposeService = new CardPurposeService(cardPurposeRepo.Object);

            ProcessingService = new CardProcessingService(
                MtgCardService,
                MtgKeywordService,
                ColorIdentityService,
                CardTypeService,
                MasterTypeService,
                MasterPurposeService,
                CardPurposeService);
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

        [Test]
        [Order(17)]
        public void Can_Process_CardPurpose_LandDestruction_With_Existing_CardPurposes_For_Card()
        {
            // Arrange
            EmptyAllRepos();
            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", OracleText = "Sacrifice Strip Mine: Destroy target land." });
            CardPurposeService.Add(new CardPurpose() { Id = Guid.NewGuid(), Description = "Test", Name = "Test", MtgCardId = guid });

            // Act
            var result = ProcessingService.ProcessCardPurpose(guid);
            var cardPurposes = CardPurposeService.GetAll();

            // Assert
            result.ShouldBeTrue();
            cardPurposes.Count().ShouldBe(1);
        }

        [Test]
        [Order(18)]
        public void Can_Process_CardPurpose_LandDestruction_With_Existing_CardPurposes_For_Other_Cards()
        {
            // Arrange
            EmptyAllRepos();
            var guid = Guid.NewGuid();
            MtgCardService.Add(new MtgCard() { Id = guid, OracleId = guid, Name = "Test Card", Slug = "TEST CARD", OracleText = "Sacrifice Strip Mine: Destroy target land." });
            CardPurposeService.Add(new CardPurpose() { Id = Guid.NewGuid(), Description = "Test", Name = "Test", MtgCardId = Guid.NewGuid() });

            // Act
            var result = ProcessingService.ProcessCardPurpose(guid);
            var cardPurposes = CardPurposeService.GetAll();

            // Assert
            result.ShouldBeTrue();
            cardPurposes.Count().ShouldBe(2);
        }

        [Test]
        [TestCase("Destroy target Land.", 1)]
        [TestCase("Destroy target creature.", 1)]
        [TestCase("Choose one —\r\n\r\n• Destroy target Vampire.\r\n\r\n• Destroy target enchantment.", 1)]
        [TestCase("Destroy target artifact.\r\n\r\nFlashback Green (You may cast this card from your graveyard for its flashback cost. Then exile it.)", 1)]
        [TestCase("Destroy target artifact, creature, or planeswalker.", 3)]
        [TestCase("Destroy target permanent", 1)]
        [TestCase("Destroy All Lands", 1)]
        [TestCase("Destroy all creatures. They can't be regenerated.", 1)]
        [TestCase("Destroy all enchantments", 1)]
        [TestCase("Destroy all artifacts", 1)]
        [TestCase("Destroy all creatures and planeswalkers except for commanders.", 2)]
        [TestCase("Destroy all permanents", 1)]
        [TestCase("Target player draws thee cards", 1)]
        [TestCase("Target player reveals his or her hand. You choose a nonland card from it. That player discards that card. You lose 2 life.", 2)]
        [TestCase("Spells you control can't be countered by blue or black spells this turn, and creatures you control can't be the targets of blue or black spells this turn.", 0)]
        [TestCase("Sliver Queen counts as a Sliver.\r\n2: Put a Sliver token into play. Treat this token as a 1/1 colorless creature.", 1)]
        [TestCase("Whenever you cast a noncreature spell, create an X/X blue Shark creature token with flying, where X is that spell’s converted mana cost.\r\nCycling X1U (X1U, Discard this card: Draw a card.)\r\nWhen you cycle Shark Typhoon, create an X/X blue Shark creature token with flying.", 3)]
        [TestCase("Bojuka Bog enters the battlefield tapped.\r\n\r\nWhen Bojuka Bog enters the battlefield, exile target player’s graveyard.", 1)]
        [TestCase("Creatures you control get +2/+0 until end of turn. If this spell was kicked, whenever a creature you control dies this turn, draw a card.", 2)]
        [TestCase("\r\nChoose one. If you control a commander as you cast this spell, you may choose both instead.\r\n• Creatures you control gain flying, vigilance, and double strike until end of turn.\r\n• Creatures you control gain lifelink, indestructible, and protection from all colors until end of turn.", 1)]
        [TestCase("Exile target permanent with converted mana cost 1.", 1)]
        [TestCase("\r\nExile target creature. Its controller gains life equal to its power.", 2)]
        [TestCase("Whenever another creature comes into play, you gain 1 life.", 1)]
        [TestCase("Destroy target nonblack creature. It can't be regenerated. You lose life equal to that creature's toughness.\r\n", 2)]

        public void Can_Process_CardPurpose_No_Existing_CardPurposes(string oracleText, int expectedPurposes)
        {
            // Arrange
            EmptyAllRepos();
            var id = Guid.NewGuid();
            MtgCard card = new MtgCard() { Id = id, OracleId = id, Name = "Test", Slug = "TEST", OracleText = oracleText };
            MtgCardService.Add(card);

            // Act
            var result = ProcessingService.ProcessCardPurpose(card.Id);
            var cardPurposes = CardPurposeService.GetAll();

            // Assert
            result.ShouldBeTrue();
            cardPurposes.Count().ShouldBe(expectedPurposes);
        }

        private void EmptyAllRepos()
        {
            RemoveAllFromCardRepo();
            RemoveAllFromKeywordRepo();
            RemoveAllFromColorIdentityRepo();
            RemoveAllFromCardTypeRepo();
            RemoveAllFromCardPurposeRepo();
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
            CardTypeService.GetAll().Count().ShouldBe(0);
        }

        private void RemoveAllFromCardPurposeRepo()
        {
            var all = CardPurposeService.GetAll();
            all.ToList().ForEach(x => CardPurposeService.Delete(x));
            CardPurposeService.GetAll().Count().ShouldBe(0);
        }
    }
}