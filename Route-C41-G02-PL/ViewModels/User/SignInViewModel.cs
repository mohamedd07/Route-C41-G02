using System.ComponentModel.DataAnnotations;

namespace Route_C41_G02_PL.ViewModels.User
{
    public class SignInViewModel
    {
        [Required(ErrorMessage = "Email Is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public  bool RememberMe { get; set; }
    }
}
