using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using static TechStore.Infrastructure.Constants.DataConstant.UserConstants;

namespace TechStore.Infrastructure.Data.Models.Account
{
    public class User : IdentityUser
    {
        [MaxLength(FirstNameMaxLength)]
        public string? FirstName { get; set; }

        [MaxLength(LastNameMaxLength)]
        public string? LastName { get; set; }
    }
}
