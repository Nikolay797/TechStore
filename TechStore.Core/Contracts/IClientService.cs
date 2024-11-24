using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Core.Contracts
{
    public interface IClientService
    {
        Task<int> GetNumberOfActiveSales(string userId);
    }
}
