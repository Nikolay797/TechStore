﻿using TechStore.Core.Enums;
using TechStore.Core.Models.Headphone;

namespace TechStore.Core.Contracts
{
	public interface IHeadphoneService
	{
		Task<HeadphonesQueryModel> GetAllHeadphonesAsync(
			string? type = null,
			Wireless wireless = Wireless.Regardless,
			string? keyword = null,
			Sorting sorting = Sorting.Newest,
			int currentPage = 1);
		Task<IEnumerable<string>> GetAllHeadphonesTypesAsync();
		Task<HeadphoneDetailsExportViewModel> GetHeadphoneByIdAsHeadphoneDetailsExportViewModelAsync(int id);
		Task DeleteHeadphoneAsync(int id);
		Task<int> AddHeadphoneAsync(HeadphoneImportViewModel model, string? userId);
		Task<HeadphoneEditViewModel> GetHeadphoneByIdAsHeadphoneEditViewModelAsync(int id);
		Task<int> EditHeadphoneAsync(HeadphoneEditViewModel model);
		Task<IEnumerable<HeadphoneDetailsExportViewModel>> GetUserHeadphonesAsync(string userId);
		Task MarkHeadphoneAsBoughtAsync(int id);
	}
}
