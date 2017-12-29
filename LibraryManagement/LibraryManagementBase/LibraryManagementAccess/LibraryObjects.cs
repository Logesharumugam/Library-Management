using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementBase.LibraryManagementAccess
{
    public class LibraryDetails
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string ISBNNumber { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int ProposalId { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public string ReviewLink { get; set; }
        public int NoOfCopies { get; set; }
        public int AvailableCopies { get; set; }
        public int RequestedCopies { get; set; }
        public int IssuedCopies { get; set; }
        public bool IsAvailable { get; set; }
        public string Comments { get; set; }
        public DateTime PurchasedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ProposedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Publisher { get; set; }
        public decimal FineAmount { get; set; }
        public int BookIssueId { get; set; }
        public int LostBy { get; set; }
        public int RequestedBy { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public int RequestId { get; set; }
        public int CreatedBy { get; set; }

        public int SerialNo { get; set; }
    }

    public enum PermissionLevel
    {
        Admin = 1,
        Employee
    }
    public class UserloginDetails
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserEmail { get; set; }
        public int PernissionLevel { get; set; }
        public string Role { get; set; }
    }

    public class TransactionResult
    {
        public bool IsSuccess { get; set; }
        public string ExceptionMessage { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }
    }
}
