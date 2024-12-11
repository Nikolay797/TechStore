using System.Globalization;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Exceptions;
using TechStore.Core.Models.Smartwatch;
using TechStore.Core.Services;
using TechStore.Infrastructure.Common;


namespace TechStore.Tests.UnitTests
{
	[TestFixture]
	public class SmartwatchServiceTests : UnitTestsBase
	{
		private IRepository repository;
		private IGuard guard;
		private ISmartwatchService smartwatchService;

		[OneTimeSetUp]
		public void SetUp()
		{
			this.repository = new Repository(this.data);
			this.guard = new Guard();

			this.smartwatchService = new SmartwatchService(this.repository, this.guard);
		}

		[TestCase(null)]
		[TestCase("NewColor")]
		public async Task AddSmartwatchAsync_ShouldAddSmartwatch(string? color)
		{
			// Arrange
			var countOfSmartwatchesInDbBeforeAddition = this.data.SmartWatches.Count();
			var userId = "User1";

			var smartwatch = new SmartwatchImportViewModel()
			{
				ImageUrl = null,
				Warranty = 1,
				Price = 1111.00M,
				Quantity = 1,
				Brand = "NewBrand",
				Color = color,
			};

			// Act
			var smartwatchId = await this.smartwatchService.AddSmartwatchAsync(smartwatch, userId);

			// Assert
			var countOfSmartwatchesInDbAfterAddition = this.data.SmartWatches.Count();
			Assert.That(countOfSmartwatchesInDbAfterAddition, Is.EqualTo(countOfSmartwatchesInDbBeforeAddition + 1));

			var smartwatchInDb = this.data.SmartWatches.First(s => s.Id == smartwatchId);

			Assert.Multiple(() =>
			{
				Assert.That(smartwatchInDb, Is.Not.Null);
				Assert.That(smartwatchInDb.ImageUrl, Is.Null);
				Assert.That(smartwatchInDb.Warranty, Is.EqualTo(smartwatch.Warranty));
				Assert.That(smartwatchInDb.Price, Is.EqualTo(smartwatch.Price));
				Assert.That(smartwatchInDb.Quantity, Is.EqualTo(smartwatch.Quantity));
				Assert.That(smartwatchInDb.Brand.Name, Is.EqualTo(smartwatch.Brand));
				Assert.That(smartwatchInDb.Color?.Name, Is.EqualTo(color));
			});
		}


		[Test]
		public async Task DeleteSmartwatchAsync_ShouldMarkTheSpecifiedSmartwatchAsDeleted()
		{
			// Arrange
			var smartwatch = new SmartwatchImportViewModel()
			{
				ImageUrl = null,
				Warranty = 1,
				Price = 1111.00M,
				Quantity = 1,
				Brand = "NewBrand",
				Color = null,
			};

			var userId = "User1";

			// Act
			var smartwatchId = await this.smartwatchService.AddSmartwatchAsync(smartwatch, userId);

			var addedSmartWatch = this.data.SmartWatches.First(m => m.Id == smartwatchId);
			Assert.That(addedSmartWatch.IsDeleted, Is.False);

			await this.smartwatchService.DeleteSmartwatchAsync(smartwatchId);

			// Assert
			Assert.That(addedSmartWatch.IsDeleted, Is.True);
		}

		[Test]
		public async Task EditSmartwatchAsync_ShouldEditSmartwatchCorrectly()
		{
			var smartwatchOrigin = this.data.SmartWatches.First();

			var newPrice = smartwatchOrigin.Price + 1000;

			var smartwatch = new SmartwatchEditViewModel()
			{
				Id = smartwatchOrigin.Id,
				ImageUrl = smartwatchOrigin.ImageUrl,
				Warranty = smartwatchOrigin.Warranty,
				Price = newPrice,
				Quantity = smartwatchOrigin.Quantity,
				Brand = smartwatchOrigin.Brand.Name,
				Color = smartwatchOrigin.Color?.Name,
				Seller = smartwatchOrigin.Seller,
			};

			var editedSmartwatchId = await this.smartwatchService.EditSmartwatchAsync(smartwatch);

			var editedSmartwatchInDb = this.data.SmartWatches.First(s => s.Id == editedSmartwatchId);

			Assert.Multiple(() =>
			{
				Assert.That(editedSmartwatchInDb, Is.Not.Null);

				Assert.That(editedSmartwatchInDb.Price, Is.EqualTo(newPrice));

				Assert.That(editedSmartwatchInDb.ImageUrl, Is.EqualTo(smartwatchOrigin.ImageUrl));
				Assert.That(editedSmartwatchInDb.Warranty, Is.EqualTo(smartwatchOrigin.Warranty));
				Assert.That(editedSmartwatchInDb.Quantity, Is.EqualTo(smartwatchOrigin.Quantity));
				Assert.That(editedSmartwatchInDb.Brand.Name, Is.EqualTo(smartwatchOrigin.Brand.Name));
				Assert.That(editedSmartwatchInDb.Color?.Name, Is.EqualTo(smartwatchOrigin.Color?.Name));
				Assert.That(editedSmartwatchInDb.Seller, Is.EqualTo(smartwatchOrigin.Seller));
			});
		}

		[Test]
		public async Task GetAllSmartwatchesAsync_ShouldReturnAllSmartwatchesWhenThereAreNoSearchingParameters()
		{
			var result = await this.smartwatchService.GetAllSmartwatchesAsync();

			var expectedCount = this.data.SmartWatches.Count();
			Assert.That(result.TotalSmartwatchesCount, Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task GetAllSmartwatchesAsync_ShouldReturnCorrectSmartwatchesAccordingToTheSearchingParameters()
		{
			var keyword = "1";
			var sorting = Sorting.PriceMaxToMin;

			var result = await this.smartwatchService.GetAllSmartwatchesAsync(keyword, sorting);

			var expected = this.data.SmartWatches
				.Where(m => m.Brand.Name.Contains(keyword))
				.OrderByDescending(m => m.Price)
				.ToList();

			Assert.That(result.TotalSmartwatchesCount, Is.EqualTo(expected.Count));
		}

		[Test]
		public async Task GetAllSmartwatchesAsync_ShouldReturnEmptyCollectionWhenThereIsNoSmartwatchAccordingToTheSpecifiedCriteria()
		{
			var brandName = "invalid";

			var result = await this.smartwatchService.GetAllSmartwatchesAsync(brandName);

			Assert.That(result.TotalSmartwatchesCount, Is.Zero);
		}

		[Test]
		public async Task GetSmartwatchByIdAsSmartwatchDetailsExportViewModelAsync_ShouldReturnTheCorrectSmartwatch()
		{
			var smartwatchId = this.data.SmartWatches.First().Id;

			var result = await this.smartwatchService.GetSmartwatchByIdAsSmartwatchDetailsExportViewModelAsync(smartwatchId);

			var expected = this.data.SmartWatches.First();

			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);

				Assert.That(result.Id, Is.EqualTo(expected.Id));
				Assert.That(result.Brand, Is.EqualTo(expected.Brand.Name));
				Assert.That(result.Price, Is.EqualTo(expected.Price));
				Assert.That(result.ImageUrl, Is.EqualTo(expected.ImageUrl));
				Assert.That(result.Warranty, Is.EqualTo(expected.Warranty));
				Assert.That(result.Quantity, Is.EqualTo(expected.Quantity));
				Assert.That(result.AddedOn, Is.EqualTo(expected.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture)));
				Assert.That(result.Seller, Is.EqualTo(expected.Seller));
			});
		}

		[Test]
		public async Task GetSmartwatchByIdAsSmartwatchEditViewModelAsync_ShouldReturnCorrectSmartwatch()
		{
			var smartwatchId = this.data.SmartWatches.First().Id;

			var result = await this.smartwatchService.GetSmartwatchByIdAsSmartwatchEditViewModelAsync(smartwatchId);

			var expected = this.data.SmartWatches.First();

			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);

				Assert.That(result.Id, Is.EqualTo(expected.Id));
				Assert.That(result.Brand, Is.EqualTo(expected.Brand.Name));
				Assert.That(result.Quantity, Is.EqualTo(expected.Quantity));
				Assert.That(result.Price, Is.EqualTo(expected.Price));
				Assert.That(result.Warranty, Is.EqualTo(expected.Warranty));
				Assert.That(result.ImageUrl, Is.EqualTo(expected.ImageUrl));
				Assert.That(result.Seller, Is.EqualTo(expected.Seller));
			});
		}
		[Test]
		public async Task GetUserSmartwatchesAsync_ShouldReturnOnlySmartwatchesThatHaveSellerIdEqualToTheClientIdOfTheClientWithTheGivenUserId()
		{
			// Arrange
			var userId = "User1"; 
			var clientId = this.data.Clients.FirstOrDefault(c => c.UserId == userId)?.Id;

			// Act
			var result = await this.smartwatchService.GetUserSmartwatchesAsync(userId);

			// Assert
			var resultFirst = result.First();
			var expected = this.data.SmartWatches.Where(s => s.SellerId == clientId);
			var expectedFirst = expected.First();

			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(resultFirst, Is.Not.Null);
				Assert.That(resultFirst.Id, Is.EqualTo(expectedFirst.Id));
				Assert.That(resultFirst.Brand, Is.EqualTo(expectedFirst.Brand.Name));
				Assert.That(resultFirst.Price, Is.EqualTo(expectedFirst.Price));
				Assert.That(resultFirst.Warranty, Is.EqualTo(expectedFirst.Warranty));
			});
		}

		[Test]
		public async Task MarkSmartwatchAsBoughtAsync_ShouldDecreaseSmartwatchQuantityWhenGivenSmartwatchIdIsValid()
		{
			var smartwatch = this.data.SmartWatches.First();

			var smartwatchQuantityBeforeBuying = smartwatch.Quantity;

			await this.smartwatchService.MarkSmartwatchAsBought(smartwatch.Id);

			var smartwatchQuantityAfterBuying = smartwatch.Quantity;

			Assert.That(smartwatchQuantityAfterBuying, Is.EqualTo(smartwatchQuantityBeforeBuying - 1));
		}
	}
}