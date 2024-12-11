using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using TechStore.Core.Contracts;
using TechStore.Core.Services;
using TechStore.Infrastructure.Data.Models;
using TechStore.Infrastructure.Data.Models.Account;
using static TechStore.Infrastructure.Constants.DataConstant.ClientConstants;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;

namespace TechStore.Tests.UnitTests
{
	[TestFixture]
	public class UserServiceTests
	{
		private Mock<UserManager<User>> userManagerMock;
		private Mock<SignInManager<User>> signInManagerMock;
		private IUserService userService;

		[SetUp]
		public void SetUp()
		{
			var userStoreMock = new Mock<IUserStore<User>>();
			userManagerMock = new Mock<UserManager<User>>(
				userStoreMock.Object,
				null, null, null, null, null, null, null, null);

			var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
			var userClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<User>>();

			signInManagerMock = new Mock<SignInManager<User>>(
				userManagerMock.Object,
				httpContextAccessorMock.Object,
				userClaimsPrincipalFactoryMock.Object,
				null, null, null, null);

			userService = new UserService(userManagerMock.Object, signInManagerMock.Object);
		}

		[Test]
		public async Task ShouldBePromotedToBestUser_ShouldReturnTrueWhenCountOfPurchasesIsEqualToRequiredNumber()
		{
			// Arrange
			var user = new User { Id = "User1", UserName = "TestUser" };
			var client = new Client { UserId = user.Id, CountOfPurchases = RequiredNumberOfPurchasesToBeBestUser };

			userManagerMock.Setup(um => um.FindByIdAsync(user.Id)).ReturnsAsync(user);
			userManagerMock.Setup(um => um.AddToRoleAsync(user, BestUser)).ReturnsAsync(IdentityResult.Success);
			signInManagerMock.Setup(sm => sm.SignOutAsync()).Returns(Task.CompletedTask);
			signInManagerMock.Setup(sm => sm.SignInAsync(user, false, null)).Returns(Task.CompletedTask);

			// Act
			var result = await userService.ShouldBePromotedToBestUser(client);

			// Assert
			Assert.IsTrue(result);
			userManagerMock.Verify(um => um.AddToRoleAsync(user, BestUser), Times.Once);
			signInManagerMock.Verify(sm => sm.SignOutAsync(), Times.Once);
			signInManagerMock.Verify(sm => sm.SignInAsync(user, false, null), Times.Once);
		}

		[Test]
		public async Task ShouldBePromotedToBestUser_ShouldReturnFalseWhenCountOfPurchasesIsNotEqualToRequiredNumber()
		{
			// Arrange
			var user = new User { Id = "User1", UserName = "TestUser" };
			var client = new Client { UserId = user.Id, CountOfPurchases = RequiredNumberOfPurchasesToBeBestUser - 1 };

			// Act
			var result = await userService.ShouldBePromotedToBestUser(client);

			// Assert
			Assert.IsFalse(result);
			userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Never);
			signInManagerMock.Verify(sm => sm.SignOutAsync(), Times.Never);
			signInManagerMock.Verify(sm => sm.SignInAsync(It.IsAny<User>(), false, null), Times.Never);
		}
	}
}
