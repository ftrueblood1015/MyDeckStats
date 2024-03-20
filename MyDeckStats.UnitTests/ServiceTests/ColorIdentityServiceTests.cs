using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Mtg;
using MyDeckStats.Domain.Interfaces.Services.Mtg;
using MyDeckStats.Services.Mtg;
using MyDeckStats.UnitTests.MockBases;
using Shouldly;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class ColorIdentityServiceTests
    {
        private ColorIdentity Add;
        private ColorIdentity Read;
        private ColorIdentity Update;
        private ColorIdentity Delete;
        private readonly IColorIdentityService ColorIdentityService;

        public ColorIdentityServiceTests()
        {
            Add = new ColorIdentity() { Id = Guid.NewGuid(), Description = "Add", Name = "Add", MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };
            Read = new ColorIdentity() { Id = Guid.NewGuid(), Description = "Read", Name = "Read", MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };
            Update = new ColorIdentity() { Id = Guid.NewGuid(), Description = "Update", Name = "Update", MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };
            Delete = new ColorIdentity() { Id = Guid.NewGuid(), Description = "Delete", Name = "Delete", MtgCardId = new Guid("0aa556a6-66aa-42f1-ba41-0001e10c20a4") };

            var ColorIdentityRepo = MockRepositoryBase.MockRepo<IColorIdentityRepository, ColorIdentity>(new List<ColorIdentity>() { Read, Update, Delete });

            ColorIdentityService = new ColorIdentityService(ColorIdentityRepo.Object);
        }

        [Test]
        public void ColorIdentityService_Can_Add()
        {
            // Act
            var result = ColorIdentityService.Add(Add);

            // Assert 
            result.Id.ShouldBe(Add.Id);
        }

        [Test]
        public void ColorIdentityService_Can_GetById()
        {
            // Act
            var result = ColorIdentityService.GetById(Read.Id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Read.Id);
        }

        [Test]
        public void ColorIdentityService_Can_GetAll()
        {
            // Act
            var result = ColorIdentityService.GetAll();

            // Assert
            result.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void ColorIdentityService_Can_Update()
        {
            // Arrange
            var colorIdentity = ColorIdentityService.GetById(Update.Id);
            colorIdentity.ShouldNotBeNull();

            // Act
            colorIdentity.Description = "UPDATED";
            var result = ColorIdentityService.Update(colorIdentity);

            // Assert
            result.Description.ShouldBe("UPDATED");
        }

        [Test]
        public void ColorIdentityService_Can_Delete()
        {
            // Arrange
            var colorIdentity = ColorIdentityService.GetById(Delete.Id);
            colorIdentity.ShouldNotBeNull();

            // Act
            var result = ColorIdentityService.Delete(colorIdentity);

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void ColorIdentityService_Can_Filter()
        {
            // Act
            var result = ColorIdentityService.Filter(x => x.Id == Read.Id);

            // Assert
            result.Count().ShouldBe(1);
        }
    }
}
