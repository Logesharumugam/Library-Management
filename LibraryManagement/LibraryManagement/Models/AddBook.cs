using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class AddBook
    {
        [Display(Name = "Book Name")]
        public string BookName { get; set; }

        [Display(Name = "Category")]
        public string Cagegory { get; set; }

        [Display(Name = "ISBN No")]
        public int  IsbnNo { get; set; }

        [Display(Name = "Publisher")]
        public string Publisher { get; set; }

        public string Author { get; set; }

        [Display(Name = "Book Review Link")]
        public string BookReviewLink { get; set; }

        [Display(Name = "Total No Of Copies")]
        public int NumberOfCopies { get; set; }
        public string Comments { get; set; }



    }
}