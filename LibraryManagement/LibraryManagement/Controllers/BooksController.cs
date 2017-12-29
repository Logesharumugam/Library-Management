using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;
using LibraryManagementBase.LibraryManagementAccess;

namespace LibraryManagement.Controllers
{
    public class BooksController : Controller
    {
        DBAccess _dbAccess = new DBAccess();
        // GET: Books
        [HttpGet]
        public ActionResult IssuedDetails()
        {
            var userEmail = (string)Session["UserEmail"];
            var loginDetails = new DBAccess().GetUserDetails(userEmail);
            ViewBag.UserEmail = loginDetails.UserEmail;
            ViewBag.UserName = loginDetails.UserName;
            ViewBag.UserId = loginDetails.UserId;
            ViewBag.Role = loginDetails.Role;
            var details = new UserloginDetails();
            loginDetails.PernissionLevel = loginDetails.Role == "Admin" ? 1 : 2;
            var result = _dbAccess.GetBookIssueDetails(loginDetails);
            ViewBag.BookDetails = result;
            return View("~/Views/Books/_issuedBookDetails.cshtml");
        }

        [HttpGet]
        public ActionResult NewProposalsDetails()
        {
            var userEmail = (string)Session["UserEmail"];
            var loginDetails = new DBAccess().GetUserDetails(userEmail);
            ViewBag.UserEmail = loginDetails.UserEmail;
            ViewBag.UserName = loginDetails.UserName;
            ViewBag.UserId = loginDetails.UserId;
            ViewBag.Role = loginDetails.Role;
            var details = new UserloginDetails();
            loginDetails.PernissionLevel = loginDetails.Role == "Admin" ? 1 : 2;
            var result = _dbAccess.GetNewBookProposalDetails(loginDetails);
            ViewBag.BookDetails = result;
            return View("/Views/Books/_newProposalBookDetails.cshtml");
        }

        [HttpGet]
        public ActionResult LostDetails()
        {
            var userEmail = (string)Session["UserEmail"];
            var loginDetails = new DBAccess().GetUserDetails(userEmail);
            ViewBag.UserEmail = loginDetails.UserEmail;
            ViewBag.UserName = loginDetails.UserName;
            ViewBag.UserId = loginDetails.UserId;
            ViewBag.Role = loginDetails.Role;
            var details = new UserloginDetails();
            loginDetails.PernissionLevel = loginDetails.Role == "Admin" ? 1 : 2;
            var result = _dbAccess.GetBookLostDetails(loginDetails);
            ViewBag.BookDetails = result;
            return View("/Views/Books/_lostBookDetails.cshtml");
        }

        [HttpGet]
        public ActionResult RequestDetails()
        {
            var userEmail = (string)Session["UserEmail"];
            var loginDetails = new DBAccess().GetUserDetails(userEmail);
            ViewBag.UserEmail = loginDetails.UserEmail;
            ViewBag.UserName = loginDetails.UserName;
            ViewBag.UserId = loginDetails.UserId;
            ViewBag.Role = loginDetails.Role;
            var details = new UserloginDetails();
            loginDetails.PernissionLevel = loginDetails.Role == "Admin" ? 1 : 2;
            var result = _dbAccess.GetBookRequestDetails(loginDetails);
            ViewBag.BookDetails = result;
            return View("/Views/Books/_requestBookDetails.cshtml");
        }

        public ActionResult GetBookDetails()
        {
            var userEmail = (string)Session["UserEmail"];
            var loginDetails = new DBAccess().GetUserDetails(userEmail);
            ViewBag.UserEmail = loginDetails.UserEmail;
            ViewBag.UserName = loginDetails.UserName;
            ViewBag.UserId = loginDetails.UserId;
            ViewBag.Role = loginDetails.Role;
            var modal = new AddBook();
            var category = new List<SelectListItem>();

            var details = new UserloginDetails();
            loginDetails.PernissionLevel = loginDetails.Role == "Admin" ? 1 : 2;

            category.Add(new SelectListItem()
            {
                Text = "Data Structures",
                Value = "1"
            });

            //var result = new DBAccess().Cate(needDetails);
            ViewBag.Category = category;
            return View("~/Views/Common/_addNewBook.cshtml", modal);
        }

        public JsonResult Update(AddBook details)
        {
            var userEmail = (string)Session["UserEmail"];
            var loginDetails = new DBAccess().GetUserDetails(userEmail);
            ViewBag.UserEmail = loginDetails.UserEmail;
            ViewBag.UserName = loginDetails.UserName;
            ViewBag.UserId = loginDetails.UserId;
            ViewBag.Role = loginDetails.Role;
            var needDetails = new LibraryDetails();
            needDetails.BookName = details.BookName;
            needDetails.AuthorName = details.Author;
            needDetails.AvailableCopies = details.NumberOfCopies;
            needDetails.BookIssueId = 1;
            needDetails.CategoryId = 108;
            needDetails.CategoryName = "Technical";
            needDetails.Comments = details.Comments;
            needDetails.PurchasedDate = DateTime.Now;
            needDetails.ISBNNumber = details.IsbnNo.ToString();
            needDetails.Publisher = details.Publisher;
            needDetails.IssuedCopies = details.NumberOfCopies;
            needDetails.Price = 500;
            needDetails.ReviewLink = details.BookReviewLink;

            var result = new DBAccess().AddNewBookDetails(needDetails);
            return Json(new {status = result.IsSuccess});

        }

        [HttpGet]
        public ActionResult CategoryDetails()
        {
            var userEmail = (string)Session["UserEmail"];
            var loginDetails = new DBAccess().GetUserDetails(userEmail);
            ViewBag.UserEmail = loginDetails.UserEmail;
            ViewBag.UserName = loginDetails.UserName;
            ViewBag.UserId = loginDetails.UserId;
            ViewBag.Role = loginDetails.Role;
            var details = new UserloginDetails();
            var result = _dbAccess.GetCategoryDetails();
            ViewBag.BookDetails = result;
            return View("/Views/Books/_categoryBookDetail.cshtml");
        }

        public JsonResult SendBookRequest(int bookId, int userId, string comments)
        {
            var details = new LibraryDetails();
            details.BookId = bookId;
            details.UserId = userId;
            details.Comments = comments;
            TransactionResult isSuccess = new DBAccess().SendBookRequest(details);
            return this.Json(new { result = isSuccess.IsSuccess }, JsonRequestBehavior.AllowGet);
        }
    }
}
