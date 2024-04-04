using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Route_C41_G02_DAL.Models;
using Route_C41_G02_PL.ViewModels.User;
using System.Threading.Tasks;

namespace Route_C41_G02_PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        #region Sign Up
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if(ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user is null)
                {
                    user = new ApplicationUser()
                    {
                        FName = model.FirstName,
                        LName = model.LastName,
                        UserName = model.UserName,
                        Email = model.Email,
                        IsAgree = model.IsAgree,
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if(result.Succeeded)
                    {
                        return RedirectToAction(nameof(SignIn));
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                ModelState.AddModelError(string.Empty, "This User Name Is Already used from another account");

                

            }



            return View(model);
		}

		#endregion
	}
}
