using MyDeckStats.Domain.Entities;
using MyDeckStats.Domain.Interfaces.Repositories;
using MyDeckStats.Domain.Interfaces.Services;
using MyDeckStats.Services.Users;
using MyDeckStats.UnitTests.MockBases;
using Shouldly;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class ServiceBaseTests<T>
        where T : EntityBase, new()
    {
        protected T Add;
        protected T Read;
        protected T Update;
        protected T Delete;
        protected static Random random = new Random();
        protected IServiceBase<T> Service;

        public ServiceBaseTests()
        {
            Add = CreateRandomEntity();
            Read = CreateRandomEntity();
            Update = CreateRandomEntity();
            Delete = CreateRandomEntity();

            var Repo = MockRepositoryBase.MockRepo<IRepositoryBase<T>, T>(new List<T>() { Read, Update, Delete });

            Service = new ServiceBase<T, IRepositoryBase<T>>(Repo.Object);
        }

        protected virtual T CreateRandomEntity()
        {
            return new T() { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString(), Description = Guid.NewGuid().ToString() };
        }

        [Test]
        public virtual void Can_Add()
        {
            // Act
            var result = Service.Add(Add);

            // Assert 
            result.Id.ShouldBe(Add.Id);
        }

        [Test]
        public virtual void Can_GetById()
        {
            // Act
            var result = Service.GetById(Read.Id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(Read.Id);
        }

        [Test]
        public virtual void Can_GetAll()
        {
            // Act
            var result = Service.GetAll();

            // Assert
            result.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public virtual void Can_Update()
        {
            // Arrange
            var entity = Service.GetById(Update.Id);
            entity.ShouldNotBeNull();

            // Act
            entity.Description = "UPDATED";
            var result = Service.Update(entity);

            // Assert
            result.Description.ShouldBe("UPDATED");
        }

        [Test]
        public virtual void Can_Delete()
        {
            // Arrange
            var entity = Service.GetById(Delete.Id);
            entity.ShouldNotBeNull();

            // Act
            var result = Service.Delete(entity);

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public virtual void Can_Filter()
        {
            // Act
            var result = Service.Filter(x => x.Id == Read.Id);

            // Assert
            result.Count().ShouldBe(1);
        }
    }
}
