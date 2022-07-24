using Demo.DAL.Entities;
using Demo.Pl.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.Pl.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager ,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        #region  Register
        public IActionResult Register()
        {
            return View();
        }  
        [HttpPost]
        public async Task<IActionResult>  Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                };
                var result= await userManager.CreateAsync(user ,model.Password);
                if(result.Succeeded)
                    return RedirectToAction(nameof(Login));
                foreach(var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(model);
            }
            return View(model);
        }
        #endregion

        #region  Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var Password = await userManager.CheckPasswordAsync(user, model.Password);

                    if (Password)

                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded) { 
                            return RedirectToAction("Index" ,"Home");
                        }
                    }
                }
            
            }
            return View(model);
        }
        #endregion

        #region  SignOut
        public async new Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        #endregion
    }
}
