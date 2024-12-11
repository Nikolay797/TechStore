using System.Globalization;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Exceptions;
using TechStore.Core.Models.Laptop;
using TechStore.Core.Services;
using TechStore.Infrastructure.Common;
using TechStore.Infrastructure.Data.Models;
using TechStore.Tests.Mocks;
using static TechStore.Infrastructure.Constants.DataConstant.ProductConstants;


namespace TechStore.Tests.UnitTests
{
	[TestFixture]
	public class LaptopServiceTests : UnitTestsBase
	{
		private IRepository repository;
		private IGuard guard;
		private ILaptopService laptopService;

		[OneTimeSetUp]
		public void SetUp()
		{
			this.repository = new Repository(this.data);
			this.guard = new Guard();

			this.laptopService = new LaptopService(this.repository, this.guard);
		}

		[Test]
		public async Task AddLaptopAsync_ShouldAddLaptop_WithValidUserId()
		{
			// Arrange
			var userId = "User1"; // Assume User1 exists in the database
			var model = new LaptopImportViewModel
			{
				ImageUrl = "http://example.com/laptop.jpg",
				Warranty = 12,
				Price = 1500.99m,
				Quantity = 10,
				Brand = "Brand1",
				CPU = "CPU1",
				RAM = 16,
				SSDCapacity = 512,
				VideoCard = "VideoCard1",
				Type = "Gaming",
				DisplaySize = 15.6,
				DisplayCoverage = "Matte",
				DisplayTechnology = "IPS",
				Resolution = "1920x1080",
				Color = "Black"
			};

			// Act
			var laptopId = await this.laptopService.AddLaptopAsync(model, userId);

			// Assert
			var addedLaptop = await this.repository.GetByIdAsync<Laptop>(laptopId);

			Assert.Multiple(() =>
			{
				Assert.That(addedLaptop, Is.Not.Null);
				Assert.That(addedLaptop.ImageUrl, Is.EqualTo(model.ImageUrl));
				Assert.That(addedLaptop.Warranty, Is.EqualTo(model.Warranty));
				Assert.That(addedLaptop.Price, Is.EqualTo(model.Price));
				Assert.That(addedLaptop.Quantity, Is.EqualTo(model.Quantity));
				Assert.That(addedLaptop.IsDeleted, Is.False);
				Assert.That(addedLaptop.Seller.UserId, Is.EqualTo(userId));
			});
		}

		[Test]
		public async Task AddLaptopAsync_ShouldAddLaptop_WithoutUserId()
		{
			// Arrange
			var model = new LaptopImportViewModel
			{
				ImageUrl = "http://example.com/laptop.jpg",
				Warranty = 24,
				Price = 2000.50m,
				Quantity = 5,
				Brand = "Brand2",
				CPU = "CPU2",
				RAM = 8,
				SSDCapacity = 256,
				VideoCard = "VideoCard2",
				Type = "Ultrabook",
				DisplaySize = 14,
				DisplayCoverage = "Glossy",
				DisplayTechnology = "OLED",
				Resolution = "2560x1440",
				Color = "Silver"
			};

			// Act
			var laptopId = await this.laptopService.AddLaptopAsync(model, null);

			// Assert
			var addedLaptop = await this.repository.GetByIdAsync<Laptop>(laptopId);

			Assert.Multiple(() =>
			{
				Assert.That(addedLaptop, Is.Not.Null);
				Assert.That(addedLaptop.ImageUrl, Is.EqualTo(model.ImageUrl));
				Assert.That(addedLaptop.Warranty, Is.EqualTo(model.Warranty));
				Assert.That(addedLaptop.Price, Is.EqualTo(model.Price));
				Assert.That(addedLaptop.Quantity, Is.EqualTo(model.Quantity));
				Assert.That(addedLaptop.IsDeleted, Is.False);
				Assert.That(addedLaptop.Seller, Is.Null);
			});
		}

		[Test]
		public void AddLaptopAsync_ShouldThrowException_WithInvalidUserId()
		{
			// Arrange
			var invalidUserId = "InvalidUserId"; // Assume this user does not exist
			var model = new LaptopImportViewModel
			{
				ImageUrl = "http://example.com/laptop.jpg",
				Warranty = 12,
				Price = 1500.99m,
				Quantity = 10,
				Brand = "Brand1",
				CPU = "CPU1",
				RAM = 16,
				SSDCapacity = 512,
				VideoCard = "VideoCard1",
				Type = "Gaming",
				DisplaySize = 15.6,
				DisplayCoverage = "Matte",
				DisplayTechnology = "IPS",
				Resolution = "1920x1080",
				Color = "Black"
			};

			// Act & Assert
			var exception = Assert.ThrowsAsync<TechStoreException>(async () =>
			{
				await this.laptopService.AddLaptopAsync(model, invalidUserId);
			});

			Assert.That(exception.Message, Is.EqualTo("Invalid User Id!"));
		}

		[Test]
		public async Task DeleteLaptopAsync_ShouldMarkLaptopAsDeleted_WhenLaptopExistsAndIsNotDeleted()
		{
			// Arrange
			var laptop = new Laptop
			{
				Id = 10,
				BrandId = 1,
				CPUId = 1,
				RAMId = 1,
				SSDCapacityId = 1,
				VideoCardId = 1,
				Price = 1000.00M,
				TypeId = 1,
				DisplaySizeId = 1,
				Warranty = 1,
				Quantity = 1,
				AddedOn = DateTime.UtcNow.Date,
				IsDeleted = false
			};
			this.data.Add(laptop);
			this.data.SaveChanges();

			// Act
			await this.laptopService.DeleteLaptopAsync(laptop.Id);

			// Assert
			var deletedLaptop = this.data.Laptops.FirstOrDefault(l => l.Id == laptop.Id);
			Assert.Multiple(() =>
			{
				Assert.That(deletedLaptop, Is.Not.Null, "Laptop was not found in the database.");
				Assert.That(deletedLaptop.IsDeleted, Is.True, "Laptop was not marked as deleted.");
			});
		}

		[Test]
		public async Task EditLaptopAsync_ShouldEditLaptopCorrectly()
		{
			var laptopOrigin = this.data.Laptops.First();

			var newPrice = laptopOrigin.Price + 1000;

			var laptop = new LaptopEditViewModel()
			{
				Id = laptopOrigin.Id,
				ImageUrl = laptopOrigin.ImageUrl,
				Warranty = laptopOrigin.Warranty,
				Price = newPrice,
				Quantity = laptopOrigin.Quantity,
				Brand = laptopOrigin.Brand.Name,
				CPU = laptopOrigin.CPU.Name,
				RAM = laptopOrigin.RAM.Value,
				SSDCapacity = laptopOrigin.SSDCapacity.Value,
				VideoCard = laptopOrigin.VideoCard.Name,
				Type = laptopOrigin.Type.Name,
				DisplaySize = laptopOrigin.DisplaySize.Value,
				DisplayCoverage = laptopOrigin.DisplayCoverage?.Name,
				DisplayTechnology = laptopOrigin.DisplayTechnology?.Name,
				Resolution = laptopOrigin.Resolution?.Value,
				Color = laptopOrigin.Color?.Name,
				Seller = laptopOrigin.Seller,
			};

			var editedLaptopId = await this.laptopService.EditLaptopAsync(laptop);

			var editedLaptopInDb = this.data.Laptops.First(l => l.Id == editedLaptopId);

			Assert.Multiple(() =>
			{
				Assert.That(editedLaptopInDb, Is.Not.Null);

				Assert.That(editedLaptopInDb.Price, Is.EqualTo(newPrice));

				Assert.That(editedLaptopInDb.ImageUrl, Is.EqualTo(laptopOrigin.ImageUrl));
				Assert.That(editedLaptopInDb.Warranty, Is.EqualTo(laptopOrigin.Warranty));
				Assert.That(editedLaptopInDb.Quantity, Is.EqualTo(laptopOrigin.Quantity));
				Assert.That(editedLaptopInDb.Brand.Name, Is.EqualTo(laptopOrigin.Brand.Name));
				Assert.That(editedLaptopInDb.CPU.Name, Is.EqualTo(laptopOrigin.CPU.Name));
				Assert.That(editedLaptopInDb.RAM.Value, Is.EqualTo(laptopOrigin.RAM.Value));
				Assert.That(editedLaptopInDb.SSDCapacity.Value, Is.EqualTo(laptopOrigin.SSDCapacity.Value));
				Assert.That(editedLaptopInDb.VideoCard.Name, Is.EqualTo(laptopOrigin.VideoCard.Name));
				Assert.That(editedLaptopInDb.Type.Name, Is.EqualTo(laptopOrigin.Type.Name));
				Assert.That(editedLaptopInDb.DisplaySize.Value, Is.EqualTo(laptopOrigin.DisplaySize.Value));
				Assert.That(editedLaptopInDb.DisplayCoverage?.Name, Is.EqualTo(laptopOrigin.DisplayCoverage?.Name));
				Assert.That(editedLaptopInDb.DisplayTechnology?.Name, Is.EqualTo(laptopOrigin.DisplayTechnology?.Name));
				Assert.That(editedLaptopInDb.Resolution?.Value, Is.EqualTo(laptopOrigin.Resolution?.Value));
				Assert.That(editedLaptopInDb.Color?.Name, Is.EqualTo(laptopOrigin.Color?.Name));
				Assert.That(editedLaptopInDb.Seller, Is.EqualTo(laptopOrigin.Seller));
			});
		}

		[Test]
		public async Task GetAllLaptopsAsync_ShouldReturnAllLaptopsWhenThereAreNoSearchingParameters()
		{
			// Act
			var result = await this.laptopService.GetAllLaptopsAsync();

			// Assert
			var expectedCount = this.data.Laptops.Count(l => !l.IsDeleted);
			Assert.That(result.TotalLaptopsCount, Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task GetAllLaptopsAsync_ShouldReturnCorrectLaptopsAccordingToTheSearchingParameters()
		{
			// Arrange
			var cpuName = "CPU1";
			var ramValue = 1;
			var ssdCapacityValue = 1;
			var videoCardName = "VideoCard1";
			var keyword = "1";
			var sorting = Sorting.PriceMaxToMin;

			// Act
			var result = await this.laptopService.GetAllLaptopsAsync(cpuName, ramValue, ssdCapacityValue, videoCardName, keyword, sorting);

			// Assert
			var expected = this.data.Laptops
				.Where(l => l.CPU.Name == cpuName
				            && l.RAM.Value == ramValue
				            && l.SSDCapacity.Value == ssdCapacityValue
				            && l.VideoCard.Name == videoCardName)
				.Where(l => l.Brand.Name.Contains(keyword)
				            || l.CPU.Name.Contains(keyword)
				            || l.VideoCard.Name.Contains(keyword)
				            || l.Type.Name.Contains(keyword))
				.ToList();

			Assert.That(result.TotalLaptopsCount, Is.EqualTo(expected.Count));
		}

		[Test]
		public async Task GetAllLaptopsAsync_ShouldReturnEmptyCollectionWhenThereIsNoLaptopAccordingToTheSpecifiedCriteria()
		{
			var cpuName = "InvalidCPU";

			var result = await this.laptopService.GetAllLaptopsAsync(cpuName);

			Assert.That(result.TotalLaptopsCount, Is.Zero);
		}

		[Test]
		public async Task GetLaptopByIdAsLaptopDetailsExportViewModelAsync_ShouldReturnTheCorrectLaptop()
		{
			var laptopId = this.data.Laptops.First().Id;

			var result = await this.laptopService.GetLaptopByIdAsLaptopDetailsExportViewModelAsync(laptopId);

			var expected = this.data.Laptops.First();

			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);

				Assert.That(result.Id, Is.EqualTo(expected.Id));
				Assert.That(result.Brand, Is.EqualTo(expected.Brand.Name));
				Assert.That(result.CPU, Is.EqualTo(expected.CPU.Name));
				Assert.That(result.RAM, Is.EqualTo(expected.RAM.Value));
				Assert.That(result.SSDCapacity, Is.EqualTo(expected.SSDCapacity.Value));
				Assert.That(result.VideoCard, Is.EqualTo(expected.VideoCard.Name));
				Assert.That(result.Price, Is.EqualTo(expected.Price));
				Assert.That(result.DisplaySize, Is.EqualTo(expected.DisplaySize.Value));
				Assert.That(result.Warranty, Is.EqualTo(expected.Warranty));
				Assert.That(result.Type, Is.EqualTo(expected.Type.Name));
				Assert.That(result.ImageUrl, Is.EqualTo(expected.ImageUrl));
				Assert.That(result.AddedOn, Is.EqualTo(expected.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture)));
				Assert.That(result.Quantity, Is.EqualTo(expected.Quantity));
				Assert.That(result.Seller, Is.EqualTo(expected.Seller));
			});
		}

		[Test]
		public async Task GetLaptopByIdAsLaptopEditViewModelAsync_ShouldReturnCorrectLaptop()
		{
			var laptopId = this.data.Laptops.First().Id;

			var result = await this.laptopService.GetLaptopByIdAsLaptopEditViewModelAsync(laptopId);

			var expected = this.data.Laptops.First();

			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);

				Assert.That(result.Id, Is.EqualTo(expected.Id));
				Assert.That(result.Brand, Is.EqualTo(expected.Brand.Name));
				Assert.That(result.CPU, Is.EqualTo(expected.CPU.Name));
				Assert.That(result.RAM, Is.EqualTo(expected.RAM.Value));
				Assert.That(result.SSDCapacity, Is.EqualTo(expected.SSDCapacity.Value));
				Assert.That(result.VideoCard, Is.EqualTo(expected.VideoCard.Name));
				Assert.That(result.Price, Is.EqualTo(expected.Price));
				Assert.That(result.Type, Is.EqualTo(expected.Type.Name));
				Assert.That(result.DisplaySize, Is.EqualTo(expected.DisplaySize.Value));
				Assert.That(result.Warranty, Is.EqualTo(expected.Warranty));
				Assert.That(result.Quantity, Is.EqualTo(expected.Quantity));
			});
		}

		[Test]
		public void GetUserLaptopsAsync_ShouldThrowException_WhenUserIdIsInvalid()
		{
			// Arrange
			var invalidUserId = "InvalidUser";

			// Act & Assert
			var exception = Assert.ThrowsAsync<TechStoreException>(async () =>
				await this.laptopService.GetUserLaptopsAsync(invalidUserId));

			Assert.That(exception.Message, Is.EqualTo("Invalid User Id!"));
		}

		[Test]
		public async Task MarkLaptopAsBoughtAsync_ShouldDecreaseLaptopQuantityWhenGivenLaptopIdIsValid()
		{
			var laptop = this.data.Laptops.First();

			var laptopQuantityBeforeBuying = laptop.Quantity;

			await this.laptopService.MarkLaptopAsBoughtAsync(laptop.Id);

			var laptopQuantityAfterBuying = laptop.Quantity;

			Assert.That(laptopQuantityAfterBuying, Is.EqualTo(laptopQuantityBeforeBuying - 1));
		}

		[Test]
		public async Task GetAllCpusNamesAsync_ShouldReturnCorrectCpuNames()
		{
			var result = await this.laptopService.GetAllCpusNamesAsync();

			var expectedCount = this.data.CPUs.Count();
			Assert.That(result.Count(), Is.EqualTo(expectedCount));

			var expected = this.data.CPUs.Select(x => x.Name).OrderBy(x => x).ToList();
			Assert.That(expected.SequenceEqual<string>(result));
		}

		[Test]
		public async Task GetAllRamsValuesAsync_ShouldReturnCorrectRamValues()
		{
			var result = await this.laptopService.GetAllRamsValuesAsync();

			var expectedCount = this.data.RAMs.Count();
			Assert.That(result.Count(), Is.EqualTo(expectedCount));

			var expected = this.data.RAMs.Select(x => x.Value).OrderBy(x => x).ToList();
			Assert.That(expected.SequenceEqual<int>(result));
		}

		[Test]
		public async Task GetAllSsdCapacitiesValuesAsync_ShouldReturnCorrectSsdCapacityValues()
		{
			var result = await this.laptopService.GetAllSsdCapacitiesValuesAsync();

			var expectedCount = this.data.SSDCapacities.Count();
			Assert.That(result.Count(), Is.EqualTo(expectedCount));

			var expected = this.data.SSDCapacities.Select(x => x.Value).OrderBy(x => x).ToList();
			Assert.That(expected.SequenceEqual<int>(result));
		}

		[Test]
		public async Task GetAllVideoCardsNamesAsync_ShouldReturnCorrectVideoCardNames()
		{
			var result = await this.laptopService.GetAllVideoCardsNamesAsync();

			var expectedCount = this.data.VideoCards.Count();
			Assert.That(result.Count(), Is.EqualTo(expectedCount));

			var expected = this.data.VideoCards.Select(x => x.Name).OrderBy(x => x).ToList();
			Assert.That(expected.SequenceEqual<string>(result));
		}
	}
}