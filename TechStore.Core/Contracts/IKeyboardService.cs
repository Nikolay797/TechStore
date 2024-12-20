﻿using TechStore.Core.Enums;
using TechStore.Core.Models.Keyboard;

namespace TechStore.Core.Contracts
{
    public interface IKeyboardService
    {
        Task<KeyboardsQueryModel> GetAllKeyboardsAsync(string? format = null, string? type = null,
            Wireless wireless = Wireless.Regardless, string? keyword = null, Sorting sorting = Sorting.Newest,
            int currentPage = 1);
        Task<IEnumerable<string>> GetAllKeyboardsFormatsAsync();
        Task<IEnumerable<string>> GetAllKeyboardsTypesAsync();
        Task<KeyboardDetailsExportViewModel> GetKeyboardByIdAsKeyboardDetailsExportViewModelAsync(int id);
        Task DeleteKeyboardAsync(int id);
        Task<int> AddKeyboardAsync(KeyboardImportViewModel model, string? userId);
        Task<KeyboardEditViewModel> GetKeyboardByIdAsKeyboardEditViewModelAsync(int id);
        Task<int> EditKeyboardAsync(KeyboardEditViewModel model);
        Task<IEnumerable<KeyboardDetailsExportViewModel>> GetUserKeyboardsAsync(string userId);
        Task MarkKeyboardAsBoughtAsync(int id);
	}
}
