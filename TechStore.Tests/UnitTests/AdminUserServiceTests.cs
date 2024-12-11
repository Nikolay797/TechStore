using Microsoft.AspNetCore.Identity;
using TechStore.Tests.Mocks;
using TechStore.Core.Contracts;
using TechStore.Core.Exceptions;
using TechStore.Core.Services;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models.Account;

namespace TechStore.Tests.UnitTests
{
	[TestFixture]
	public class AdminUserServiceTests : UnitTestsBase
	{
		private IRepository repository;
		private UserManager<User> userManager;
		private IGuard guard;

		private IAdminUserService adminUserService;

		[OneTimeSetUp]
		public void SetUp()
		{
			this.repository = new Repository(this.data);
			this.userManager = UserManagerMock.MockUserManager(this.data.Users.ToList());
			this.guard = new Guard();

			this.adminUserService = new AdminUserService(this.repository, this.userManager, this.guard);
		}

		[Test]
		public async Task GetAllUsersThatAreNotInTheSpecifiedRole_ShouldReturnAllUsersWhenNoRoleIdIsGiven()
		{
			var resultUsers = await this.adminUserService.GetAllUsersThatAreNotInTheSpecifiedRole(null);

			var expectedUsers = this.data.Users.OrderBy(u => u.UserName);

			Assert.That(resultUsers.Count(), Is.EqualTo(expectedUsers.Count()));

			var resultUsersFirst = resultUsers.First();

			var expectedUsersFirst = expectedUsers.First();

			Assert.That(resultUsersFirst.Id, Is.EqualTo(expectedUsersFirst.Id));
		}

		[Test]
		public async Task GetAllUsersThatAreNotInTheSpecifiedRole_ShouldReturnAllUsersThatAreNotInTheGivenRole()
		{
			var roleId = this.data.Roles.First().Id;

			var resultUsers = await this.adminUserService.GetAllUsersThatAreNotInTheSpecifiedRole(roleId);

			var expectedUsers = this.data.Users
				.Where(u => !this.data.UserRoles.Any(ur => ur.UserId == u.Id && ur.RoleId == roleId))
				.OrderBy(u => u.UserName);

			Assert.That(resultUsers.Count(), Is.EqualTo(expectedUsers.Count()));

			var resultUsersFirst = resultUsers.First();

			var expectedUsersFirst = expectedUsers.First();

			Assert.That(resultUsersFirst.Id, Is.EqualTo(expectedUsersFirst.Id));
		}

		[Test]
		public async Task PromoteToAdminAsync_ShouldWorkCorrectlyWhenTheGivenUserIdIsValid()
		{
			var user = this.data.Users.First();

			var promotedUser = await this.adminUserService.PromoteToAdminAsync(user.Id);

			Assert.That(user, Is.EqualTo(promotedUser));
		}

		[OneTimeTearDown]
		public void TearDown()
		{
			if (this.userManager is IDisposable disposableUserManager)
			{
				disposableUserManager.Dispose();
			}
		}
	}

}
