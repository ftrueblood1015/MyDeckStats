using Microsoft.AspNetCore.Identity;
using Moq;
using MudBlazor;
using MyDeckStats.Domain.Entities.Mtg.Cards;
using MyDeckStats.Domain.Interfaces.Repositories.Users;
using MyDeckStats.Domain.Interfaces.Services.Users;
using MyDeckStats.Services.Mtg;
using MyDeckStats.Services.Users;
using MyDeckStats.UnitTests.MockBases;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Admin	Admin	ADMIN	NULL
namespace MyDeckStats.UnitTests.ServiceTests
{
    public class RoleServiceTests
    {
        private readonly IRoleService<IdentityRole> RoleService;
        private IdentityRole ReadRole;
        private IdentityRole DeleteRole;
        private IdentityRole UpdateRole;
        private IdentityRole AddRole;

        public RoleServiceTests()
        {
            ReadRole = new IdentityRole() { Id = "ReadRole", Name = "ReadRole", NormalizedName = "READROLE"};
            DeleteRole = new IdentityRole() { Id = "DeleteRole", Name = "DeleteRole", NormalizedName = "DELETEROLE"};
            UpdateRole = new IdentityRole() { Id = "UpdateRole", Name = "UpdateRole", NormalizedName = "UPDATEROLE"};
            AddRole = new IdentityRole() { Id = "AddRole", Name = "AddRole", NormalizedName = "ADDROLE"};

            var RoleList = new List<IdentityRole>() { ReadRole, DeleteRole, UpdateRole };

            var RoleRepoMock = new Mock<IRoleRepository<IdentityRole>>();

            RoleRepoMock.Setup(x => x.Add(It.IsAny<IdentityRole>())).Returns((IdentityRole x) =>
            {
                RoleList.Add(x);
                return x;
            });

            RoleRepoMock.Setup(x => x.GetById(It.IsAny<string>())).Returns((string x) => { return RoleList.FirstOrDefault(y => y.Id == x); });

            RoleRepoMock.Setup(x => x.GetAll()).Returns(() => RoleList);

            RoleRepoMock.Setup(x => x.Update(It.IsAny<IdentityRole>())).Returns((IdentityRole Entity) => { return Entity; });

            RoleRepoMock.Setup(x => x.Delete(It.IsAny<IdentityRole>())).Returns((IdentityRole Entity) =>
            {
                RoleList.Remove(Entity);
                return !RoleList.Contains(Entity);
            });

            RoleRepoMock.Setup(x => x.Filter(It.IsAny<Func<IdentityRole, Boolean>>())).Returns((Func<IdentityRole, bool> x) => { return RoleList.Where(x); });

            RoleService = new RoleService(RoleRepoMock.Object);
        }

        [Test]
        public void RoleService_Can_Add()
        {
            // Act
            var result = RoleService.Add(AddRole);

            // Arrange 
            result.Id.ShouldBe(AddRole.Id);
        }

        [Test]
        public void RoleService_Can_GetById()
        {
            // Arrange
            var id = ReadRole.Id;

            // Act
            var result = RoleService.GetById(id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(ReadRole.Id);
        }

        [Test]
        public void RoleService_Can_GetAll()
        {
            // Act
            var result = RoleService.GetAll();

            // Assert
            result.Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void RoleService_Can_Update()
        {
            // Arrange
            var id = UpdateRole.Id;
            var role = RoleService.GetById(id);
            role.ShouldNotBeNull();

            // Act
            role.NormalizedName = "UPDATED";
            var result = RoleService.Update(role);

            // Assert
            result.NormalizedName.ShouldBe("UPDATED");
        }

        [Test]
        public void RoleService_Can_Delete()
        {
            // Arrange
            var id = DeleteRole.Id;
            var role = RoleService.GetById(id);
            role.ShouldNotBeNull();

            // Act
            var result = RoleService.Delete(role);

            // Assert
            result.ShouldBeTrue();
        }

        [Test]
        public void RoleService_Can_Filter()
        {
            // Arrange
            var id = ReadRole.Id;

            // Act
            var result = RoleService.Filter(x => x.Id == id);

            // Assert
            result.Count().ShouldBe(1);
        }
    }
}
