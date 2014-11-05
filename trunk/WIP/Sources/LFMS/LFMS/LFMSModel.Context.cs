﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LFMSEntities : DbContext
    {
        public LFMSEntities()
            : base("name=LFMSEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Assurance> Assurances { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<CalendarEvent> CalendarEvents { get; set; }
        public DbSet<Case_Customer> Case_Customer { get; set; }
        public DbSet<Case_Staff> Case_Staff { get; set; }
        public DbSet<Case> Cases { get; set; }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Impress> Impresses { get; set; }
        public DbSet<Office_Staff> Office_Staff { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<OperationalEvent> OperationalEvents { get; set; }
        public DbSet<OtherCost> OtherCosts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Reward> Rewards { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<SalaryAssuranceDetail> SalaryAssuranceDetails { get; set; }
        public DbSet<SalaryBenefitDetail> SalaryBenefitDetails { get; set; }
        public DbSet<SalaryTaxDetail> SalaryTaxDetails { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<StaffGroup> StaffGroups { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<UsedService> UsedServices { get; set; }
    }
}
