//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryManagementBase.LibraryEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookLostDetail
    {
        public int BookLostId { get; set; }
        public int BookIssueId { get; set; }
        public decimal FineAmount { get; set; }
        public int LostBy { get; set; }
        public string Comments { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    
        public virtual CheckInCheckOutDetail CheckInCheckOutDetail { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual UserDetail UserDetail1 { get; set; }
    }
}
