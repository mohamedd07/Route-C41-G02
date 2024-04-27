using System.ComponentModel.DataAnnotations;

namespace Route_C41_G02_PL.ViewModels.User
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email Is required")]
		[EmailAddress(ErrorMessage = "Invalid Email")]
		public string Email { get; set; }
	}
}
