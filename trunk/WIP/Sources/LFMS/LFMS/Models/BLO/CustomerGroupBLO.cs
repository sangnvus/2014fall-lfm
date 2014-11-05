using LFMS.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFMS.Models.BLO
{
    public class CustomerGroupBLO
    {
        private CustomerGroupDAO customerGroupDAO;

        public CustomerGroupBLO()
        {
            customerGroupDAO = new CustomerGroupDAO();
        }

        public List<CustomerGroup> GetAllCustomerGroup()
        {
            return customerGroupDAO.GetAllCustomerGroup();
        }

        public CustomerGroup GetCustomerGroupByID(int id)
        {
            return customerGroupDAO.GetCustomerGroupByID(id);
        }

        public bool AddCustomerGroup(string name, string description)
        {
            return customerGroupDAO.AddCustomerGroup(name, description);
        }

        public bool UpdateCustomerGroup(int id, string name, string description)
        {
            return customerGroupDAO.UpdateCustomerGroup(id, name, description);
        }

        public bool DeleteCustomerGroup(int id)
        {
            return customerGroupDAO.DeleteCustomerGroup(id);
        }
    }
}