using LibraryManagement.Models;
using LibraryManagementBase.LibraryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LibraryManagement.Models
{
    public class AuthenticationController : Controller
    {

        public static string UserEmail { get; set; }
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string EmailId, string Password)
        {
            try
            {
                var loginModel = new LibraryManagementBase.LibraryManagementAccess.DBAccess().GetLoginDetails(EmailId, Password);
                if (loginModel != null)
                {
                    FormsAuthentication.SetAuthCookie(loginModel.UserEmail, true);
                    Session["UserEmail"] = loginModel.UserEmail;
                    return Redirect("/profile/userbooklistcollection");
                }
                else
                {
                    ModelState.AddModelError("Login", "Invalid Email ID or Password");
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect("/login");
        }

    }
}