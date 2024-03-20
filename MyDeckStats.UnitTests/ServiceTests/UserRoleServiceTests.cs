using Microsoft.AspNetCore.Identity;
using Moq;
using MyDeckStats.Domain.Interfaces.Repositories.Users;
using MyDeckStats.Services.Users;
using Shouldly;

namespace MyDeckStats.UnitTests.ServiceTests
{
    public class UserRoleServiceTests
    {
        private readonly UserRoleService UserRoleService;
        private IdentityUserRole<string> Add;
        private IdentityUserRole<string> Read;
        private IdentityUserRole<string> Update;
        private IdentityUserRole<string> Delete;

        public UserRoleServiceTests()
        {
            Add = new IdentityUserRole<string>() { UserId = "be4c47b8-d302-45be-9fdc-bb6797e8f71f", RoleId = "Add" };
            Read = new IdentityUserRole<string>() { UserId = "be4c47b8-d302-45be-9fdc-bb6797e8f71f", RoleId = "Read" };
            Update = new IdentityUserRole<string>() { UserId = "be4c47b8-d302-45be-9fdc-bb6797e8f71f", RoleId = "Update" };
            Delete = new IdentityUserRole<string>() { UserId = "be4c47b8-d302-45be-9fdc-bb6797e8f71f", RoleId = "Delete" };

            var UserRoleList = new List<IdentityUserRole<string>>() { Read, Update, Delete };

            var UserRoleRepoMock = new Mock<IUserRoleRepository<IdentityUserRole<string>>>();

            UserRoleRepoMock.Setup(x => x.Add(It.IsAny<IdentityUserRole<string>>())).Returns((IdentityUserRole<string> x) =>
            {
                UserRoleList.Add(x);
                return x;
            });

            UserRoleRepoMock.Setup(x => x.GetAll()).Returns(() => UserRoleList);

            UserRoleRepoMock.Setup(x => x.Delete(It.IsAny<IdentityUserRole<string>>())).Returns((IdentityUserRole<string> Entity) =>
            {
                UserRoleList.Remove(Entity);
                return !UserRoleList.Contains(Entity);
            });

            UserRoleRepoMock.Setup(x => x.Filter(It.IsAny<Func<IdentityUserRole<string>, Boolean>>())).Returns((Func<IdentityUserRole<string>, bool> x) => { return UserRoleList.Where(x); });

            UserRoleService = new UserRoleService(UserRoleRepoMock.Object);
        }

        [Test]
        public void UserRoleService_Can_Add()
        {
            // Act
            var result = UserRoleService.Add(Add);

            // Assert 
            result.RoleId.ShouldBe(Add.RoleId);
        }

        [Test]
        public void UserRoleService_Can_GetAll()
        {
            // Act
            var result = UserRoleService.GetAll();

            // Assert
            result.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void UserRoleService_Can_Delete()
        {
            // Act
            var result = UserRoleService.Delete(Delete);

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void UserRoleService_Can_Filter()
        {
            // Arrange
            var roleId = Read.RoleId;

            // Act
            var result = UserRoleService.Filter(x => x.RoleId == roleId);

            // Assert
            result.Count().ShouldBe(1);
        }
    }
}
