using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Core.Constants
{
    public static class Constants
    {
        public static class User
        {
            public const int FirstNameMinLength = 2;
            public const int LastNameMinLength = 2;
            public const int UsernameMinLength = 2;
            public const int UserNameMaxLength = 20;

            public const int EmailMinLength = 10;
            public const int EmailMaxLength = 40;
            public const int PasswordMinLength = 4;
            public const int PasswordMaxLength = 20;
        }
    }
}
