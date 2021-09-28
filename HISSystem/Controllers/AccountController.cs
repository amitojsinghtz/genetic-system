using HISSystem.Helper;
using HISSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.IServices;
using Service.UnitOfServices;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using GeneticSystem.Areas.Admin.Models;

namespace HISSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfService db;
        public AccountController(IUnitOfService db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            var image = db.CompanyProfileService.GetImageByTypeId(180);
            var logo = db.CompanyProfileService.GetImageByTypeId(181);
            ViewBag.img = String.Format("data:image/gif;base64,{0}", image);
            string something = String.Format("data:image/gif;base64,{0}", logo);
            HttpContext.Session.SetString("logo", something);
            //var option = new CookieOptions();
            //option.Expires = DateTime.Now.AddHours(100);
            //Response.Cookies.Append("LoginBackdrop", image, option);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (model.UserName == null && model.Password == null)
            {
                return View();
            }
            else
            {
                //var user = db.UserService.Login(model.UserName, /*CommonMethod.Encrypt(*/model.Password);
                //if (user != null)
                //{
                //    var option = new CookieOptions();
                //    option.Expires = DateTime.Now.AddHours(10);
                //    Response.Cookies.Append("UserName", user.UserName, option);
                //    Response.Cookies.Append("RoleID", Convert.ToString(user.RoleID), option);
                //    Response.Cookies.Append("ID", Convert.ToString(user.ID), option);
                //    HttpContext.Session.SetString("UserName", user.UserName);
                //    HttpContext.Session.SetString("RoleID", Convert.ToString(user.RoleID));
                //    HttpContext.Session.SetString("Image", user.ImagePath ?? "/uploaded/neon-after-effects-template-graphics-pack-13-1000x562.jpg");
                //    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                //    identity.AddClaim(new Claim(ClaimTypes.Name, user.Role.Name));
                //    identity.AddClaim(new Claim("User", Convert.ToString(user.RoleID)));
                //    identity.AddClaim(new Claim(ClaimTypes.GivenName, user.UserName));
                //    identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));

                //    var principal = new ClaimsPrincipal(identity);
                //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                //    return Redirect("/admin/dashboard/index");


                var user = db.UserService.Login(model.UserName, CommonMethod.Encrypt(model.Password));
                var pass = CommonMethod.Encrypt(model.Password);
                if (user != null)
                {
                    var option = new CookieOptions();
                    option.Expires = DateTime.Now.AddHours(10);
                    Response.Cookies.Append("UserName", user.UserName, option);
                    Response.Cookies.Append("RoleID", Convert.ToString(user.RoleID), option);
                    Response.Cookies.Append("ID", Convert.ToString(user.ID), option);
                    HttpContext.Session.SetString("FullName", Convert.ToString((user.EnFirstName ?? "") + " " + (user.EnSecondName ?? "") + " " + (user.EnThirdName)));
                    HttpContext.Session.SetString("UserName", user.UserName);
                    HttpContext.Session.SetInt32("UserID", user.ID);
                    HttpContext.Session.SetString("RoleID", Convert.ToString(user.RoleID));
                    if (user.ImagePath != null)
                        HttpContext.Session.SetString("Image", user.ImagePath);

                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Role.Name));
                    identity.AddClaim(new Claim("User", Convert.ToString(user.RoleID)));
                    identity.AddClaim(new Claim(ClaimTypes.GivenName, user.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));

                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    
                    if(user.RoleID == 5)
                    {
                        return Redirect("/admin/followup/index");
                    }
                    else
                    {
                        return Redirect("/admin/dashboard/index");
                    }
                    
                }
                else
                {
                    var image = db.CompanyProfileService.GetImageByTypeId(180);
                    var logo = db.CompanyProfileService.GetImageByTypeId(181);
                    ViewBag.img = String.Format("data:image/png;base64,{0}", image);
                    string something = String.Format("data:image/png;base64,{0}", logo);
                    HttpContext.Session.SetString("logo", something);
                    return View();
                }
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
        public IActionResult AccessDenied()
        {
            return RedirectToAction("Login");
        }

        public bool ChangePassword(ChangePassVM changePass)
        {
            if (changePass.userName != null && changePass.oldPass != null && changePass.newPass != null)
            {
                var user = db.UserService.Login(changePass.userName, CommonMethod.Encrypt(changePass.oldPass));

                if (user == null)
                    return false;

                else
                    user.Password = CommonMethod.Encrypt(changePass.newPass);

                db.UserService.UpdateUser(user);
                return true;

            }
            else
                return false;
        }

    }
}