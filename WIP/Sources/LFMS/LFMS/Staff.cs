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
    
    public partial class Staff
    {
        public Staff()
        {
            this.Accounts = new HashSet<Account>();
            this.CalendarEvents = new HashSet<CalendarEvent>();
            this.Case_Staff = new HashSet<Case_Staff>();
            this.Impresses = new HashSet<Impress>();
            this.Office_Staff = new HashSet<Office_Staff>();
            this.Rewards = new HashSet<Reward>();
            this.Salaries = new HashSet<Salary>();
            this.TimeSheets = new HashSet<TimeSheet>();
        }
    
        public int StaffId { get; set; }
        public int RoleId { get; set; }
        public int StaffGroupId { get; set; }
        public string StaffName { get; set; }
        public string Position { get; set; }
        public string Avatar { get; set; }
        public string Sex { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string IdentityNumber { get; set; }
        public System.DateTime IdentityDate { get; set; }
        public string IdentityPlace { get; set; }
        public string TaxCode { get; set; }
        public string BankAccount { get; set; }
        public string BankBranch { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public Nullable<int> AppendantPeople { get; set; }
    
        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<CalendarEvent> CalendarEvents { get; set; }
        public virtual ICollection<Case_Staff> Case_Staff { get; set; }
        public virtual ICollection<Impress> Impresses { get; set; }
        public virtual ICollection<Office_Staff> Office_Staff { get; set; }
        public virtual ICollection<Reward> Rewards { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
        public virtual StaffGroup StaffGroup { get; set; }
        public virtual ICollection<TimeSheet> TimeSheets { get; set; }
    }
}