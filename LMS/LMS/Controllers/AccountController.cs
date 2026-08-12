using LMS.Models;
using LMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace LMS.Controllers;
public class AccountController(SignInManager<ApplicationUser> signInManager) : Controller
{
    [AllowAnonymous] public IActionResult Login(string? returnUrl=null)=>View(new LoginViewModel());
    [HttpPost,AllowAnonymous,ValidateAntiForgeryToken] public async Task<IActionResult> Login(LoginViewModel model,string? returnUrl=null)
    { if(ModelState.IsValid) { var result=await signInManager.PasswordSignInAsync(model.Email,model.Password,model.RememberMe,true); if(result.Succeeded)return LocalRedirect(returnUrl??"/"); ModelState.AddModelError(string.Empty,result.IsLockedOut?"Account is temporarily locked.":"Invalid email or password."); } return View(model); }
    [HttpPost,ValidateAntiForgeryToken] public async Task<IActionResult> Logout(){await signInManager.SignOutAsync();return RedirectToAction(nameof(Login));}
    [AllowAnonymous] public IActionResult AccessDenied()=>View();
}
