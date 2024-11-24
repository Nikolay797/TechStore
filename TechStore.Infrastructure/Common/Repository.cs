using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechStore.Infrastructure.Data;

namespace TechStore.Infrastructure.Common
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync<T>(T entity)
            where T : class
        {
            await this.DbSet<T>().AddAsync(entity);
        }

        public IQueryable<T> All<T>(Expression<Func<T, bool>> condition)
            where T : class
        {
            return this.DbSet<T>().Where(condition);
        }

        public IQueryable<T> AllAsReadOnly<T>(Expression<Func<T, bool>> condition)
            where T : class
        {
            return this.DbSet<T>().AsNoTracking().Where(condition);
        }

        public async Task<T?> GetByPropertyAsync<T>(Expression<Func<T, bool>> condition)
            where T : class
        {
            return await this.DbSet<T>().Where(condition).FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }

        private DbSet<T> DbSet<T>()
            where T : class
        {
            return this.dbContext.Set<T>();
        }
    }
}
