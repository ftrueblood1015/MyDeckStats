using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Mtg;
using MyDeckStats.UnitTests.MockBases;
using Shouldly;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class MtgSetServiceTests
    {
        private MtgSet Add;
        private MtgSet Read;
        private MtgSet Update;
        private MtgSet Delete;
        private readonly IMtgSetService MtgSetService;

        public MtgSetServiceTests()
        {
            Add = new MtgSet() { Id = new Guid("43057fad-b1c1-437f-bc48-0045bce6d8c9"), Code = "khm", Uri = "https://api.scryfall.com/sets/43057fad-b1c1-437f-bc48-0045bce6d8c9", ScryfallUri = "https://scryfall.com/sets/khm", SearchUri = "https://api.scryfall.com/cards/search?include_extras=true&include_variations=true&order=set&q=e%3Akhm&unique=prints" };
            Read = new MtgSet() { Id = new Guid("43057fad-b1c1-437f-bc48-0045bce6d8c8"), Code = "abc", Uri = "https://api.scryfall.com/sets/43057fad-b1c1-437f-bc48-0045bce6d8c9", ScryfallUri = "https://scryfall.com/sets/khm", SearchUri = "https://api.scryfall.com/cards/search?include_extras=true&include_variations=true&order=set&q=e%3Akhm&unique=prints" };
            Update = new MtgSet() { Id = new Guid("43057fad-b1c1-437f-bc48-0045bce6d8c7"), Code = "def", Uri = "https://api.scryfall.com/sets/43057fad-b1c1-437f-bc48-0045bce6d8c9", ScryfallUri = "https://scryfall.com/sets/khm", SearchUri = "https://api.scryfall.com/cards/search?include_extras=true&include_variations=true&order=set&q=e%3Akhm&unique=prints" };
            Delete = new MtgSet() { Id = new Guid("43057fad-b1c1-437f-bc48-0045bce6d8c6"), Code = "ghi", Uri = "https://api.scryfall.com/sets/43057fad-b1c1-437f-bc48-0045bce6d8c9", ScryfallUri = "https://scryfall.com/sets/khm", SearchUri = "https://api.scryfall.com/cards/search?include_extras=true&include_variations=true&order=set&q=e%3Akhm&unique=prints" };

            var MtgSetRepo = MockRepositoryBase.MockRepo<IMtgSetRepository, MtgSet>(new List<MtgSet>() { Read, Update, Delete });

            MtgSetService = new MtgSetService(MtgSetRepo.Object);
        }

        [Test]
        public void MtgSetService_Can_Add()
        {
            // Act
            var result = MtgSetService.Add(Add);

            // Assert 
            result.Id.ShouldBe(Add.Id);
        }

        [Test]
        public void MtgSetService_Can_GetById()
        {
            // Act
            var result = MtgSetService.GetById(Read.Id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Read.Id);
        }

        [Test]
        public void MtgSetService_Can_GetAll()
        {
            // Act
            var result = MtgSetService.GetAll();

            // Assert
            result.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void MtgSetService_Can_Update()
        {
            // Arrange
            var mtgSet = MtgSetService.GetById(Update.Id);
            mtgSet.ShouldNotBeNull();

            // Act
            mtgSet.Description = "UPDATED";
            var result = MtgSetService.Update(mtgSet);

            // Assert
            result.Description.ShouldBe("UPDATED");
        }

        [Test]
        public void MtgSetService_Can_Delete()
        {
            // Arrange
            var mtgSet = MtgSetService.GetById(Delete.Id);
            mtgSet.ShouldNotBeNull();

            // Act
            var result = MtgSetService.Delete(mtgSet);

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void MtgSetService_Can_Filter()
        {
            // Act
            var result = MtgSetService.Filter(x => x.Id == Read.Id);

            // Assert
            result.Count().ShouldBe(1);
        }
    }
}
