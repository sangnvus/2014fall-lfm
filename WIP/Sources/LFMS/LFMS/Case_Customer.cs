//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LFMS
{
    using System;
    using System.Collections.Generic;
    
    public partial class Case_Customer
    {
        public int CaseCustomerId { get; set; }
        public int CaseId { get; set; }
        public int CustomerId { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Case Case { get; set; }
    }
}
