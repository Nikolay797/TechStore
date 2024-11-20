using TechStore.Core.Models.Laptop;

namespace TechStore.Core.Contracts
{
    public interface ILaptopService
    {
        Task<IEnumerable<LaptopExportViewModel>> GetAllLaptopsAsync();
    }
}
