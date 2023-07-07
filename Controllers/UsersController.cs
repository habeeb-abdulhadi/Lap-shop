using Microsoft.AspNetCore.Mvc;
using LapShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace LapShop.Controllers
{
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login(string returnUrl)
        {
            UserModel model = new UserModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        public async Task<IActionResult> LoginOut()
        {
            await _signInManager.SignOutAsync();
            return Redirect("~/");
        }

        public IActionResult Register(string? returnUrl = null)
        {
            UserModel model = new UserModel()
            {
                ReturnUrl = returnUrl
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel model)
        {
            //if (!ModelState.IsValid)
            //    return View("Register", model);

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
                
            };
            UserModel returnurl = new UserModel()
            {
               
                ReturnUrl = model.ReturnUrl

            };
            if (string.IsNullOrEmpty(user.FirstName))
            {
                return RedirectToAction("Register",returnurl);
            }
            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                    var myUser = await _userManager.FindByEmailAsync(user.Email);
                    await _userManager.AddToRoleAsync(myUser, "Customer");
                    if (loginResult.Succeeded)
                        if (string.IsNullOrEmpty(model.ReturnUrl))
                            return Redirect("~/");
                        else
                            return Redirect(model.ReturnUrl);
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            
            return View(new UserModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            try
            {
                var loginResult = await _signInManager.PasswordSignInAsync(user.Email, model.Password, true, true);
                if (loginResult.Succeeded)
                {
                  if (string.IsNullOrEmpty(model.ReturnUrl))
                        return Redirect("~/");
                    else
                        return Redirect(model.ReturnUrl);
                }
            }
            catch (Exception ex)
            {

            }
            return View(new UserModel());
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
