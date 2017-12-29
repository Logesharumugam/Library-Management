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
    
    public partial class CheckInCheckOutDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CheckInCheckOutDetail()
        {
            this.BookLostDetails = new HashSet<BookLostDetail>();
        }
    
        public int BookIssueId { get; set; }
        public int BookRequestId { get; set; }
        public int IssuedBy { get; set; }
        public System.DateTime IssueDate { get; set; }
        public System.DateTime DueDate { get; set; }
        public Nullable<System.DateTime> ReturnedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BookLostDetail> BookLostDetails { get; set; }
        public virtual BookRequestDetail BookRequestDetail { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        public virtual UserDetail UserDetail1 { get; set; }
    }
}