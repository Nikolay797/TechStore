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

            public const int FirstNameMaxLength = 20;

            public const int LastNameMaxLength = 20;

            //public const int UsernameMaxLength = 20;

            //public const int EmailMaxLength = 40;

            public const string AdminUserName = "admin";

            public const string TopUserUserName = "topUser";
        }

        public static class RoleConstants
        {
            public const string Administrator = "Administrator";

            public const string TopUser = "TopUser";
        }

        public static class BrandConstants
        {
            public const int BrandNameMaxLength = 20;
        }

        public static class ColorConstants
        {
            public const int ColorNameMaxLength = 10;
        }

        public static class CPUConstants
        {
            public const int CPUNameMaxLength = 30;
        }

        public static class DisplayCoverageConstants
        {

            public const int DisplayCoverageNameMaxLength = 15;
        }

        public static class DisplayTechnologyConstants
        {
            public const int DisplayTechnologyNameMaxLength = 10;
        }

        public static class FormatConstants
        {
			public const int FormatNameMaxLength = 20;
        }

        public static class ResolutionConstants
        {
            public const int ResolutionValueMaxLength = 25;
        }

        public static class SensitivityConstants
        {
            public const int SensitivityRangeMaxLength = 20;
        }

        public static class TypeConstants
        {
			public const int TypeNameMaxLength = 25;
        }

        public static class VideoCardConstants
        {
			public const int VideoCardNameMaxLength = 30;
        }
    }
}
