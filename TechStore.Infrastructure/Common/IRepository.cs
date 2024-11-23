using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Infrastructure.Common
{
    public interface IRepository
    {
        IQueryable<T> AllAsReadOnly<T>(Expression<Func<T, bool>> condition)
            where T : class;
        IQueryable<T> All<T>(Expression<Func<T, bool>> condition)
            where T : class;

        Task<int> SaveChangesAsync();
    }
}
