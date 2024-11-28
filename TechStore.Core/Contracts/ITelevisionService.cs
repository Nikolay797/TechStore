﻿using TechStore.Core.Enums;
using TechStore.Core.Models.Television;

namespace TechStore.Core.Contracts
{
    public interface ITelevisionService
    {
        Task<TelevisionsQueryModel> GetAllTelevisionsAsync(string? brand = null, double? displaySize = null,
            string? resolution = null, string? keyword = null, Sorting sorting = Sorting.PriceMinToMax,
            int currentPage = 1);
        Task<IEnumerable<string>> GetAllBrandsNames();
        Task<IEnumerable<double>> GetAllDisplaysSizesValues();
        Task<IEnumerable<string>> GetAllResolutionsValues();
        Task<TelevisionDetailsExportViewModel> GetTelevisionByIdAsTelevisionDetailsExportViewModelAsync(int id);
        Task DeleteTelevisionAsync(int id);
        Task<int> AddTelevisionAsync(TelevisionImportViewModel model, string? userId);
        Task<TelevisionEditViewModel> GetTelevisionByIdAsTelevisionEditViewModelAsync(int id);
        Task<int> EditTelevisionAsync(TelevisionEditViewModel model);
    }
}
