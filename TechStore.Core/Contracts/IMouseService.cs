using TechStore.Core.Enums;
using TechStore.Core.Models.Mice;

namespace TechStore.Core.Contracts
{
	public interface IMouseService
	{
		Task<MiceQueryModel> GetAllMiceAsync(
			string? type = null,
			string? sensitivity = null,
			Wireless wireless = Wireless.Regardless,
			string? keyword = null,
			Sorting sorting = Sorting.Newest,
			int currentPage = 1);
		Task<IEnumerable<string>> GetAllMiceTypesAsync();
		Task<IEnumerable<string>> GetAllMiceSensitivitiesAsync();
	}
}
