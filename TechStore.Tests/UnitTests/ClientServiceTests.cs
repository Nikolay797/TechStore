using TechStore.Core.Contracts;
using TechStore.Core.Exceptions;
using TechStore.Core.Services;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models.Account;

namespace TechStore.Tests.UnitTests
{
	[TestFixture]
	public class ClientServiceTests : UnitTestsBase
	{
		private IRepository repository;
		private IGuard guard;
		private IClientService clientService;

		[OneTimeSetUp]
		public void SetUp()
		{
			this.repository = new Repository(this.data);
			this.guard = new Guard();

			this.clientService = new ClientService(this.repository, this.guard);
		}

		[Test]
		public async Task BuyProduct_ShouldIncreaseCountOfPurchasesWhenClientExistsInDb()
		{
			var client = this.data.Clients.First();

			var countOfPurchasesBeforeBuying = client.CountOfPurchases;

			await this.clientService.BuyProduct(client.UserId);

			var countOfPurchasesAfterBuying = client.CountOfPurchases;

			Assert.That(countOfPurchasesAfterBuying, Is.EqualTo(countOfPurchasesBeforeBuying + 1));
		}

		[Test]
		public async Task BuyProduct_ShouldCreateClientAndThenIncreaseItsCountOfPurchasesWhenClientDoesNotExistInDb()
		{
			// Arrange
			var clientsCountBefore = this.data.Clients.Count();
			var userId = "NewUser"; 
			var newUser = new User { Id = userId, UserName = "TestUser" };
			this.data.Users.Add(newUser);
			this.data.SaveChanges();

			// Act
			await this.clientService.BuyProduct(userId);

			// Assert
			var clientsCountAfter = this.data.Clients.Count();
			Assert.That(clientsCountAfter, Is.EqualTo(clientsCountBefore + 1), "Клиентът не е създаден коректно.");
		}

		[Test]
		public void GetNumberOfActiveSales_ShouldThrowExceptionWhenClientDoesNotExist()
		{
			// Arrange
			var userId = "InvalidUserId";

			// Act & Assert
			Assert.ThrowsAsync<TechStoreException>(
				async () => await this.clientService.GetNumberOfActiveSales(userId),
				"Expected an exception when the client does not exist.");
		}

		[Test]
		public async Task GetNumberOfActiveSales_ShouldReturnCorrectCountWhenClientExists()
		{
			// Arrange
			var userId = "User1"; 
			var client = this.data.Clients.First(c => c.UserId == userId);

			// Act
			var result = await this.clientService.GetNumberOfActiveSales(userId);

			// Изчисляваме очаквания резултат
			var expected = client.Laptops.Count(l => !l.IsDeleted)
			               + client.Televisions.Count(t => !t.IsDeleted)
			               + client.Keyboards.Count(k => !k.IsDeleted)
			               + client.Mice.Count(m => !m.IsDeleted)
			               + client.Headphones.Count(h => !h.IsDeleted)
			               + client.SmartWatches.Count(s => !s.IsDeleted);

			// Assert
			Assert.That(result, Is.EqualTo(expected), "The number of active sales is incorrect.");
		}
	}
}