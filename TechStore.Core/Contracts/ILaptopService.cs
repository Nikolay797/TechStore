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
	        string? keyWord = null,
	        Sorting sorting = Sorting.PriceMinToMax, int currentPage = 1);

        Task<LaptopDetailsExportViewModel> GetLaptopByIdAsLaptopDetailsExportViewModelAsync(int id);
        Task DeleteLaptopAsync(int id);
        Task<int> AddLaptopAsync(LaptopImportViewModel model, string? userId);
        Task<LaptopEditViewModel> GetLaptopByIdAsLaptopEditViewModelAsync(int id);
        Task<int> EditLaptopAsync(LaptopEditViewModel model);
        Task<IEnumerable<LaptopDetailsExportViewModel>> GetUserLaptopsAsync(string userId);
        Task MarkLaptopAsBought(int id);
        Task<IEnumerable<string>> GetAllCpusNames();
        Task<IEnumerable<int>> GetAllRamsValues();
        Task<IEnumerable<int>> GetAllSsdCapacitiesValues();
        Task<IEnumerable<string>> GetAllVideoCardsNames();
	}
}
