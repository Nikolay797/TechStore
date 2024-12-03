using TechStore.Core.Enums;
using TechStore.Core.Models.Smartwatch;

namespace TechStore.Core.Contracts
{
	public interface ISmartwatchService
	{
		Task<SmartwatchesQueryModel> GetAllSmartwatchesAsync(
			string? keyword = null,
			Sorting sorting = Sorting.Newest,
			int currentPage = 1);
		Task<SmartwatchDetailsExportViewModel> GetSmartwatchByIdAsSmartwatchDetailsExportViewModelAsync(int id);
		Task DeleteSmartwatchAsync(int id);
		Task<int> AddSmartwatchAsync(SmartwatchImportViewModel model, string? userId);
		Task<SmartwatchEditViewModel> GetSmartwatchByIdAsSmartwatchEditViewModelAsync(int id);
		Task<int> EditSmartwatchAsync(SmartwatchEditViewModel model);
		Task<IEnumerable<SmartwatchExportViewModel>> GetUserSmartwatchesAsync(string userId);
	}
}
