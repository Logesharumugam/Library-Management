using Syncfusion.JavaScript;
using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;
using LibraryManagementBase.LibraryManagementAccess;

namespace LibraryManagement.Controllers
{
    public class ProfileController : Controller
    {
        DBAccess _dbAccess = new DBAccess();
        public class ProfileDetails
        {
            public string BookName { get; set; }

            public string UserName { get; set; }

            public string StatusName { get; set; }

            public string Comments { get; set; }

            public int FineAmount { get; set; }

            public string Category { get; set; }

            public string Author { get; set; }
        }

        // GET: Profile
        public ActionResult Index()
        {
            var userEmail = (string)Session["UserEmail"];
            var loginDetails = new DBAccess().GetUserDetails(userEmail);
            ViewBag.UserEmail = loginDetails.UserEmail;
            ViewBag.UserName = loginDetails.UserName;
            ViewBag.UserId = loginDetails.UserId;
            ViewBag.Role = loginDetails.Role;
            loginDetails.PernissionLevel = loginDetails.Role == "Admin" ? 1 : 2;
            var details = new UserloginDetails();
            var proposalDetails = _dbAccess.GetNewBookProposalDetails(loginDetails);
            var lostDetails = _dbAccess.GetBookLostDetails(loginDetails);
            var requestDetails = _dbAccess.GetBookRequestDetails(loginDetails);
            ViewBag.Booklostdetails = lostDetails;
            ViewBag.ProposalBookdetails = proposalDetails;
            ViewBag.BookRequestList = requestDetails;
            return View("~/Views/Profile.cshtml");
        }

        public ActionResult UserBookListCollection(DataManager dataManager)
        {
            var userEmail = (string)Session["UserEmail"];
            var loginDetails = new DBAccess().GetUserDetails(userEmail);
            ViewBag.UserEmail = loginDetails.UserEmail;
            ViewBag.UserName = loginDetails.UserName;
            ViewBag.UserId = loginDetails.UserId;
            ViewBag.Role = loginDetails.Role;
            ViewBag.BookList = new ProfilePresentationModel().GetUserBookDetails(dataManager);
            return View("/Views/Profile/UserBookList.cshtml");
        }
    }
}