using LFMS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;

namespace LFMS.Models.DAO
{
    public class CustomerDAO
    {
        LFMSEntities db;
        public CustomerDAO()
        {
            db = new LFMSEntities();
        }


        public Object GetPagingCustomerJson(int displayNum, int orderKey, string orderType, string code, int pageNum)
        {
            try
            {
                var cusList = new List<Customer>();
                
                if (orderKey == 0)
                    cusList = "asc".Equals(orderType) ? db.Customers.Where(cs => cs.Active == true).OrderBy(cs => cs.CustomerName).ToList() : db.Customers.Where(cs => cs.Active == true).OrderByDescending(cs => cs.CustomerName).ToList();
                else if (orderKey == 1)
                    cusList = "asc".Equals(orderType) ? db.Customers.Where(cs => cs.Active == true).OrderBy(cs => cs.Representative).ToList() : db.Customers.Where(cs => cs.Active == true).OrderByDescending(cs => cs.Representative).ToList();
                else if (orderKey == 2)
                    cusList = "asc".Equals(orderType) ? db.Customers.Where(cs => cs.Active == true).OrderBy(cs => cs.Address).ToList() : db.Customers.Where(cs => cs.Active == true).OrderByDescending(cs => cs.Address).ToList();
                else if (orderKey == 3)
                    cusList = "asc".Equals(orderType) ? db.Customers.Where(cs => cs.Active == true).OrderBy(cs => cs.Mobile).ToList() : db.Customers.Where(cs => cs.Active == true).OrderByDescending(cs => cs.Mobile).ToList();
               
                if (code != "")
                {
                    cusList = cusList.Where(c => c.CustomerName.ToLower().Contains(code.ToLower()) || c.Representative.ToLower().Contains(code.ToLower())
                       || c.Address.ToLower().Contains(code.ToLower()) || c.Mobile.ToLower().Contains(code.ToLower())).ToList();
                }

                var total = cusList.Count;
                var cusPage = cusList.Skip(pageNum * displayNum).Take(displayNum).ToList();

                return new { cusPage, total };

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
      
        }

        public List<Customer> GetCustomersAutoSearch(string cusName)
        {
            var customer = db.Customers.Where(c => c.CustomerName.ToLower().Contains(cusName.ToLower())).ToList();
            return customer;
        }

        public Customer GetCustomerByID(int id)
        {
            var customer = db.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
            return customer;
        }

        public int AddCustomer(string taxCode, string name, int groupId, string represent, string sex, string birthDay, string identityNum, string identityDate, string identityPlace, string bankAccount, string bankBranch, string address, string mobile, string telephone, string email)
        {
            Customer customer = new Customer();
            customer.TaxCode = taxCode;
            customer.CustomerName = name;
            customer.Representative = represent;
            customer.CustomerGroupId = groupId;
            customer.Sex = sex;
            if (!birthDay.IsNullOrWhiteSpace())
            {
                DateTime birthDayDt = DateTime.ParseExact(birthDay, "dd/MM/yyyy", null);
                customer.DateOfBirth = birthDayDt;
            }
            customer.IdentityNumber = identityNum;
            if (!identityDate.IsNullOrWhiteSpace())
            {
                DateTime identityDateDt = DateTime.ParseExact(identityDate, "dd/MM/yyyy", null);
                customer.IdentityDate = identityDateDt;
            }
            customer.IdentityPlace = identityPlace;
            customer.BankAccount = bankAccount;
            customer.BankBranch = bankBranch;
            customer.Address = address;
            customer.Mobile = mobile;
            customer.Telephone = telephone;
            customer.Email = email;
            customer.Active = true;
            try
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return customer.CustomerId;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }

        public bool UpdateCustomer(int id, string taxCode, string name, int groupId, string represent, string sex, string birthDay, string identityNum, string identityDate, string identityPlace, string bankAccount, string bankBranch, string address, string mobile, string telephone, string email)
        {
            Customer customer = GetCustomerByID(id);
            if (customer!=null)
            {
                try
                {
                    customer.TaxCode = taxCode;
                    customer.CustomerName = name;
                    customer.Representative = represent;
                    customer.CustomerGroupId = groupId;
                    customer.Sex = sex;
                    if (!birthDay.IsNullOrWhiteSpace())
                    {
                        DateTime birthDayDt = DateTime.ParseExact(birthDay, "dd/MM/yyyy", null);
                        customer.DateOfBirth = birthDayDt;
                    }
                    customer.IdentityNumber = identityNum;
                    if (!identityDate.IsNullOrWhiteSpace())
                    {
                        DateTime identityDateDt = DateTime.ParseExact(identityDate, "dd/MM/yyyy", null);
                        customer.IdentityDate = identityDateDt;
                    }
                    customer.IdentityPlace = identityPlace;
                    customer.BankAccount = bankAccount;
                    customer.BankBranch = bankBranch;
                    customer.Address = address;
                    customer.Mobile = mobile;
                    customer.Telephone = telephone;
                    customer.Email = email;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            return false;
        }

        public bool DeleteCustomer(int id)
        {
            Customer customer = GetCustomerByID(id);
            if (customer != null)
            {
                try
                {
                    customer.Active = false;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            return false;
        }

        public List<CustomerGroup> GetAllCustomerGroup()
        {
            var customerGroupList = db.CustomerGroups.ToList();
            return customerGroupList;
        }

        public List<Object> GetAllCustomerGroupJson()
        {
            var customerGroup = GetAllCustomerGroup();
            var level1 = new List<string>();
            level1.Add("Customer");

            var list = new List<object>();
            foreach (var cus in customerGroup)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1);
                list.Add(result);
            }
            return list;
        }

        public CustomerGroup GetCustomerGroupByID(int id)
        {
            var customerGroup = db.CustomerGroups.Where(c => c.CustomerGroupId == id).FirstOrDefault();
            return customerGroup;
        }

        public bool AddCustomerGroup(string name, string description)
        {
            CustomerGroup customerGroup = new CustomerGroup();
            customerGroup.CustomerGroupName = name;
            customerGroup.Description = description;

            try
            {
                db.CustomerGroups.Add(customerGroup);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateCustomerGroup(int id, string name, string description)
        {
            CustomerGroup customerGroup = GetCustomerGroupByID(id);
            if (customerGroup != null)
            {
                try
                {
                    customerGroup.CustomerGroupName = name;
                    customerGroup.Description = description;

                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            return false;
        }

        public bool DeleteCustomerGroup(int id)
        {
            CustomerGroup customerGroup = GetCustomerGroupByID(id);
            if (customerGroup != null)
            {
                try
                {
                    db.CustomerGroups.Remove(customerGroup);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
            }
            return false;
        }
    }
}