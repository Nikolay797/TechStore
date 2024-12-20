﻿using Microsoft.EntityFrameworkCore;
using TechStore.Infrastructure.Data;

namespace TechStore.Tests.Mocks
{
	public static class DatabaseMock
	{
		public static ApplicationDbContext Instance
		{
			get
			{
				var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
					.UseInMemoryDatabase(Guid.NewGuid().ToString())
					.Options;

				return new ApplicationDbContext(dbContextOptions, false);
			}
		}
	}
}