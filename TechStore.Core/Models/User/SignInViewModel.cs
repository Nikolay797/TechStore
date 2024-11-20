using System.ComponentModel.DataAnnotations;


namespace TechStore.Core.Models.User
{
    public class SignInViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;

        [UIHint("hidden")]
        public string? ReturnUrl { get; set; }
    }
}
