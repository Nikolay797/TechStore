using TechStore.Core.Enums;
using TechStore.Core.Models.Laptop;

namespace TechStore.Core.Contracts
{
    public interface ILaptopService
    {
        Task<LaptopsQueryModel> GetAllLaptopsAsync
			(string? cpu = null,
	        int? ram = null,
	        int? ssdCapacity = null,
	        string? videoCard = null,
	        string? keyword = null,
	        Sorting sorting = Sorting.Newest, int currentPage = 1);

        Task<LaptopDetailsExportViewModel> GetLaptopByIdAsLaptopDetailsExportViewModelAsync(int id);
        Task DeleteLaptopAsync(int id);
        Task<int> AddLaptopAsync(LaptopImportViewModel model, string? userId);
        Task<LaptopEditViewModel> GetLaptopByIdAsLaptopEditViewModelAsync(int id);
        Task<int> EditLaptopAsync(LaptopEditViewModel model);
        Task<IEnumerable<LaptopDetailsExportViewModel>> GetUserLaptopsAsync(string userId);
        Task MarkLaptopAsBoughtAsync(int id);
        Task<IEnumerable<string>> GetAllCpusNamesAsync();
        Task<IEnumerable<int>> GetAllRamsValuesAsync();
        Task<IEnumerable<int>> GetAllSsdCapacitiesValuesAsync();
        Task<IEnumerable<string>> GetAllVideoCardsNamesAsync();
	}
}
