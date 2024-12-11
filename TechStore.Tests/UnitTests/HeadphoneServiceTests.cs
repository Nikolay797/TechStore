using System.Globalization;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Exceptions;
using TechStore.Core.Models.Headphone;
using TechStore.Core.Services;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;

namespace TechStore.Tests.UnitTests
{
	[TestFixture]
	public class HeadphoneServiceTests : UnitTestsBase
	{
		private IRepository repository;
		private IGuard guard;
		private IHeadphoneService headphoneService;

		[OneTimeSetUp]
		public void SetUp()
		{
			this.repository = new Repository(this.data);
			this.guard = new Guard();

			this.headphoneService = new HeadphoneService(this.repository, this.guard);
		}

		[TestCase(null)]
		[TestCase("NewColor")]
		public async Task AddHeadphoneAsync_ShouldAddHeadphone(string? color)
		{
			// Arrange
			var userId = this.data.Users.FirstOrDefault(u => u.Id == "User1")?.Id;

			// Уверяваме се, че User1 има свързан Client
			var client = this.data.Clients.FirstOrDefault(c => c.UserId == userId);
			if (client == null)
			{
				Assert.Fail("Test failed because no client is associated with the given user.");
			}

			var initialCount = this.data.Headphones.Count();

			var headphone = new HeadphoneImportViewModel
			{
				ImageUrl = null,
				Warranty = 1,
				Price = 1111.00M,
				Quantity = 1,
				IsWireless = true,
				HasMicrophone = true,
				Brand = "NewBrand",
				Type = "NewType",
				Color = color,
			};

			// Act
			var headphoneId = await this.headphoneService.AddHeadphoneAsync(headphone, userId);

			// Assert
			var finalCount = this.data.Headphones.Count();
			Assert.That(finalCount, Is.EqualTo(initialCount + 1), "The headphone count in the database did not increase by 1.");

			var addedHeadphone = this.data.Headphones.First(h => h.Id == headphoneId);
			Assert.Multiple(() =>
			{
				Assert.That(addedHeadphone, Is.Not.Null);
				Assert.That(addedHeadphone.ImageUrl, Is.Null);
				Assert.That(addedHeadphone.Warranty, Is.EqualTo(headphone.Warranty));
				Assert.That(addedHeadphone.Price, Is.EqualTo(headphone.Price));
				Assert.That(addedHeadphone.Quantity, Is.EqualTo(headphone.Quantity));
				Assert.That(addedHeadphone.IsWireless, Is.EqualTo(headphone.IsWireless));
				Assert.That(addedHeadphone.HasMicrophone, Is.EqualTo(headphone.HasMicrophone));
				Assert.That(addedHeadphone.Brand.Name, Is.EqualTo(headphone.Brand));
				Assert.That(addedHeadphone.Type.Name, Is.EqualTo(headphone.Type));
				Assert.That(addedHeadphone.Color?.Name, Is.EqualTo(color));
			});
		}


		[Test]
		public async Task DeleteHeadphoneAsync_ShouldMarkTheSpecifiedHeadphoneAsDeleted()
		{
			// Arrange
			var headphone = new HeadphoneImportViewModel()
			{
				ImageUrl = null,
				Warranty = 1,
				Price = 1111.00M,
				Quantity = 1,
				IsWireless = true,
				HasMicrophone = true,
				Brand = "NewBrand",
				Type = "NewType",
				Color = null,
			};

			var userId = this.data.Users.FirstOrDefault()?.Id;

			// Ensure the user has an associated client
			var client = new Client { UserId = userId };
			this.data.Clients.Add(client);
			this.data.SaveChanges();

			// Act
			var headphoneId = await this.headphoneService.AddHeadphoneAsync(headphone, userId);
			var addedHeadphone = this.data.Headphones.First(h => h.Id == headphoneId);

			// Assert
			Assert.That(addedHeadphone.IsDeleted, Is.False);

			await this.headphoneService.DeleteHeadphoneAsync(headphoneId);

			Assert.That(addedHeadphone.IsDeleted, Is.True);

			// Reset the state for future tests
			addedHeadphone.IsDeleted = false;
		}


		[Test]
		public async Task EditHeadphoneAsync_ShouldEditHeadphoneCorrectly()
		{
			var headphoneOrigin = this.data.Headphones.First();

			var newPrice = headphoneOrigin.Price + 1000;

			var headphone = new HeadphoneEditViewModel()
			{
				Id = headphoneOrigin.Id,
				ImageUrl = headphoneOrigin.ImageUrl,
				Warranty = headphoneOrigin.Warranty,
				Price = newPrice,
				Quantity = headphoneOrigin.Quantity,
				IsWireless = headphoneOrigin.IsWireless,
				HasMicrophone = headphoneOrigin.HasMicrophone,
				Brand = headphoneOrigin.Brand.Name,
				Type = headphoneOrigin.Type.Name,
				Color = headphoneOrigin.Color?.Name,
				Seller = headphoneOrigin.Seller,
			};

			var editedHeadphoneId = await this.headphoneService.EditHeadphoneAsync(headphone);

			var editedHeadphoneInDb = this.data.Headphones.First(h => h.Id == editedHeadphoneId);

			Assert.Multiple(() =>
			{
				Assert.That(editedHeadphoneInDb, Is.Not.Null);

				Assert.That(editedHeadphoneInDb.Price, Is.EqualTo(newPrice));

				Assert.That(editedHeadphoneInDb.ImageUrl, Is.EqualTo(headphoneOrigin.ImageUrl));
				Assert.That(editedHeadphoneInDb.Warranty, Is.EqualTo(headphoneOrigin.Warranty));
				Assert.That(editedHeadphoneInDb.Quantity, Is.EqualTo(headphoneOrigin.Quantity));
				Assert.That(editedHeadphoneInDb.IsWireless, Is.EqualTo(headphoneOrigin.IsWireless));
				Assert.That(editedHeadphoneInDb.HasMicrophone, Is.EqualTo(headphoneOrigin.HasMicrophone));
				Assert.That(editedHeadphoneInDb.Brand.Name, Is.EqualTo(headphoneOrigin.Brand.Name));
				Assert.That(editedHeadphoneInDb.Type.Name, Is.EqualTo(headphoneOrigin.Type.Name));
				Assert.That(editedHeadphoneInDb.Color?.Name, Is.EqualTo(headphoneOrigin.Color?.Name));
				Assert.That(editedHeadphoneInDb.Seller, Is.EqualTo(headphoneOrigin.Seller));
			});
		}

		[Test]
		public async Task GetAllHeadphonesAsync_ShouldReturnAllHeadphonesWhenThereAreNoSearchingParameters()
		{
			var result = await this.headphoneService.GetAllHeadphonesAsync();

			var expectedCount = this.data.Headphones.Count();
			Assert.That(result.TotalHeadphonesCount, Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task GetAllHeadphonesAsync_ShouldReturnCorrectHeadphonesAccordingToTheSearchingParameters()
		{
			var typeName = this.data.Types.First().Name;
			var wireless = Wireless.Regardless;
			var keyword = "1";
			var sorting = Sorting.PriceMaxToMin;

			var result = await this.headphoneService.GetAllHeadphonesAsync(
				typeName,
				wireless,
				keyword,
				sorting);

			var expected = this.data.Headphones
				.Where(h => h.Type.Name == typeName)
				.Where(h => h.Brand.Name.Contains(keyword)
							|| h.Type.Name.Contains(keyword))
				.OrderByDescending(h => h.Price)
				.ToList();

			Assert.That(result.TotalHeadphonesCount, Is.EqualTo(expected.Count));
		}

		[Test]
		public async Task GetAllHeadphonesAsync_ShouldReturnEmptyCollectionWhenThereIsNoHeadphoneAccordingToTheSpecifiedCriteria()
		{
			var brandName = "invalid";

			var result = await this.headphoneService.GetAllHeadphonesAsync(brandName);

			Assert.That(result.TotalHeadphonesCount, Is.Zero);
		}

		[Test]
		public async Task GetAllHeadphonesTypesAsync_ShouldReturnCorrectTypesNames()
		{
			var result = await this.headphoneService.GetAllHeadphonesTypesAsync();

			var expectedCount = this.data.Types.Count();
			Assert.That(result.Count(), Is.EqualTo(expectedCount));

			var expected = this.data.Types.Select(x => x.Name).OrderBy(x => x).ToList();
			Assert.That(expected.SequenceEqual<string>(result));
		}

		[Test]
		public async Task GetHeadphonesByIdAsHeadphoneDetailsExportViewModelAsync_ShouldReturnTheCorrectHeadphone()
		{
			var headphoneId = this.data.Headphones.First().Id;

			var result = await this.headphoneService.GetHeadphoneByIdAsHeadphoneDetailsExportViewModelAsync(headphoneId);

			var expected = this.data.Headphones.First();

			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);

				Assert.That(result.Id, Is.EqualTo(expected.Id));
				Assert.That(result.Brand, Is.EqualTo(expected.Brand.Name));
				Assert.That(result.Price, Is.EqualTo(expected.Price));
				Assert.That(result.IsWireless, Is.EqualTo(expected.IsWireless));
				Assert.That(result.HasMicrophone, Is.EqualTo(expected.HasMicrophone));
				Assert.That(result.Type, Is.EqualTo(expected.Type.Name));
				Assert.That(result.ImageUrl, Is.EqualTo(expected.ImageUrl));
				Assert.That(result.Warranty, Is.EqualTo(expected.Warranty));
				Assert.That(result.Quantity, Is.EqualTo(expected.Quantity));
				Assert.That(result.AddedOn, Is.EqualTo(expected.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture)));
				Assert.That(result.Seller, Is.EqualTo(expected.Seller));
			});
		}

		[Test]
		public async Task GetHeadphoneByIdAsHeadphoneEditViewModelAsync_ShouldReturnCorrectHeadphone()
		{
			var headphoneId = this.data.Headphones.First().Id;

			var result = await this.headphoneService.GetHeadphoneByIdAsHeadphoneEditViewModelAsync(headphoneId);

			var expected = this.data.Headphones.First();

			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);

				Assert.That(result.Id, Is.EqualTo(expected.Id));
				Assert.That(result.Brand, Is.EqualTo(expected.Brand.Name));
				Assert.That(result.IsWireless, Is.EqualTo(expected.IsWireless));
				Assert.That(result.HasMicrophone, Is.EqualTo(expected.HasMicrophone));
				Assert.That(result.Type, Is.EqualTo(expected.Type.Name));
				Assert.That(result.Quantity, Is.EqualTo(expected.Quantity));
				Assert.That(result.Price, Is.EqualTo(expected.Price));
				Assert.That(result.Warranty, Is.EqualTo(expected.Warranty));
				Assert.That(result.ImageUrl, Is.EqualTo(expected.ImageUrl));
				Assert.That(result.Seller, Is.EqualTo(expected.Seller));
			});
		}

		[Test]
		public async Task GetUserHeadphonesAsync_ShouldReturnOnlyHeadphonesThatHaveSellerIdEqualToTheClientIdOfTheClientWithTheGivenUserId()
		{
			// Arrange
			var userId = "User1"; // Увери се, че този потребител съществува

			// Act
			var result = await this.headphoneService.GetUserHeadphonesAsync(userId);

			// Assert
			var clientId = this.data.Clients.FirstOrDefault(c => c.UserId == userId)?.Id;
			var expected = this.data.Headphones.Where(h => h.SellerId == clientId).ToList();

			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result.Count(), Is.EqualTo(expected.Count));

				for (int i = 0; i < result.Count(); i++)
				{
					var actual = result.ElementAt(i);
					var exp = expected[i];

					Assert.That(actual.Id, Is.EqualTo(exp.Id));
					Assert.That(actual.Brand, Is.EqualTo(exp.Brand.Name));
					Assert.That(actual.Price, Is.EqualTo(exp.Price));
					Assert.That(actual.IsWireless, Is.EqualTo(exp.IsWireless));
					Assert.That(actual.HasMicrophone, Is.EqualTo(exp.HasMicrophone));
					Assert.That(actual.Type, Is.EqualTo(exp.Type.Name));
					Assert.That(actual.ImageUrl, Is.EqualTo(exp.ImageUrl));
					Assert.That(actual.Warranty, Is.EqualTo(exp.Warranty));
					Assert.That(actual.Quantity, Is.EqualTo(exp.Quantity));
					Assert.That(actual.AddedOn, Is.EqualTo(exp.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture)));
				}
			});
		}


		[Test]
		public async Task MarkHeadphoneAsBoughtAsync_ShouldDecreaseHeadphoneQuantityWhenGivenHeadphoneIdIsValid()
		{
			var headphone = this.data.Headphones.First();

			var headphoneQuantityBeforeBuying = headphone.Quantity;

			await this.headphoneService.MarkHeadphoneAsBoughtAsync(headphone.Id);

			var headphoneQuantityAfterBuying = headphone.Quantity;

			Assert.That(headphoneQuantityAfterBuying, Is.EqualTo(headphoneQuantityBeforeBuying - 1));
		}
	}
}