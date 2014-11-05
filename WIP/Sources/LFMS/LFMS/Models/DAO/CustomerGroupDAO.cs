using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFMS.Models.DAO
{
    public class CustomerGroupDAO
    {
        LFMSEntities db;
        public CustomerGroupDAO()
        {
            db = new LFMSEntities();
        }

        public List<CustomerGroup> GetAllCustomerGroup()
        {
            var customerGroupList = db.CustomerGroups.ToList();
            return customerGroupList;
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
            if (customerGroup == null)
            {
                return false;
            }
            else
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
        }

        public bool DeleteCustomerGroup(int id)
        {
            CustomerGroup customerGroup = GetCustomerGroupByID(id);
            if (customerGroup == null)
            {
                return false;
            }
            else
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
        }
    }
}