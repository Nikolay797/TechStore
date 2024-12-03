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
	}
}
