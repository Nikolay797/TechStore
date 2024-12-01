﻿using TechStore.Infrastructure.Data.Models;

namespace TechStore.Core.Models.Keyboard
{
    public class KeyboardDetailsExportViewModel : KeyboardExportViewModel
    {
        public string? Color { get; init; }
        public string? ImageUrl { get; init; }
        public string AddedOn { get; init; } = null!;
        public int Quantity { get; init; }
        public Client? Seller { get; init; }
        public string? SellerFirstName { get; init; }
        public string? SellerLastName { get; init; }
    }
}