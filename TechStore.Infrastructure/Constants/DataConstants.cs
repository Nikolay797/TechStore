using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Infrastructure.Constants
{
    public static class DataConstant
    {
        public static class UserConstants
        {
            public const int FirstNameMinLength = 4;
            public const int FirstNameMaxLength = 20;
            public const int LastNameMinLength = 4;
            public const int LastNameMaxLength = 20;
            public const int UserNameMinLength = 4;
            public const int UserNameMaxLength = 20;
            public const int EmailMinLength = 10;
            public const int EmailMaxLength = 40;
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 20;
        }

        public static class RoleConstants
        {
            public const string Administrator = "Administrator";

            public const string BestUser = "BestUser";
        }

        public static class BrandConstants
        {
            public const int BrandNameMinLength = 3;
            public const int BrandNameMaxLength = 20;
        }

        public static class ColorConstants
        {
            public const int ColorNameMinLength = 3;
            public const int ColorNameMaxLength = 10;
        }

        public static class CPUConstants
        {
            public const int CPUNameMinLength = 3;
            public const int CPUNameMaxLength = 30;
        }

        public static class DisplayCoverageConstants
        {
            public const int DisplayCoverageNameMinLength = 3;
            public const int DisplayCoverageNameMaxLength = 15;
        }

        public static class DisplayTechnologyConstants
        {
            public const int DisplayTechnologyNameMinLength = 3;
            public const int DisplayTechnologyNameMaxLength = 10;
        }

        public static class FormatConstants
        {
            public const int FormatNameMinLength = 3;
            public const int FormatNameMaxLength = 20;
        }

        public static class ResolutionConstants
        {
            public const int ResolutionValueMinLength = 5;
            public const int ResolutionValueMaxLength = 25;
        }

        public static class SensitivityConstants
        {
            public const int SensitivityRangeMinLength = 5;
            public const int SensitivityRangeMaxLength = 20;
        }

        public static class TypeConstants
        {
            public const int TypeNameMinLength = 3;
            public const int TypeNameMaxLength = 25;
        }

        public static class VideoCardConstants
        {
            public const int VideoCardNameMinLength = 3;
            public const int VideoCardNameMaxLength = 30;
        }

        public static class LaptopConstants
        {
            public const int RAMMinValue = 1;
            public const int RAMMaxValue = int.MaxValue;
            public const int SSDCapacityMinValue = 1;
            public const int SSDCapacityMaxValue = int.MaxValue;
            public const string ErrorMessageForInvalidLaptopId = "Invalid Laptop Id!";
            public const int LaptopsPerPage = 25;
        }

        public static class ProductConstants
        {
            public const int QuantityMinValue = 1;
            public const int QuantityMaxValue = int.MaxValue;
            public const int WarrantyMinValue = 0;
            public const int WarrantyMaxValue = int.MaxValue;
            public const string IntegerErrorMessage = "The field {0} must be an integer between {1} and {2}";
            public const string ErrorMessageForDeletedProduct = "The selected product is deleted!";
            public const string ErrorMessageForProductThatIsOutOfStock = "The selected product is out of stock!";
		}

        public static class ClientConstants
        {
            public const int MaxNumberOfAllowedSales = 10;
            public const string ErrorMessageForInvalidUserId = "Invalid User Id!";
            public const int RequiredNumberOfPurchasesToBeBestUser = 3;
		}
    }
}
