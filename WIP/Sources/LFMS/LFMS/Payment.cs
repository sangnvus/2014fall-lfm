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
    
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int CaseId { get; set; }
        public string Description { get; set; }
        public System.DateTime PaymentTime { get; set; }
        public int PaymentMoney { get; set; }
    
        public virtual Case Case { get; set; }
    }
}
