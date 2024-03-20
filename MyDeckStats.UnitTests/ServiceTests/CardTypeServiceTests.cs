using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Mtg;
using MyDeckStats.UnitTests.MockBases;
using Shouldly;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class CardTypeServiceTests
    {
        private CardType Add;
        private CardType Read;
        private CardType Update;
        private CardType Delete;
        private readonly ICardTypeService CardTypeService;

        public CardTypeServiceTests()
        {
            Add = new CardType() { Id = Guid.NewGuid(), Description = "Add", Name = "Add", MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };
            Read = new CardType() { Id = Guid.NewGuid(), Description = "Read", Name = "Read", MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };
            Update = new CardType() { Id = Guid.NewGuid(), Description = "Update", Name = "Update", MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };
            Delete = new CardType() { Id = Guid.NewGuid(), Description = "Delete", Name = "Delete", MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };

            var CardTypeRepo = MockRepositoryBase.MockRepo<ICardTypeRepository, CardType>(new List<CardType>() { Read, Update, Delete });

            CardTypeService = new CardTypeService(CardTypeRepo.Object);
        }

        [Test]
        public void CardTypeService_Can_Add()
        {
            // Act
            var result = CardTypeService.Add(Add);

            // Assert 
            result.Id.ShouldBe(Add.Id);
        }

        [Test]
        public void CardTypeService_Can_GetById()
        {
            // Act
            var result = CardTypeService.GetById(Read.Id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Read.Id);
        }

        [Test]
        public void CardTypeService_Can_GetAll()
        {
            // Act
            var result = CardTypeService.GetAll();

            // Assert
            result.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void CardTypeService_Can_Update()
        {
            // Arrange
            var cardType = CardTypeService.GetById(Update.Id);
            cardType.ShouldNotBeNull();

            // Act
            cardType.Description = "UPDATED";
            var result = CardTypeService.Update(cardType);

            // Assert
            result.Description.ShouldBe("UPDATED");
        }

        [Test]
        public void CardTypeService_Can_Delete()
        {
            // Arrange
            var cardType = CardTypeService.GetById(Delete.Id);
            cardType.ShouldNotBeNull();

            // Act
            var result = CardTypeService.Delete(cardType);

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void CardTypeService_Can_Filter()
        {
            // Act
            var result = CardTypeService.Filter(x => x.Id == Read.Id);

            // Assert
            result.Count().ShouldBe(1);
        }
    }
}
