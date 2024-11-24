using TechStore.Core.Models.Laptop;

namespace TechStore.Core.Contracts
{
    public interface ILaptopService
    {
        Task<IEnumerable<LaptopExportViewModel>> GetAllLaptopsAsync();
        Task<LaptopDetailsExportViewModel> GetLaptopByIdAsLaptopDetailsExportViewModelAsync(int id);
        Task DeleteLaptopAsync(int id);
        Task<int> AddLaptopAsync(LaptopImportViewModel model, string? userId);
        Task<LaptopEditViewModel> GetLaptopByIdAsLaptopEditViewModelAsync(int id);
        Task<int> EditLaptopAsync(LaptopEditViewModel model);
    }
}
