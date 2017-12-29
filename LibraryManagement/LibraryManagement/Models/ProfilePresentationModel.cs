using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Syncfusion.JavaScript;
using LibraryManagementBase.LibraryManagementAccess;

namespace LibraryManagement.Models
{
    public class ProfilePresentationModel
    {
        public List<LibraryDetails> GetUserBookDetails(DataManager dataManager)
        {
            IEnumerable<LibraryDetails> bookList = new DBAccess().GetBookDetails();
            
            return bookList.ToList();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }
}