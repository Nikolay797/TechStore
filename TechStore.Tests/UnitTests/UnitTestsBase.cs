using Microsoft.AspNetCore.Identity;
using TechStore.Infrastructure.Data;
using TechStore.Infrastructure.Data.Models;
using TechStore.Infrastructure.Data.Models.Account;
using TechStore.Infrastructure.Data.Models.AttributesClasses;
using TechStore.Tests.Mocks;
using Type = TechStore.Infrastructure.Data.Models.AttributesClasses.Type;
using static TechStore.Infrastructure.Constants.DataConstant.RoleConstants;


namespace TechStore.Tests.UnitTests
{
	public class UnitTestsBase
	{
		protected ApplicationDbContext data;

		[OneTimeSetUp]
		public void SetUpBase()
		{
			this.data = DatabaseMock.Instance;

			this.SeedDatabase();
		}

		[OneTimeTearDown]
		public void TearDownBase()
		{
			this.data.Database.EnsureDeleted();
			this.data.Dispose();
		}

		private void SeedDatabase()
		{
			this.ClearDatabase();

			var brands = this.CreateBrands();

			this.data.AddRange(brands);

			var cpus = this.CreateCPUs();

			this.data.AddRange(cpus);

			var rams = this.CreateRAMs();

			this.data.AddRange(rams);

			var ssdCapacities = this.CreateSSDCapacities();

			this.data.AddRange(ssdCapacities);

			var videoCards = this.CreateVideoCards();

			this.data.AddRange(videoCards);

			var types = this.CreateTypes();

			this.data.AddRange(types);

			var displaySizes = CreateDisplaySizes();

			this.data.AddRange(displaySizes);

			var laptops = this.CreateLaptops();

			this.data.AddRange(laptops);

			var resolutions = this.CreateResolutions();

			this.data.AddRange(resolutions);

			var televisions = this.CreateTelevisions();

			this.data.AddRange(televisions);

			var keyboards = this.CreateKeyboards();

			this.data.AddRange(keyboards);

			var sensitivities = this.CreateSensitivities();

			this.data.AddRange(sensitivities);

			var mice = this.CreateMice();

			this.data.AddRange(mice);

			var headphones = this.CreateHeadphones();

			this.data.AddRange(headphones);

			var smartwatches = this.CreateSmartwatches();

			this.data.AddRange(smartwatches);

			var users = this.CreateUsers();

			this.data.AddRange(users);

			var roles = this.CreateRoles();

			this.data.AddRange(roles);

			var userRole = new IdentityUserRole<string>() { UserId = "User1", RoleId = "Role1" };

			this.data.Add(userRole);

			var client = new Client() { Id = 1, UserId = "User1" };

			this.data.Add(client);

			this.data.SaveChanges();
		}

		private void ClearDatabase()
		{
			this.data.Laptops.RemoveRange(this.data.Laptops);
			this.data.Brands.RemoveRange(this.data.Brands);
			this.data.CPUs.RemoveRange(this.data.CPUs);
			this.data.RAMs.RemoveRange(this.data.RAMs);
			this.data.SSDCapacities.RemoveRange(this.data.SSDCapacities);
			this.data.VideoCards.RemoveRange(this.data.VideoCards);
			this.data.Types.RemoveRange(this.data.Types);
			this.data.DisplaySizes.RemoveRange(this.data.DisplaySizes);
			this.data.Users.RemoveRange(this.data.Users);
			this.data.Clients.RemoveRange(this.data.Clients);
			this.data.Roles.RemoveRange(this.data.Set<IdentityRole>());
			this.data.RemoveRange(this.data.Set<IdentityUserRole<string>>());

			this.data.SaveChanges();
		}

		private IEnumerable<IdentityRole> CreateRoles()
		{
			return new List<IdentityRole>()
			{
				new IdentityRole() { Id = "Role1", Name = "Role1" },
				new IdentityRole() { Id = "Administrator", Name = Administrator },
			};
		}

		private IEnumerable<User> CreateUsers()
		{
			return new List<User>()
			{
				new User() { Id = "User1" },
				new User() { Id = "User2" },
			};
		}

		private IEnumerable<SmartWatch> CreateSmartwatches()
		{
			return new List<SmartWatch>()
			{
				new SmartWatch()
				{
					Id = 1,
					BrandId = 1,
					Price = 1000.00M,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
				},
				new SmartWatch()
				{
					Id = 2,
					BrandId = 2,
					Price = 2000.00M,
					Warranty = 2,
					Quantity = 2,
					AddedOn = DateTime.UtcNow.Date,
				},
				new SmartWatch()
				{
					Id = 3,
					BrandId = 3,
					Price = 3000.00M,
					Warranty = 3,
					Quantity = 3,
					AddedOn = DateTime.UtcNow.Date,
				},
				new SmartWatch()
				{
					Id = 4,
					BrandId = 1,
					Price = 4000.00M,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
					SellerId = 1,
				},
				new SmartWatch()
				{
					Id = 5,
					BrandId = 1,
					Price = 5000.00M,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
				},
			};
		}

		private IEnumerable<Headphone> CreateHeadphones()
		{
			return new List<Headphone>()
			{
				new Headphone()
				{
					Id = 1,
					BrandId = 1,
					Price = 1000.00M,
					TypeId = 1,
					IsWireless = true,
					HasMicrophone = true,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Headphone()
				{
					Id = 2,
					BrandId = 2,
					Price = 2000.00M,
					TypeId = 2,
					IsWireless = false,
					HasMicrophone = false,
					Warranty = 2,
					Quantity = 2,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Headphone()
				{
					Id = 3,
					BrandId = 3,
					Price = 3000.00M,
					TypeId = 3,
					IsWireless = true,
					HasMicrophone = true,
					Warranty = 3,
					Quantity = 3,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Headphone()
				{
					Id = 4,
					BrandId = 1,
					Price = 4000.00M,
					TypeId = 1,
					IsWireless = false,
					HasMicrophone = false,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
					SellerId = 1,
				},
				new Headphone()
				{
					Id = 5,
					BrandId = 1,
					Price = 5000.00M,
					TypeId = 1,
					IsWireless = true,
					HasMicrophone = true,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
				},
			};
		}

		private IEnumerable<Mouse> CreateMice()
		{
			return new List<Mouse>()
			{
				new Mouse()
				{
					Id = 1,
					BrandId = 1,
					Price = 1000.00M,
					IsWireless = true,
					TypeId = 1,
					SensitivityId = 1,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
					SellerId = 1
				},
				new Mouse()
				{
					Id = 2,
					BrandId = 2,
					Price = 2000.00M,
					IsWireless = false,
					TypeId = 2,
					SensitivityId = 2,
					Warranty = 2,
					Quantity = 2,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Mouse()
				{
					Id = 3,
					BrandId = 3,
					Price = 3000.00M,
					IsWireless = true,
					TypeId = 3,
					SensitivityId = 3,
					Warranty = 3,
					Quantity = 3,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Mouse()
				{
					Id = 4,
					BrandId = 1,
					Price = 4000.00M,
					IsWireless = false,
					TypeId = 1,
					SensitivityId = 1,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
					SellerId = 1,
				},
				new Mouse()
				{
					Id = 5,
					BrandId = 1,
					Price = 5000.00M,
					IsWireless = true,
					TypeId = 1,
					SensitivityId = 1,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
				},
			};
		}

		private IEnumerable<Sensitivity> CreateSensitivities()
		{
			return new List<Sensitivity>()
			{
				new Sensitivity() { Id = 1, Range = "0 - 100 DPI" },
				new Sensitivity() { Id = 2, Range = "100 - 200 DPI" },
				new Sensitivity() { Id = 3, Range = "200 - 300 DPI" },
			};
		}

		private IEnumerable<Keyboard> CreateKeyboards()
		{
			return new List<Keyboard>()
			{
				new Keyboard()
				{
					Id = 1,
					BrandId = 1,
					Price = 1000.00M,
					IsWireless = true,
					TypeId = 1,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
					SellerId = 1
				},
				new Keyboard()
				{
					Id = 2,
					BrandId = 2,
					Price = 2000.00M,
					IsWireless = false,
					TypeId = 2,
					Warranty = 2,
					Quantity = 2,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Keyboard()
				{
					Id = 3,
					BrandId = 3,
					Price = 3000.00M,
					IsWireless = true,
					TypeId = 3,
					Warranty = 3,
					Quantity = 3,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Keyboard()
				{
					Id = 4,
					BrandId = 1,
					Price = 4000.00M,
					IsWireless = false,
					TypeId = 1,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
					SellerId = 1,
				},
				new Keyboard()
				{
					Id = 5,
					BrandId = 1,
					Price = 5000.00M,
					IsWireless = true,
					TypeId = 1,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
				},
			};
		}

		private IEnumerable<Television> CreateTelevisions()
		{
			return new List<Television>()
			{
				new Television()
				{
					Id = 1,
					BrandId = 1,
					Price = 1000.00M,
					DisplaySizeId = 1,
					TypeId = 1,
					ResolutionId = 1,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Television()
				{
					Id = 2,
					BrandId = 2,
					Price = 2000.00M,
					DisplaySizeId = 2,
					TypeId = 2,
					ResolutionId = 2,
					Warranty = 2,
					Quantity = 2,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Television()
				{
					Id = 3,
					BrandId = 3,
					Price = 3000.00M,
					DisplaySizeId = 3,
					TypeId = 3,
					ResolutionId = 3,
					Warranty = 3,
					Quantity = 3,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Television()
				{
					Id = 4,
					BrandId = 1,
					Price = 4000.00M,
					DisplaySizeId = 1,
					TypeId = 1,
					ResolutionId = 1,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
					SellerId = 1,
				},
				new Television()
				{
					Id = 5,
					BrandId = 1,
					Price = 5000.00M,
					DisplaySizeId = 1,
					TypeId = 1,
					ResolutionId = 1,
					Warranty = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
				},
			};
		}

		private IEnumerable<Resolution> CreateResolutions()
		{
			return new List<Resolution>()
			{
				new Resolution() { Id = 1, Value = "1000x1000" },
				new Resolution() { Id = 2, Value = "2000x2000" },
				new Resolution() { Id = 3, Value = "3000x3000" },
			};
		}

		private IEnumerable<Laptop> CreateLaptops()
		{
			return new List<Laptop>()
			{
				new Laptop()
				{
					Id = 1,
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
				},
				new Laptop()
				{
					Id = 2,
					BrandId = 2,
					CPUId = 2,
					RAMId = 2,
					SSDCapacityId = 2,
					VideoCardId = 2,
					Price = 2000.00M,
					DisplaySizeId = 2,
					Warranty = 2,
					TypeId = 2,
					Quantity = 2,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Laptop()
				{
					Id = 3,
					BrandId = 3,
					CPUId = 3,
					RAMId = 3,
					SSDCapacityId = 3,
					VideoCardId = 3,
					Price = 3000.00M,
					DisplaySizeId = 3,
					Warranty = 3,
					TypeId = 3,
					Quantity = 3,
					AddedOn = DateTime.UtcNow.Date,
				},
				new Laptop()
				{
					Id = 4,
					BrandId = 1,
					CPUId = 1,
					RAMId = 1,
					SSDCapacityId = 1,
					VideoCardId = 1,
					Price = 4000.00M,
					DisplaySizeId = 1,
					Warranty = 0,
					TypeId = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
					SellerId = 1,
				},
				new Laptop()
				{
					Id = 5,
					BrandId = 1,
					CPUId = 1,
					RAMId = 1,
					SSDCapacityId = 1,
					VideoCardId = 1,
					Price = 5000.00M,
					DisplaySizeId = 1,
					Warranty = 1,
					TypeId = 1,
					Quantity = 1,
					AddedOn = DateTime.UtcNow.Date,
				},
			};
		}

		private IEnumerable<DisplaySize> CreateDisplaySizes()
		{
			return new List<DisplaySize>()
			{
				new DisplaySize() { Id = 1, Value = 1 },
				new DisplaySize() { Id = 2, Value = 2 },
				new DisplaySize() { Id = 3, Value = 3 },
			};
		}

		private IEnumerable<Type> CreateTypes()
		{
			return new List<Type>()
			{
				new Type() { Id = 1, Name = "Type1" },
				new Type() { Id = 2, Name = "Type2" },
				new Type() { Id = 3, Name = "Type3" },
			};
		}

		private IEnumerable<VideoCard> CreateVideoCards()
		{
			return new List<VideoCard>()
			{
				new VideoCard() { Id = 1, Name = "VideoCard1" },
				new VideoCard() { Id = 2, Name = "VideoCard2" },
				new VideoCard() { Id = 3, Name = "VideoCard3" },
			};
		}

		private IEnumerable<SSDCapacity> CreateSSDCapacities()
		{
			return new List<SSDCapacity>()
			{
				new SSDCapacity() { Id = 1, Value = 1 },
				new SSDCapacity() { Id = 2, Value = 2 },
				new SSDCapacity() { Id = 3, Value = 3 },
			};
		}

		private IEnumerable<RAM> CreateRAMs()
		{
			return new List<RAM>()
			{
				new RAM() { Id = 1, Value = 1 },
				new RAM() { Id = 2, Value = 2 },
				new RAM() { Id = 3, Value = 3 },
			};
		}

		private IEnumerable<CPU> CreateCPUs()
		{
			return new List<CPU>()
			{
				new CPU() { Id = 1, Name = "CPU1" },
				new CPU() { Id = 2, Name = "CPU2" },
				new CPU() { Id = 3, Name = "CPU3" },
			};
		}

		private IEnumerable<Brand> CreateBrands()
		{
			return new List<Brand>()
			{
				new Brand() { Id = 1, Name = "Brand1" },
				new Brand() { Id = 2, Name = "Brand2" },
				new Brand() { Id = 3, Name = "Brand3" },
			};
		}
	}
}