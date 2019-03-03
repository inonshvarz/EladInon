using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EladInon.Models;
using EladInon.ViewModels;
using EladInon.Data;

namespace EladInon.Controllers
{
    public class AccountController : Controller
    {
        private readonly PhotoContext _context;

        public AccountController(PhotoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Logoff()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private User Login(AuthenticationDetails details)
        {
            return _context.Users.FirstOrDefault(user => user.UserName == details.UserName && user.Password == Models.User.EncryptedPassword(details.Password));
        }

        private User UserForChangePassword(string password)
        {
            return _context.Users.FirstOrDefault(user => user.Id == HttpContext.GetUserId() && user.Password == Models.User.EncryptedPassword(password));
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password,IsPersistent")] AuthenticationDetails authDetails, string returnUrl)
        {
            try
            {
                var authenticatedUser = Login(authDetails);
                if (authenticatedUser == null)
                {
                    // TODO send to login with failedToAuthenticate=true
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }

                #region snippet1
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, authenticatedUser.UserName),
                    new Claim(ClaimTypes.Email,authenticatedUser.EMail),
                    new Claim("UserId", authenticatedUser.Id.ToString())
                };

                if (authenticatedUser.IsAdmin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                }
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    IsPersistent = authDetails.IsPersistent,
                    // Whether the authentication session is persisted across 
                    // multiple requests. Required when setting the 
                    // ExpireTimeSpan option of CookieAuthenticationOptions 
                    // set with AddCookie. Also required when setting 
                    ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(10.0)),

                    IssuedUtc = DateTimeOffset.UtcNow,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                #endregion
                if (returnUrl != null)
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Internal error at login.");
                //log failure
            }

            // Something failed. Redisplay the form.
            return View(authDetails);
        }

     
        public IActionResult Login(bool? failedToAuthenticate, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

      
       
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword([Bind("Password,NewPassword,ReNewPassword")] ChangePasswordDetails changePasswordDetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (changePasswordDetails.NewPassword == changePasswordDetails.ReNewPassword)
                    {
                        var user = UserForChangePassword(changePasswordDetails.Password);
                        if (user != null)
                        {
                            user.Password = changePasswordDetails.NewPassword;
                            _context.Update(user);
                            _context.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "סיסמה לא נכונה");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "הסיסמאות שהוכנסו אינן תואמות");
                    }
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError(string.Empty, "שגיאה התרחשה בעת החלפת הסיסמה");
            }

            return View(changePasswordDetails);
        }
    }
}