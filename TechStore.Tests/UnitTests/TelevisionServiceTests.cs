using System.Globalization;
using TechStore.Core.Contracts;
using TechStore.Core.Enums;
using TechStore.Core.Exceptions;
using TechStore.Core.Models.Television;
using TechStore.Core.Services;
using TechStore.Infrastructure.Common;

namespace TechStore.Tests.UnitTests
{
	[TestFixture]
	public class TelevisionServiceTests : UnitTestsBase
	{
		private IRepository repository;
		private IGuard guard;
		private ITelevisionService televisionService;

		[OneTimeSetUp]
		public void SetUp()
		{
			this.repository = new Repository(this.data);
			this.guard = new Guard();

			this.televisionService = new TelevisionService(this.repository, this.guard);
		}

		[TestCase(null, null)]
		[TestCase("NewDT", "NewColor")]
		public async Task AddTelevisionAsync_ShouldAddTelevision(
				string? displayTechnology,
				string? color)
		{
			var countOfTelevisionsInDbBeforeAddition = this.data.Televisions.Count();

			var television = new TelevisionImportViewModel()
			{
				ImageUrl = null,
				Warranty = 1,
				Price = 1111.00M,
				Quantity = 1,
				Brand = "NewBrand",
				DisplaySize = 4,
				Resolution = "1111x1111",
				Type = "NewType",
				DisplayTechnology = displayTechnology,
				Color = color,
			};

			// Вземете съществуващ userId
			var userId = "User1";

			// Изпълнете метода
			var televisionId = await this.televisionService.AddTelevisionAsync(television, userId);

			var countOfTelevisionsInDbAfterAddition = this.data.Televisions.Count();

			// Утвърждаване
			Assert.That(countOfTelevisionsInDbAfterAddition, Is.EqualTo(countOfTelevisionsInDbBeforeAddition + 1));

			var televisionInDb = this.data.Televisions.First(m => m.Id == televisionId);

			Assert.Multiple(() =>
			{
				Assert.That(televisionInDb, Is.Not.Null);

				Assert.That(televisionInDb.ImageUrl, Is.Null);
				Assert.That(televisionInDb.Warranty, Is.EqualTo(television.Warranty));
				Assert.That(televisionInDb.Price, Is.EqualTo(television.Price));
				Assert.That(televisionInDb.Quantity, Is.EqualTo(television.Quantity));
				Assert.That(televisionInDb.Brand.Name, Is.EqualTo(television.Brand));
				Assert.That(televisionInDb.DisplaySize.Value, Is.EqualTo(television.DisplaySize));
				Assert.That(televisionInDb.Resolution.Value, Is.EqualTo(television.Resolution));
				Assert.That(televisionInDb.Type.Name, Is.EqualTo(television.Type));
				Assert.That(televisionInDb.DisplayTechnology?.Name, Is.EqualTo(displayTechnology));
				Assert.That(televisionInDb.Color?.Name, Is.EqualTo(color));
			});
		}


		[Test]
		public async Task DeleteTelevisionAsync_ShouldMarkTheSpecifiedTelevisionAsDeleted()
		{
			var television = new TelevisionImportViewModel()
			{
				ImageUrl = null,
				Warranty = 1,
				Price = 1111.00M,
				Quantity = 1,
				Brand = "NewBrand",
				DisplaySize = 4,
				Resolution = "1111x1111",
				Type = "NewType",
				DisplayTechnology = null,
				Color = null,
			};

			// Вземете валиден userId
			var userId = "User1";

			var televisionId = await this.televisionService.AddTelevisionAsync(television, userId);

			var addedTelevision = this.data.Televisions.First(t => t.Id == televisionId);

			Assert.That(addedTelevision.IsDeleted, Is.False);

			await this.televisionService.DeleteTelevisionAsync(televisionId);

			Assert.That(addedTelevision.IsDeleted, Is.True);

			addedTelevision.IsDeleted = false;
		}


		[Test]
		public async Task EditTelevisionAsync_ShouldEditTelevisionCorrectly()
		{
			var televisionOrigin = this.data.Televisions.First();

			var newPrice = televisionOrigin.Price + 1000;

			var television = new TelevisionEditViewModel()
			{
				Id = televisionOrigin.Id,
				ImageUrl = televisionOrigin.ImageUrl,
				Warranty = televisionOrigin.Warranty,
				Price = newPrice,
				Quantity = televisionOrigin.Quantity,
				Brand = televisionOrigin.Brand.Name,
				DisplaySize = televisionOrigin.DisplaySize.Value,
				Resolution = televisionOrigin.Resolution.Value,
				Type = televisionOrigin.Type.Name,
				DisplayTechnology = televisionOrigin.DisplayTechnology?.Name,
				Color = televisionOrigin.Color?.Name,
				Seller = televisionOrigin.Seller,
			};

			var editedTelevisionId = await this.televisionService.EditTelevisionAsync(television);

			var editedTelevisionInDb = this.data.Televisions.First(m => m.Id == editedTelevisionId);

			Assert.Multiple(() =>
			{
				Assert.That(editedTelevisionInDb, Is.Not.Null);

				Assert.That(editedTelevisionInDb.Price, Is.EqualTo(newPrice));

				Assert.That(editedTelevisionInDb.ImageUrl, Is.EqualTo(televisionOrigin.ImageUrl));
				Assert.That(editedTelevisionInDb.Warranty, Is.EqualTo(televisionOrigin.Warranty));
				Assert.That(editedTelevisionInDb.Quantity, Is.EqualTo(televisionOrigin.Quantity));
				Assert.That(editedTelevisionInDb.Brand.Name, Is.EqualTo(televisionOrigin.Brand.Name));
				Assert.That(editedTelevisionInDb.DisplaySize.Value, Is.EqualTo(televisionOrigin.DisplaySize.Value));
				Assert.That(editedTelevisionInDb.Resolution.Value, Is.EqualTo(televisionOrigin.Resolution.Value));
				Assert.That(editedTelevisionInDb.Type.Name, Is.EqualTo(televisionOrigin.Type.Name));
				Assert.That(editedTelevisionInDb.DisplayTechnology?.Name, Is.EqualTo(televisionOrigin.DisplayTechnology?.Name));
				Assert.That(editedTelevisionInDb.Color?.Name, Is.EqualTo(televisionOrigin.Color?.Name));
				Assert.That(editedTelevisionInDb.Seller, Is.EqualTo(televisionOrigin.Seller));
			});
		}

		[Test]
		public async Task GetAllBrandsNamesAsync_ShouldReturnCorrectBrandsNames()
		{
			var result = await this.televisionService.GetAllBrandsNamesAsync();

			var expectedCount = this.data.Brands.Count();
			Assert.That(result.Count(), Is.EqualTo(expectedCount));

			var expected = this.data.Brands.Select(x => x.Name).OrderBy(x => x).ToList();
			Assert.That(expected.SequenceEqual<string>(result));
		}

		[Test]
		public async Task GetAllDisplaysSizesValuesAsync_ShouldReturnCorrectDisplaySizesValues()
		{
			var result = await this.televisionService.GetAllDisplaysSizesValuesAsync();

			var expectedCount = this.data.DisplaySizes.Count();
			Assert.That(result.Count(), Is.EqualTo(expectedCount));

			var expected = this.data.DisplaySizes.Select(x => x.Value).OrderBy(x => x).ToList();
			Assert.That(expected.SequenceEqual<double>(result));
		}

		[Test]
		public async Task GetAllTelevisionsAsync_ShouldReturnAllTelevisionsWhenThereAreNoSearchingParameters()
		{
			var result = await this.televisionService.GetAllTelevisionsAsync();

			var expectedCount = this.data.Televisions.Count();
			Assert.That(result.TotalTelevisionsCount, Is.EqualTo(expectedCount));
		}

		[Test]
		public async Task GetAllTelevisionsAsync_ShouldReturnCorrectTelevisionsAccordingToTheSearchingParameters()
		{
			var brandName = this.data.Brands.First().Name;
			var displaySizeValue = this.data.DisplaySizes.First().Value;
			var resolutionValue = this.data.Resolutions.First().Value;
			var keyword = "1";
			var sorting = Sorting.PriceMaxToMin;

			var result = await this.televisionService.GetAllTelevisionsAsync(
				brandName,
				displaySizeValue,
				resolutionValue,
				keyword,
				sorting);

			var expected = this.data.Televisions
				.Where(m => m.Brand.Name == brandName
							&& m.DisplaySize.Value == displaySizeValue
							&& m.Resolution.Value == resolutionValue)
				.Where(m => m.Brand.Name.Contains(keyword)
							|| m.Type.Name.Contains(keyword)
							|| (m.DisplayTechnology != null && m.DisplayTechnology.Name.Contains(keyword)))
				.OrderByDescending(m => m.Price)
				.ToList();

			Assert.That(result.TotalTelevisionsCount, Is.EqualTo(expected.Count));
		}

		[Test]
		public async Task GetAllTelevisionsAsync_ShouldReturnEmptyCollectionWhenThereIsNoTelevisionAccordingToTheSpecifiedCriteria()
		{
			var brandName = "invalid";

			var result = await this.televisionService.GetAllTelevisionsAsync(brandName);

			Assert.That(result.TotalTelevisionsCount, Is.Zero);
		}

		[Test]
		public async Task GetAllResolutionsValuesAsync_ShouldReturnCorrectResolutionsValues()
		{
			var result = await this.televisionService.GetAllResolutionsValuesAsync();

			var expectedCount = this.data.Resolutions.Count();
			Assert.That(result.Count(), Is.EqualTo(expectedCount));

			var expected = this.data.Resolutions.Select(x => x.Value).OrderBy(x => x).ToList();
			Assert.That(expected.SequenceEqual<string>(result));
		}

		[Test]
		public async Task GetTelevisionByIdAsTelevisionDetailsExportViewModelAsync_ShouldReturnTheCorrectTelevision()
		{
			var televisionId = this.data.Televisions.First().Id;

			var result = await this.televisionService.GetTelevisionByIdAsTelevisionDetailsExportViewModelAsync(televisionId);

			var expected = this.data.Televisions.First();

			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);

				Assert.That(result.Id, Is.EqualTo(expected.Id));
				Assert.That(result.Brand, Is.EqualTo(expected.Brand.Name));
				Assert.That(result.Price, Is.EqualTo(expected.Price));
				Assert.That(result.Warranty, Is.EqualTo(expected.Warranty));
				Assert.That(result.DisplaySize, Is.EqualTo(expected.DisplaySize.Value));
				Assert.That(result.Resolution, Is.EqualTo(expected.Resolution.Value));
				Assert.That(result.Type, Is.EqualTo(expected.Type.Name));
				Assert.That(result.ImageUrl, Is.EqualTo(expected.ImageUrl));
				Assert.That(result.AddedOn, Is.EqualTo(expected.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture)));
				Assert.That(result.Quantity, Is.EqualTo(expected.Quantity));
				Assert.That(result.Seller, Is.EqualTo(expected.Seller));
			});
		}

		[Test]
		public async Task GetTelevisionByIdAsTelevisionEditViewModelAsync_ShouldReturnCorrectTelevision()
		{
			var televisionId = this.data.Televisions.First().Id;

			var result = await this.televisionService.GetTelevisionByIdAsTelevisionEditViewModelAsync(televisionId);

			var expected = this.data.Televisions.First();

			Assert.Multiple(() =>
			{
				Assert.That(result, Is.Not.Null);

				Assert.That(result.Id, Is.EqualTo(expected.Id));
				Assert.That(result.Brand, Is.EqualTo(expected.Brand.Name));
				Assert.That(result.DisplaySize, Is.EqualTo(expected.DisplaySize.Value));
				Assert.That(result.Resolution, Is.EqualTo(expected.Resolution.Value));
				Assert.That(result.Type, Is.EqualTo(expected.Type.Name));
				Assert.That(result.Quantity, Is.EqualTo(expected.Quantity));
				Assert.That(result.Price, Is.EqualTo(expected.Price));
				Assert.That(result.Warranty, Is.EqualTo(expected.Warranty));
				Assert.That(result.ImageUrl, Is.EqualTo(expected.ImageUrl));
				Assert.That(result.Seller, Is.EqualTo(expected.Seller));
			});
		}

		[Test]
		public async Task GetUserTelevisionsAsync_ShouldReturnOnlyTelevisionsThatHaveSellerIdEqualToTheClientIdOfTheClientWithTheGivenUserId()
		{
			// Уверете се, че userId е валиден
			var userId = "User1";

			var result = await this.televisionService.GetUserTelevisionsAsync(userId);

			var clientId = this.data.Clients.FirstOrDefault(c => c.UserId == userId)?.Id;

			var expected = this.data.Televisions.Where(t => t.SellerId == clientId);

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Count(), Is.EqualTo(expected.Count()));

			foreach (var television in result)
			{
				var expectedTelevision = expected.First(t => t.Id == television.Id);
				Assert.Multiple(() =>
				{
					Assert.That(television.Brand, Is.EqualTo(expectedTelevision.Brand.Name));
					Assert.That(television.Price, Is.EqualTo(expectedTelevision.Price));
					Assert.That(television.Warranty, Is.EqualTo(expectedTelevision.Warranty));
					Assert.That(television.DisplaySize, Is.EqualTo(expectedTelevision.DisplaySize.Value));
					Assert.That(television.Resolution, Is.EqualTo(expectedTelevision.Resolution.Value));
					Assert.That(television.Type, Is.EqualTo(expectedTelevision.Type.Name));
					Assert.That(television.ImageUrl, Is.EqualTo(expectedTelevision.ImageUrl));
					Assert.That(television.AddedOn, Is.EqualTo(expectedTelevision.AddedOn.ToString("MMMM, yyyy", CultureInfo.InvariantCulture)));
					Assert.That(television.Quantity, Is.EqualTo(expectedTelevision.Quantity));
				});
			}
		}

		[Test]
		public async Task MarkTelevisionAsBoughtAsync_ShouldDecreaseTelevisionQuantityWhenGivenTelevisionIdIsValid()
		{
			var television = this.data.Televisions.First();

			var televisionQuantityBeforeBuying = television.Quantity;

			await this.televisionService.MarkTelevisionAsBoughtAsync(television.Id);

			var televisionQuantityAfterBuying = television.Quantity;

			Assert.That(televisionQuantityAfterBuying, Is.EqualTo(televisionQuantityBeforeBuying - 1));
		}
	}
}