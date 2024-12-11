using System.Globalization;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Exceptions;
using TechStore.Core.Models.Mice;
using TechStore.Core.Services;
using TechStore.Infrastructure.Common;

namespace TechStore.Tests.UnitTests
{
	[TestFixture]
	public class MouseServiceTests : UnitTestsBase
	{
		private IRepository repository;
		private IGuard guard;
		private IMouseService mouseService;

		[OneTimeSetUp]
		public void SetUp()
		{
			this.repository = new Repository(this.data);
			this.guard = new Guard();

			this.mouseService = new MouseService(this.repository, this.guard);
		}

		[TestCase(null)]
		[TestCase("NewColor")]
		public async Task AddMouseAsync_ShouldAddMouse(string? color)
		{
			// Arrange
			var userId = "User1";

			var countOfMiceInDbBeforeAddition = this.data.Mice.Count();

			var mouse = new MouseImportViewModel
			{
				ImageUrl = null,
				Warranty = 1,
				Price = 1111.00M,
				Quantity = 1,
				IsWireless = true,
				Brand = "NewBrand",
				Type = "NewType",
				Sensitivity = "0 - 100 DPI",
				Color = color
			};

			// Act
			var mouseId = await this.mouseService.AddMouseAsync(mouse, userId);

			var countOfMiceInDbAfterAddition = this.data.Mice.Count();

			// Assert
			Assert.That(countOfMiceInDbAfterAddition, Is.EqualTo(countOfMiceInDbBeforeAddition + 1));

			var mouseInDb = this.data.Mice.First(m => m.Id == mouseId);

			Assert.Multiple(() =>
			{
				Assert.That(mouseInDb, Is.Not.Null);
				Assert.That(mouseInDb.ImageUrl, Is.Null);
				Assert.That(mouseInDb.Warranty, Is.EqualTo(mouse.Warranty));
				Assert.That(mouseInDb.Price, Is.EqualTo(mouse.Price));
				Assert.That(mouseInDb.Quantity, Is.EqualTo(mouse.Quantity));
				Assert.That(mouseInDb.IsWireless, Is.EqualTo(mouse.IsWireless));
				Assert.That(mouseInDb.Brand.Name, Is.EqualTo(mouse.Brand));
				Assert.That(mouseInDb.Type.Name, Is.EqualTo(mouse.Type));
				Assert.That(mouseInDb.Sensitivity.Range, Is.EqualTo(mouse.Sensitivity));
				Assert.That(mouseInDb.Color?.Name, Is.EqualTo(color));
			});
		}


		[Test]
		public async Task DeleteMouseAsync_ShouldMarkTheSpecifiedMouseAsDeleted()
		{
			// Arrange
			var userId = "User1"; // Уверяваме се, че това userId съществува и има свързан клиент
			var mouse = new MouseImportViewModel()
			{
				ImageUrl = null,
				Warranty = 1,
				Price = 1111.00M,
				Quantity = 1,
				IsWireless = true,
				Brand = "NewBrand",
				Type = "NewType",
				Sensitivity = "0 - 100 DPI",
				Color = null,
			};

			// Добавяме мишката с валиден userId
			var mouseId = await this.mouseService.AddMouseAsync(mouse, userId);

			// Проверяваме дали мишката е добавена успешно
			var addedMouse = this.data.Mice.First(m => m.Id == mouseId);
			Assert.That(addedMouse.IsDeleted, Is.False, "The mouse should not be marked as deleted initially.");

			// Act
			await this.mouseService.DeleteMouseAsync(mouseId);

			// Assert
			Assert.That(addedMouse.IsDeleted, Is.True, "The mouse should be marked as deleted after calling DeleteMouseAsync.");

			// Clean up: Възстановяваме състоянието
			addedMouse.IsDeleted = false;
		}


		[Test]
		public async Task EditMouseAsync_ShouldEditMouseCorrectly()
		{
			var mouseOrigin = this.data.Mice.First();

			var newPrice = mouseOrigin.Price + 1000;

			var mouse = new MouseEditViewModel()
			{
				Id = mouseOrigin.Id,
				ImageUrl = mouseOrigin.ImageUrl,
				Warranty = mouseOrigin.Warranty,
				Price = newPrice,
				Quantity = mouseOrigin.Quantity,
				IsWireless = mouseOrigin.IsWireless,
				Brand = mouseOrigin.Brand.Name,
				Type = mouseOrigin.Type.Name,
				Sensitivity = mouseOrigin.Sensitivity.Range,
				Color = mouseOrigin.Color?.Name,
				Seller = mouseOrigin.Seller,
			};

			var editedMouseId = await this.mouseService.EditMouseAsync(mouse);

			var editedMouseInDb = this.data.Mice.First(m => m.Id == editedMouseId);

			Assert.Multiple(() =>
			{
				Assert.That(editedMouseInDb, Is.Not.Null);

				Assert.That(editedMouseInDb.Price, Is.EqualTo(newPrice));

				Assert.That(editedMouseInDb.ImageUrl, Is.EqualTo(mouseOrigin.ImageUrl));
				Assert.That(editedMouseInDb.Warranty, Is.EqualTo(mouseOrigin.Warranty));
				Assert.That(editedMouseInDb.Quantity, Is.EqualTo(mouseOrigin.Quantity));
				Assert.That(editedMouseInDb.IsWireless, Is.EqualTo(mouseOrigin.IsWireless));
				Assert.That(editedMouseInDb.Brand.Name, Is.EqualTo(mouseOrigin.Brand.Name));
				Assert.That(editedMouseInDb.Type.Name, Is.EqualTo(mouseOrigin.Type.Name));
				Assert.That(editedMouseInDb.Sensitivity.Range, Is.EqualTo(mouseOrigin.Sensitivity.Range));
				Assert.That(editedMouseInDb.Color?.Name, Is.EqualTo(mouseOrigin.Color?.Name));
				Assert.That(editedMouseInDb.Seller, Is.EqualTo(mouseOrigin.Seller));
			});
		}

		[Test]
		public async Task GetAllMiceAsync_ShouldReturnAllMiceWhenThereAreNoSearchingParameters()
		{
			var result = await this.mouseService.GetAllMiceAsync();

			var expectedCount = this.data.Mice.Count();
			Assert.That(result.TotalMiceCount, Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task GetAllMiceAsync_ShouldReturnCorrectMiceAccordingToTheSearchingParameters()
		{
			var typeName = this.data.Types.First().Name;
			var sensitivityRange = this.data.Sensitivities.First().Range;
			var wireless = Wireless.Regardless;
			var keyword = "1";
			var sorting = Sorting.PriceMaxToMin;

			var result = await this.mouseService.GetAllMiceAsync(
				typeName,
				sensitivityRange,
				wireless,
				keyword,
				sorting);

			var expected = this.data.Mice
				.Where(m => m.Type.Name == typeName
							&& m.Sensitivity.Range == sensitivityRange)
				.Where(m => m.Brand.Name.Contains(keyword)
							|| m.Type.Name.Contains(keyword)
							|| m.Sensitivity.Range.Contains(keyword))
				.OrderByDescending(m => m.Price)
				.ToList();

			Assert.That(result.TotalMiceCount, Is.EqualTo(expected.Count));
		}

		[Test]
		public async Task GetAllMiceAsync_ShouldReturnEmptyCollectionWhenThereIsNoMouseAccordingToTheSpecifiedCriteria()
		{
			var typeName = "invalid";

			var result = await this.mouseService.GetAllMiceAsync(typeName);

			Assert.That(result.TotalMiceCount, Is.Zero);
		}

		[Test]
		public async Task GetAllMiceSensitivitiesAsync_ShouldReturnCorrectSensitivitiesRanges()
		{
			var result = await this.mouseService.GetAllMiceSensitivitiesAsync();

			var expectedCount = this.data.Sensitivities.Count();
			Assert.That(result.Count(), Is.EqualTo(expectedCount));

			var expected = this.data.Sensitivities.Select(x => x.Range).OrderBy(x => x).ToList();
			Assert.That(expected.SequenceEqual<string>(result));
		}

		[Test]
		public async Task GetAllMiceTypesAsync_ShouldReturnCorrectTypesNames()
		{
			var result = await this.mouseService.GetAllMiceTypesAsync();

			var expectedCount = this.data.Types.Count();
			Assert.That(result.Count(), Is.EqualTo(expectedCount));

			var expected = this.data.Types.Select(x => x.Name).OrderBy(x => x).ToList();
			Assert.That(expected.SequenceEqual<string>(result));
		}

		[Test]
		public async Task GetMouseByIdAsMouseDetailsExportViewModelAsync_ShouldReturnTheCorrectMouse()
		{
			// Arrange
			var mouseId = this.data.Mice.First().Id;

			// Act
			var result = await this.mouseService.GetMouseByIdAsMouseDetailsExportViewModelAsync(mouseId);

			var expected = this.data.Mice.First();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result.Id, Is.EqualTo(expected.Id));
				Assert.That(result.Brand, Is.EqualTo(expected.Brand.Name));
				Assert.That(result.Price, Is.EqualTo(expected.Price));
				Assert.That(result.IsWireless, Is.EqualTo(expected.IsWireless));
				Assert.That(result.Type, Is.EqualTo(expected.Type.Name));
				Assert.That(result.Sensitivity, Is.EqualTo(expected.Sensitivity.Range));
				Assert.That(result.ImageUrl, Is.EqualTo(expected.ImageUrl));
				Assert.That(result.Warranty, Is.EqualTo(expected.Warranty));
				Assert.That(result.Quantity, Is.EqualTo(expected.Quantity));
				Assert.That(result.AddedOn, Is.EqualTo(expected.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture)));

				// Compare Seller by unique property (e.g., ID)
				Assert.That(result.Seller?.Id, Is.EqualTo(expected.Seller?.Id), "Seller IDs do not match.");
			});
		}


		[Test]
		public async Task GetMouseByIdAsMouseEditViewModelAsync_ShouldReturnCorrectMouse()
		{
			// Arrange
			var mouseId = this.data.Mice.First().Id;

			// Act
			var result = await this.mouseService.GetMouseByIdAsMouseEditViewModelAsync(mouseId);

			var expected = this.data.Mice.First();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);
				Assert.That(result.Id, Is.EqualTo(expected.Id));
				Assert.That(result.Brand, Is.EqualTo(expected.Brand.Name));
				Assert.That(result.IsWireless, Is.EqualTo(expected.IsWireless));
				Assert.That(result.Type, Is.EqualTo(expected.Type.Name));
				Assert.That(result.Sensitivity, Is.EqualTo(expected.Sensitivity.Range));
				Assert.That(result.Quantity, Is.EqualTo(expected.Quantity));
				Assert.That(result.Price, Is.EqualTo(expected.Price));
				Assert.That(result.Warranty, Is.EqualTo(expected.Warranty));
				Assert.That(result.ImageUrl, Is.EqualTo(expected.ImageUrl));

				// Compare Seller by unique property (e.g., ID)
				Assert.That(result.Seller?.Id, Is.EqualTo(expected.Seller?.Id), "Seller IDs do not match.");
			});
		}


		[Test]
		public async Task GetUserMiceAsync_ShouldReturnOnlyMiceThatHaveSellerIdEqualToTheClientIdOfTheClientWithTheGivenUserId()
		{
			// Arrange
			var userId = "User1";

			// Act
			var result = await this.mouseService.GetUserMiceAsync(userId);

			// Assert
			Assert.That(result, Is.Not.Null);
			Assert.That(result.Count(), Is.EqualTo(2)); // Две мишки за User1

			var firstMouse = result.First();
			Assert.Multiple(() =>
			{
				Assert.That(firstMouse.Brand, Is.EqualTo("Brand1"));
				Assert.That(firstMouse.Price, Is.EqualTo(1000.00M));
				Assert.That(firstMouse.IsWireless, Is.True);
				Assert.That(firstMouse.Warranty, Is.EqualTo(1));
			});
		}


		[Test]
		public async Task MarkMouseAsBoughtAsync_ShouldDecreaseMouseQuantityWhenGivenMouseIdIsValid()
		{
			var mouse = this.data.Mice.First();

			var mouseQuantityBeforeBuying = mouse.Quantity;

			await this.mouseService.MarkMouseAsBoughtAsync(mouse.Id);

			var mouseQuantityAfterBuying = mouse.Quantity;

			Assert.That(mouseQuantityAfterBuying, Is.EqualTo(mouseQuantityBeforeBuying - 1));
		}
	}
}