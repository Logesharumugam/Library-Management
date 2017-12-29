using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementBase.LibraryEntities;

namespace LibraryManagementBase.LibraryManagementAccess
{
    public class DBAccess
    {

        #region Book Details

        #region Create New books

        /// <summary>
        /// This method is used to add the new book details
        /// </summary>
        /// <param name="librarydetails"></param>
        /// <returns></returns>
        public TransactionResult AddNewBookDetails(LibraryDetails librarydetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    var newbookdetails = new BookDetail
                    {
                        BookName = librarydetails.BookName,
                        CategoryId = librarydetails.CategoryId,
                        ISBNNumber = librarydetails.ISBNNumber,
                        Publisher = librarydetails.Publisher,
                        AuthorName = librarydetails.AuthorName,
                        BookReviewLink = librarydetails.ReviewLink,
                        TotalNoOfCopies = librarydetails.NoOfCopies,
                        Comments = librarydetails.Comments,
                        IsAvailable = true,
                        IsActive = true,
                        Price = librarydetails.Price,
                        CreatedDate = currentDatetime,
                        ModifiedDate = currentDatetime,
                        PurchasedDate = librarydetails.PurchasedDate,
                    };
                    context.BookDetails.Add(newbookdetails);
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }

            }
        }

        #endregion

        #region Edit Books
        /// <summary>
        /// This method is used to edit the book details
        /// </summary>
        /// <param name="librarydetails"></param>
        /// <returns></returns>
        public TransactionResult EditBookDetails(LibraryDetails librarydetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    BookDetail editbookdetails = (from books in context.BookDetails.Where(p => p.BookId == librarydetails.BookId &&
                                              p.IsActive == true)
                                                  select books).FirstOrDefault();
                    if (editbookdetails != null)
                    {
                        if (!string.IsNullOrEmpty(librarydetails.BookName) && librarydetails.BookName != editbookdetails.BookName)
                            editbookdetails.BookName = librarydetails.BookName;
                        else if (librarydetails.CategoryId > 0 && librarydetails.CategoryId != editbookdetails.CategoryId)
                            editbookdetails.CategoryId = librarydetails.CategoryId;
                        else if (!string.IsNullOrEmpty(librarydetails.ISBNNumber) && librarydetails.ISBNNumber != editbookdetails.ISBNNumber)
                            editbookdetails.ISBNNumber = librarydetails.ISBNNumber;
                        else if (!string.IsNullOrEmpty(librarydetails.AuthorName) && librarydetails.AuthorName != editbookdetails.AuthorName)
                            editbookdetails.AuthorName = librarydetails.AuthorName;
                        else if (!string.IsNullOrEmpty(librarydetails.ReviewLink) && librarydetails.ReviewLink != editbookdetails.BookReviewLink)
                            editbookdetails.BookReviewLink = librarydetails.ReviewLink;
                        else if (librarydetails.Price > 0 && librarydetails.Price != editbookdetails.Price)
                            editbookdetails.Price = librarydetails.Price;
                        else if (librarydetails.NoOfCopies > 0 && librarydetails.NoOfCopies != editbookdetails.TotalNoOfCopies)
                            editbookdetails.TotalNoOfCopies = librarydetails.NoOfCopies;
                        editbookdetails.ModifiedDate = currentDatetime;
                    }
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }

            }
        }

        #endregion

        #region Delete books

        public TransactionResult DeleteBookDetails(int bookId)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    BookDetail deletebookdetails = (from books in context.BookDetails.Where(p => p.BookId == bookId &&
                                              p.IsActive == true)
                                                    select books).FirstOrDefault();
                    if (deletebookdetails != null)
                    {
                        deletebookdetails.IsActive = false;
                        deletebookdetails.ModifiedDate = currentDatetime;
                    }
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }

            }
        }

        #endregion

        #region Get Book details
        /// <summary>
        /// This method is used to get the book details
        /// </summary>
        /// <returns></returns>
        public List<LibraryDetails> GetBookDetails()
        {
            List<LibraryDetails> bookdetailsresult = new List<LibraryDetails>();
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    bookdetailsresult = (from books in context.Vw_GetBookDetails
                                         select new LibraryDetails
                                         {
                                             BookId = books.BookId,
                                             BookName = books.BookName,
                                             CategoryName = books.CategoryName,
                                             AuthorName = books.AuthorName,
                                             Publisher = books.Publisher,
                                             ISBNNumber = books.ISBNNumber,
                                             NoOfCopies = books.TotalNoOfCopies,
                                             IssuedCopies = books.IssuedCopy.Value,
                                             RequestedCopies =  books.RequestedCopy.Value,
                                             AvailableCopies = books.AvailableCopy.Value,
                                             ReviewLink = books.BookReviewLink
                                         }).Distinct().ToList<LibraryDetails>();
                    return bookdetailsresult;
                }
                catch (Exception ex)
                {
                    return bookdetailsresult;
                }
            }
        }

        #endregion

        #endregion

        #region Book Proposal details

        #region Send New book Proposal

        /// <summary>
        /// This method is used to add the new book details
        /// </summary>
        /// <param name="librarydetails"></param>
        /// <returns></returns>
        public TransactionResult SendNewBookProposal(LibraryDetails librarydetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    var newbookProposalDetails = new NewProposalDetail
                    {
                        BookName = librarydetails.BookName,
                        Category = librarydetails.CategoryName,
                        Publisher = librarydetails.Publisher,
                        Author = librarydetails.AuthorName,
                        Comments = librarydetails.Comments,
                        ProposalId = 1,
                        ProposedDate = currentDatetime,
                        IsActive = true,
                        ModifiedDate = currentDatetime,
                    };
                    context.NewProposalDetails.Add(newbookProposalDetails);
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }

            }
        }

        #endregion

        #region Edit new book proposal
        /// <summary>
        /// This method is used to edit the new book proposal
        /// </summary>
        /// <param name="librarydetails"></param>
        /// <returns></returns>
        public TransactionResult EditNewBookProposal(LibraryDetails librarydetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    NewProposalDetail editbookdproposaletails = (from books in context.NewProposalDetails.Where(p => p.ProposalId == librarydetails.ProposalId &&
                                              p.IsActive == true)
                                                                 select books).FirstOrDefault();
                    if (editbookdproposaletails != null)
                    {
                        if (!string.IsNullOrEmpty(librarydetails.BookName) && librarydetails.BookName != editbookdproposaletails.BookName)
                            editbookdproposaletails.BookName = librarydetails.BookName;
                        if (!string.IsNullOrEmpty(librarydetails.CategoryName) && librarydetails.CategoryName != editbookdproposaletails.Category)
                            editbookdproposaletails.Category = librarydetails.CategoryName;
                        else if (!string.IsNullOrEmpty(librarydetails.Publisher) && librarydetails.Publisher != editbookdproposaletails.Publisher)
                            editbookdproposaletails.Publisher = librarydetails.Publisher;
                        else if (!string.IsNullOrEmpty(librarydetails.AuthorName) && librarydetails.AuthorName != editbookdproposaletails.Author)
                            editbookdproposaletails.Author = librarydetails.AuthorName;
                        else if (!string.IsNullOrEmpty(librarydetails.Comments) && librarydetails.Comments != editbookdproposaletails.Comments)
                            editbookdproposaletails.Comments = librarydetails.Comments;
                        editbookdproposaletails.ModifiedDate = currentDatetime;
                    }
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }

            }
        }

        #endregion

        #region Delete new book proposal

        public TransactionResult DeleteNewBookProposal(int proposalId)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    NewProposalDetail deleteproposaldetails = (from books in context.NewProposalDetails.Where(p => p.ProposalId == proposalId &&
                                              p.IsActive == true)
                                                               select books).FirstOrDefault();
                    if (deleteproposaldetails != null)
                    {
                        deleteproposaldetails.IsActive = false;
                        deleteproposaldetails.ModifiedDate = currentDatetime;
                    }
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }

            }
        }

        #endregion

        #region Get New Book proposal details
        /// <summary>
        /// This method is used to get the new book proposal details
        /// </summary>
        /// <returns></returns>
        public List<LibraryDetails> GetNewBookProposalDetails(UserloginDetails loginDetails)
        {
            List<LibraryDetails> bookproposalresult = new List<LibraryDetails>();
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    if (loginDetails.PernissionLevel != (int)PermissionLevel.Admin)
                    {
                        bookproposalresult = (from proposaldetails in context.NewProposalDetails
                                              from status in context.Status.Where(p => p.StatusId == proposaldetails.ProposedStatusId && proposaldetails.ProposedBy == loginDetails.UserId && p.IsActive == true)
                                              select new LibraryDetails
                                              {
                                                  BookName = proposaldetails.BookName,
                                                  CategoryName = proposaldetails.Category,
                                                  AuthorName = proposaldetails.Author,
                                                  ProposedDate = proposaldetails.ProposedDate,
                                                  Status = status.StatusName,
                                              }).Distinct().ToList<LibraryDetails>();
                        return bookproposalresult;
                    }
                    else
                    {
                        bookproposalresult = (from proposaldetails in context.NewProposalDetails
                                              from status in context.Status.Where(p => p.StatusId == proposaldetails.ProposedStatusId && p.IsActive == true)
                                              from userdetails in context.UserDetails.Where(p => p.UserId == proposaldetails.ProposedBy && p.IsActive == true).DefaultIfEmpty()
                                              select new LibraryDetails
                                              {
                                                  BookName = proposaldetails.BookName,
                                                  CategoryName = proposaldetails.Category,
                                                  AuthorName = proposaldetails.Author,
                                                  UserName = userdetails.UserName,
                                                  ProposedDate = proposaldetails.ProposedDate,
                                                  Status = status.StatusName,
                                              }).Distinct().ToList<LibraryDetails>();
                        return bookproposalresult;
                    }
                }
                catch (Exception ex)
                {
                    return bookproposalresult;
                }
            }
        }

        #endregion

        #endregion

        #region Book Request details

        #region Send book request details

        /// <summary>
        /// This method is used to add the new book details
        /// </summary>
        /// <param name="librarydetails"></param>
        /// <returns></returns>
        public TransactionResult SendBookRequest(LibraryDetails librarydetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    var newbookrequestdetails = new BookRequestDetail
                    {
                        BookId = librarydetails.BookId,
                        UserId = librarydetails.UserId,
                        StatusId = 1,
                        RequestDate = currentDatetime,
                        ModifiedDate = currentDatetime,
                        Comments = librarydetails.Comments,
                        IsActive = true,
                    };
                    context.BookRequestDetails.Add(newbookrequestdetails);
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }

            }
        }

        #region Edit new book proposal
        /// <summary>
        /// This method is used to edit the new book proposal
        /// </summary>
        /// <param name="librarydetails"></param>
        /// <returns></returns>
        public TransactionResult EditBookRequest(LibraryDetails librarydetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    BookRequestDetail editbookrequestdetails = (from books in context.BookRequestDetails.Where(p => p.RequestId == librarydetails.RequestId &&
                                              p.IsActive == true)
                                                                select books).FirstOrDefault();
                    if (editbookrequestdetails != null)
                    {
                        if (librarydetails.BookId > 0 && librarydetails.BookId != editbookrequestdetails.BookId)
                            editbookrequestdetails.BookId = librarydetails.BookId;
                        else if (!string.IsNullOrEmpty(librarydetails.Comments) && librarydetails.Comments != editbookrequestdetails.Comments)
                            editbookrequestdetails.Comments = librarydetails.Comments;
                        editbookrequestdetails.ModifiedDate = currentDatetime;
                    }
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }

            }
        }

        #region Cancel new book request

        public TransactionResult CancelBookRequest(int requestId)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    BookRequestDetail deletebookrequest = (from books in context.BookRequestDetails.Where(p => p.RequestId == requestId &&
                                              p.IsActive == true)
                                                           select books).FirstOrDefault();
                    if (deletebookrequest != null)
                    {
                        deletebookrequest.IsActive = false;
                        deletebookrequest.ModifiedDate = currentDatetime;
                    }
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }

            }
        }

        #endregion

        #endregion

        #endregion

        #region Get Book request details
        /// <summary>
        /// This method is used to get the book request details
        /// </summary>
        /// <returns></returns>
        public List<LibraryDetails> GetBookRequestDetails(UserloginDetails logindetails)
        {
            List<LibraryDetails> bookdetailsresult = new List<LibraryDetails>();
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    if (logindetails.PernissionLevel == (int)PermissionLevel.Employee)
                    {
                        bookdetailsresult = (from bookrequest in context.BookRequestDetails
                                             from books in context.BookDetails.Where(p => p.BookId == bookrequest.BookId && bookrequest.StatusId == 1 && bookrequest.UserId == logindetails.UserId && p.IsActive == true && bookrequest.IsActive == true)
                                             from category in context.CategoryDetails.Where(p => p.CategoryId == books.CategoryId && p.IsActive == true)
                                             from status in context.Status.Where(p => p.StatusId == bookrequest.StatusId && p.IsActive == true)
                                             from issuedetails in context.CheckInCheckOutDetails.Where(p=>p.BookRequestId == bookrequest.RequestId && p.IsActive == true).DefaultIfEmpty()
                                             select new LibraryDetails
                                             {
                                                 BookId = books.BookId,
                                                 RequestId = bookrequest.RequestId,
                                                 BookName = books.BookName,
                                                 CategoryName = category.CategoryName,
                                                 AuthorName = books.AuthorName,
                                                 Publisher = books.Publisher,
                                                 ISBNNumber = books.ISBNNumber,
                                                 Status = status.StatusName,
                                                 RequestedDate = bookrequest.RequestDate,
                                                 DueDate = issuedetails.DueDate,
                                                 ReturnedDate = issuedetails.ReturnedDate.Value,
                                                 IssuedDate = issuedetails.IssueDate,
                                             }).Distinct().ToList<LibraryDetails>();
                        return bookdetailsresult;
                    }
                    else
                    {
                        bookdetailsresult = (from bookrequest in context.BookRequestDetails
                                             from books in context.BookDetails.Where(p => p.BookId == bookrequest.BookId && p.IsActive == true && bookrequest.IsActive == true)
                                             from category in context.CategoryDetails.Where(p => p.CategoryId == books.CategoryId && p.IsActive == true)
                                             from status in context.Status.Where(p => p.StatusId == bookrequest.StatusId && p.IsActive == true)
                                             from userdetails in context.UserDetails.Where(p => p.UserId == bookrequest.UserId && p.IsActive == true)
                                             select new LibraryDetails
                                             {
                                                 BookId = books.BookId,
                                                 RequestId = bookrequest.RequestId,
                                                 BookName = books.BookName,
                                                 CategoryName = category.CategoryName,
                                                 AuthorName = books.AuthorName,
                                                 Publisher = books.Publisher,
                                                 ISBNNumber = books.ISBNNumber,
                                                 Status = status.StatusName,
                                                 RequestedDate = bookrequest.RequestDate,
                                                 UserName = userdetails.UserName,
                                             }).Distinct().ToList<LibraryDetails>();
                        return bookdetailsresult;
                    }
                }
                catch (Exception ex)
                {
                    return bookdetailsresult;
                }
            }
        }

        #endregion

        #region Renewal book
        /// <summary>
        /// This method is used to renew/extend the book
        /// </summary>
        /// <returns></returns>
        public TransactionResult RenewalOrExtendBook(int requestId,UserloginDetails logindetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    CheckInCheckOutDetail issuedDetails =(from issuedetails in context.CheckInCheckOutDetails.Where(p=>p.BookRequestId == requestId && p.IsActive == true)
                        select issuedetails).FirstOrDefault();

                    if (issuedDetails != null)
                    {
                        issuedDetails.DueDate = currentDatetime;
                        issuedDetails.ModifiedDate = currentDatetime;
                        issuedDetails.ModifiedBy = logindetails.UserId;
                    }
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }
            }
        }

        #endregion

        #region Cancel book
        /// <summary>
        /// This method is used to cancel the book request
        /// </summary>
        /// <returns></returns>
        public TransactionResult CancelBookRequest(int requestId, UserloginDetails logindetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    BookRequestDetail requestdetails = (from request in context.BookRequestDetails.Where(p => p.RequestId == requestId && p.IsActive == true && p.StatusId == 1)
                                                           select request).FirstOrDefault();

                    if (requestdetails != null)
                    {
                        requestdetails.IsActive = false;
                        requestdetails.ModifiedDate = currentDatetime;
                        requestdetails.UserId = logindetails.UserId;
                    }
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }
            }
        }

        #endregion

        #endregion

        #region Book Issued & get issued details

        #region Book Issued details

        public TransactionResult IssuedBook(LibraryDetails libraryDetails, UserloginDetails logindetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    BookRequestDetail requestDetails = (from request in context.BookRequestDetails.Where(p => p.RequestId == libraryDetails.RequestId && p.IsActive == true && p.StatusId == 1)
                                                        select request).FirstOrDefault();

                    if (requestDetails != null)
                    {
                        requestDetails.StatusId =2;
                        requestDetails.ModifiedDate = currentDatetime;
                        requestDetails.UserId = logindetails.UserId;

                        var issueddetails = new CheckInCheckOutDetail
                        {
                             BookRequestId = requestDetails.RequestId,
                             IssuedBy = logindetails.UserId,
                             IssueDate = currentDatetime,
                             DueDate = libraryDetails.DueDate,
                             ReturnedDate = libraryDetails.ReturnedDate,
                             ModifiedBy = logindetails.UserId,
                             Comments = libraryDetails.Comments,
                             IsActive =  true
                        };
                        context.CheckInCheckOutDetails.Add(issueddetails);
                    }
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }
            }
        }


        #endregion

        #region Get book issued details

        public List<LibraryDetails> GetBookIssueDetails(UserloginDetails logindetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                List<LibraryDetails> bookIssuedetailsResult = new List<LibraryDetails>();
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    bookIssuedetailsResult = (from issuedetails in context.CheckInCheckOutDetails
                        from request in
                        context.BookRequestDetails.Where(
                            p => p.RequestId == issuedetails.BookRequestId && p.StatusId == 2 &&
                                p.IsActive == true)
                        from books in context.BookDetails.Where(p => p.BookId == request.BookId && p.IsActive == true)
                        from category in context.CategoryDetails.Where(p=>p.CategoryId == books.CategoryId && p.IsActive)
                        from status in context.Status.Where(p=>p.StatusId == request.StatusId && p.IsActive)
                        from userdetails in context.UserDetails.Where(p=>p.UserId == request.UserId && p.IsActive)
                        select new LibraryDetails
                        {
                            BookId = books.BookId,
                            BookName = books.BookName,
                            CategoryName = category.CategoryName,
                            IssuedDate = issuedetails.IssueDate,
                            DueDate = issuedetails.DueDate,
                            ReturnedDate = issuedetails.ReturnedDate,
                            Status = status.StatusName,
                            UserName = userdetails.UserName
                        }).Distinct().ToList<LibraryDetails>();


                    return bookIssuedetailsResult;
                }
                catch (Exception ex)
                {
                    return bookIssuedetailsResult;
                }
            }
        }

        #endregion

        #endregion

        #region Book Category details


        #region AddCategoryDetails
        ///<summary>
        ///This method is used for CategoryDetails
        ///</summary>
        ///<param name="librarydetails"></param>
        ///<returns></returns>
        ///
        public TransactionResult AddCategoryDetails(LibraryDetails librarydetails)
        {
            using (var context = new LibraryManagementEntities())
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    var addcategorydetail = new CategoryDetail
                    {
                        CategoryId = librarydetails.CategoryId,
                        CategoryName = librarydetails.CategoryName,
                        IsActive = true,
                        ModifiedDate = currentDatetime,
                        CreatedBy = librarydetails.CreatedBy
                    };
                    context.CategoryDetails.Add(addcategorydetail);
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }
        }
        #endregion

        #region EditCategoryDetails
        public TransactionResult EditCategoryDetails(LibraryDetails librarydetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    CategoryDetail editcategorydetails = (from category in context.CategoryDetails.Where(q => q.CategoryId == librarydetails.CategoryId && q.IsActive == true) select category).FirstOrDefault();
                    if (editcategorydetails != null)
                    {
                        editcategorydetails.CategoryName = librarydetails.CategoryName;
                        editcategorydetails.ModifiedDate = currentDatetime;
                    }
                    context.SaveChanges();
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }

            }
        }
        #endregion

        #region RemoveCategoryDetails
        public TransactionResult RemoveCategoryDetails(LibraryDetails librarydetails)
        {
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    DateTime currentDatetime = GetServerTime();
                    CategoryDetail removecategorydetails = (from category in context.CategoryDetails.Where(q => q.CategoryId == librarydetails.CategoryId && q.IsActive == true) select category).FirstOrDefault();
                    if (removecategorydetails != null)
                        context.CategoryDetails.Remove(removecategorydetails);
                    return new TransactionResult() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    return new TransactionResult() { IsSuccess = false };
                }

            }
        }
        #endregion

        #region Get category details

        /// <summary>
        /// This method is used to get the category details
        /// </summary>
        /// <returns></returns>
        public List<LibraryDetails> GetCategoryDetails()
        {
            List<LibraryDetails> categorydetailsresult = new List<LibraryDetails>();
            using (var context = new LibraryManagementEntities())
                try
                {
                    categorydetailsresult = (from contextVwGetBookDetail in context.Vw_GetBookDetails
                        group contextVwGetBookDetail by contextVwGetBookDetail.CategoryName
                        into categorydetails
                        select new LibraryDetails
                        {
                            CategoryName = categorydetails.Key,
                            NoOfCopies   = categorydetails.Sum(p=>p.TotalNoOfCopies),                          
                        }).Distinct().ToList<LibraryDetails>();
                    var i = 0;
                    foreach (var item in categorydetailsresult)
                    {
                        i++;
                        item.SerialNo = i;
                    }

                    return categorydetailsresult;
                }
                catch (Exception ex)
                {
                    return categorydetailsresult;
                }
        }


        /// <summary>
        /// This method is used to get the book categories
        /// </summary>
        /// <returns></returns>
        public List<LibraryDetails> GetBookCategories()
        {
            List<LibraryDetails> categorydetailsresult = new List<LibraryDetails>();
            using (var context = new LibraryManagementEntities())
                try
                {
                    categorydetailsresult = (from contextVwGetBookDetail in context.CategoryDetails.Where(p=>p.IsActive)
                                             select new LibraryDetails
                                             {
                                                 CategoryId = contextVwGetBookDetail.CategoryId,
                                                 CategoryName = contextVwGetBookDetail.CategoryName
                                             }).Distinct().ToList<LibraryDetails>();
                    return categorydetailsresult;
                }
                catch (Exception ex)
                {
                    return categorydetailsresult;
                }
        }

        #endregion

        #endregion

        #region Book lost details

        #region Get Book lost details
        /// <summary>
        /// This method is used to get the book request details
        /// </summary>
        /// <returns></returns>
        public List<LibraryDetails> GetBookLostDetails(UserloginDetails logindetails)
        {
            List<LibraryDetails> bookdetailsresult = new List<LibraryDetails>();
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    if (logindetails.PernissionLevel == (int)PermissionLevel.Employee)
                    {
                        bookdetailsresult = (from lostbook in context.BookLostDetails
                                             from issueddetails in context.CheckInCheckOutDetails.Where(p => p.BookIssueId == lostbook.BookIssueId
                                             && lostbook.LostBy == logindetails.UserId && lostbook.IsActive == true && lostbook.LostBy == logindetails.UserId && p.IsActive == true)
                                             from bookrequest in context.BookRequestDetails.Where(p => p.RequestId == issueddetails.BookRequestId && p.IsActive == true)
                                             from books in context.BookDetails.Where(p => p.BookId == bookrequest.BookId && p.IsActive == true)
                                             select new LibraryDetails
                                             {
                                                 BookId = books.BookId,
                                                 BookName = books.BookName,
                                                 FineAmount = lostbook.FineAmount,
                                                 IssuedDate = issueddetails.IssueDate,
                                             }).Distinct().ToList<LibraryDetails>();
                        return bookdetailsresult;
                    }
                    else
                    {
                        bookdetailsresult = (from lostbook in context.BookLostDetails
                                             from issueddetails in context.CheckInCheckOutDetails.Where(p => p.BookIssueId == lostbook.BookIssueId
                                             && lostbook.IsActive == true && p.IsActive == true)
                                             from bookrequest in context.BookRequestDetails.Where(p => p.RequestId == issueddetails.BookRequestId && p.IsActive == true)
                                             from books in context.BookDetails.Where(p => p.BookId == bookrequest.BookId && p.IsActive == true)
                                             from userdetails in context.UserDetails.Where(p => p.UserId == lostbook.LostBy && p.IsActive == true)
                                             select new LibraryDetails
                                             {
                                                 BookId = books.BookId,
                                                 BookName = books.BookName,
                                                 FineAmount = lostbook.FineAmount,
                                                 UserName = userdetails.UserName,
                                                 IssuedDate = issueddetails.IssueDate,
                                             }).Distinct().ToList<LibraryDetails>();
                        return bookdetailsresult;
                    }
                }
                catch (Exception ex)
                {
                    return bookdetailsresult;
                }
            }
        }

        #endregion

        #endregion

        #region Current server time

        /// <summary>
        /// This method is used to get the current time
        /// </summary>
        /// <returns></returns>
        public DateTime GetServerTime()
        {
            DateTime currentTime = new DateTime();
            using (var context = new LibraryManagementEntities())
            {
                currentTime = context.Database.SqlQuery<DateTime>("SELECT getdate()").AsQueryable().First();

            }
            return currentTime;
        }

        #endregion

        #region Get Login details
        /// <summary>
        /// This method is used to get login details
        /// </summary>
        /// <returns></returns>
        public UserloginDetails GetLoginDetails(string emailId,string password)
        {
            UserloginDetails logindetails = new UserloginDetails();
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                  logindetails = (from userdetails in context.UserDetails
                                              from role in context.Roles.Where(p => p.RoleId == userdetails.RoleId && userdetails.UserMail == emailId && userdetails.Password == password && p.IsActive == true && userdetails.IsActive == true)
                                              select new UserloginDetails
                                              {
                                                  UserId = userdetails.UserId,
                                                  UserName = userdetails.UserName,
                                                  Password = userdetails.Password,
                                                  UserEmail =  userdetails.UserMail,
                                                  PernissionLevel = role.RoleId,
                                                  Role = role.Role1
                                              }).Distinct().FirstOrDefault();
                        return logindetails;
                }
                catch (Exception ex)
                {
                    return logindetails;
                }
            }
        }

        #endregion

        #region Get User details
        /// <summary>
        /// This method is used to get login details
        /// </summary>
        /// <returns></returns>
        public UserloginDetails GetUserDetails(string emailId)
        {
            UserloginDetails logindetails = new UserloginDetails();
            using (var context = new LibraryManagementEntities())
            {
                try
                {
                    logindetails = (from userdetails in context.UserDetails
                                    from role in context.Roles.Where(p => p.RoleId == userdetails.RoleId && userdetails.UserMail == emailId && userdetails.IsActive == true)
                                    select new UserloginDetails
                                    {
                                        UserId = userdetails.UserId,
                                        UserName = userdetails.UserName,
                                        Password = userdetails.Password,
                                        UserEmail = userdetails.UserMail,
                                        PernissionLevel = role.RoleId,
                                        Role = role.Role1
                                    }).Distinct().FirstOrDefault();
                    return logindetails;
                }
                catch (Exception ex)
                {
                    return logindetails;
                }
            }
        }

        #endregion
    }
}

