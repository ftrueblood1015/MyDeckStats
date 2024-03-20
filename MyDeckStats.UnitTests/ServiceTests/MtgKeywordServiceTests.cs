using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Mtg;
using MyDeckStats.UnitTests.MockBases;
using Shouldly;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class MtgKeywordServiceTests
    {
        private MtgKeyword Add;
        private MtgKeyword Read;
        private MtgKeyword Update;
        private MtgKeyword Delete;
        private readonly IMtgKeywordService MtgKeywordService;

        public MtgKeywordServiceTests()
        {
            Add = new MtgKeyword() { Id = Guid.NewGuid(), Description = "Add", Name = "Add" , MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };
            Read = new MtgKeyword() { Id = Guid.NewGuid(), Description = "Read", Name = "Read", MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };
            Update = new MtgKeyword() { Id = Guid.NewGuid(), Description = "Update", Name = "Update", MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };
            Delete = new MtgKeyword() { Id = Guid.NewGuid(), Description = "Delete", Name = "Delete", MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };

            var MtgKeywordRepo = MockRepositoryBase.MockRepo<IKeywordRepository, MtgKeyword>(new List<MtgKeyword>() { Read, Update, Delete });

            MtgKeywordService = new MtgKeywordService(MtgKeywordRepo.Object);
        }

        [Test]
        public void MtgKeywordService_Can_Add()
        {
            // Act
            var result = MtgKeywordService.Add(Add);

            // Assert 
            result.Id.ShouldBe(Add.Id);
        }

        [Test]
        public void MtgKeywordService_Can_GetById()
        {
            // Act
            var result = MtgKeywordService.GetById(Read.Id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Read.Id);
        }

        [Test]
        public void MtgKeywordService_Can_GetAll()
        {
            // Act
            var result = MtgKeywordService.GetAll();

            // Assert
            result.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void MtgKeywordService_Can_Update()
        {
            // Arrange
            var mtgKeyword = MtgKeywordService.GetById(Update.Id);
            mtgKeyword.ShouldNotBeNull();

            // Act
            mtgKeyword.Description = "UPDATED";
            var result = MtgKeywordService.Update(mtgKeyword);

            // Assert
            result.Description.ShouldBe("UPDATED");
        }

        [Test]
        public void MtgKeywordService_Can_Delete()
        {
            // Arrange
            var mtgKeyword = MtgKeywordService.GetById(Delete.Id);
            mtgKeyword.ShouldNotBeNull();

            // Act
            var result = MtgKeywordService.Delete(mtgKeyword);

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void MtgKeywordService_Can_Filter()
        {
            // Act
            var result = MtgKeywordService.Filter(x => x.Id == Read.Id);

            // Assert
            result.Count().ShouldBe(1);
        }
    }
}
