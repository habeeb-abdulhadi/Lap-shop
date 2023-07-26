using Microsoft.AspNetCore.Mvc;
using LapShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Unicode;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;

namespace LapShop.Controllers
{
    public class UsersController : Controller
    {
        IUserStore<ApplicationUser> _userStore;
         IUserEmailStore<ApplicationUser> _emailStore;
        IEmailSender _emailSender;
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
         List<AuthenticationScheme> ExternalLogins;

        public UsersController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,IEmailSender emailSender, IUserStore<ApplicationUser> userStore)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _emailSender = emailSender;
           
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

        [AcceptVerbs("GET","POST")]
        public IActionResult VerifyEmail(string Email)
      {
            ApplicationUser vddv = new ApplicationUser();
            vddv.Email = Email;
            var result =  _userManager.FindByEmailAsync(vddv.Email);
            
                
            if(result.Result != null)
            {
                return Json($"Email {Email} is already in use.");
            }
            return Json(true);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserModel model)
        {
            model.ReturnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var returnUrl = model.ReturnUrl;
            if (!ModelState.IsValid)
                return View("Register", model);

            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
             

            };


            try
            {
                await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

                    await _userManager.AddToRoleAsync(user, "Customer");
                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    var callbackUrl = Url.Action(
                       action: "OnConfirmc",
                       controller: "Users",
                        values: new { userId = userId, code = code , returnUrl = returnUrl },
                        protocol: Request.Scheme);
                    await _emailSender.SendEmailAsync(model.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {

                        return RedirectToAction("checkacount");
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    //if (loginResult.Succeeded)
                    //    if (string.IsNullOrEmpty(model.ReturnUrl))
                    //        return Redirect("~/");
                    //    else
                    //        return Redirect(model.ReturnUrl);
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

        public IActionResult checkacount()
        {
            
            return View();
        }

        
         
       
        public async Task<IActionResult> OnConfirmc(string userId, string code)
        {
            
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }
            var StatusMessage = new messagConfirm();
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            StatusMessage.StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return View(StatusMessage);
        }



        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Register(UserModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View("Register", model);

        //    ApplicationUser user = new ApplicationUser()
        //    {
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        Email = model.Email,
        //        UserName = model.Email

        //    };


        //    try
        //    {
        //        var result = await _userManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded)
        //        {
        //            var loginResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
        //            var myUser = await _userManager.FindByEmailAsync(user.Email);
        //            await _userManager.AddToRoleAsync(myUser, "Customer");
        //            if (loginResult.Succeeded)
        //                if (string.IsNullOrEmpty(model.ReturnUrl))
        //                    return Redirect("~/");
        //                else
        //                    return Redirect(model.ReturnUrl);
        //        }
        //        else
        //        {

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception();
        //    }

        //    return View(new UserModel());
        //}



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
        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}
