using LFMS.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using LFMS.Utilities;
using System.Text.RegularExpressions;


namespace LFMS.Models.BLO
{
    public class CustomerBLO
    {
        private CustomerDAO customerDAO;

        public CustomerBLO()
        {
            customerDAO = new CustomerDAO();
        }


        public Object GetPagingCustomerJson(int displayNum, int orderKey, string orderType, string code, int pageNum)
        {
            var cusList = customerDAO.GetPagingCustomerJson(displayNum, orderKey, orderType, code, pageNum);



            var totalRecord = cusList.GetType().GetProperty("total").GetValue(cusList, null);
            var cusPage = cusList.GetType().GetProperty("cusPage").GetValue(cusList, null);
            var level1 = new List<string>();
            level1.Add("CustomerGroup");

            var list = new List<object>();
            foreach (var cus in (IEnumerable<object>)cusPage)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(cus, level1);
                list.Add(result);
            }
            return new { list, totalRecord };
        }


        public List<Customer> GetCustomersAutoSearch(string cusName)
        {
            return customerDAO.GetCustomersAutoSearch(cusName);
        }

        public Customer GetCustomerByID(int id)
        {
            return customerDAO.GetCustomerByID(id);
        }

        public int AddCustomer(string taxCode, string name, int groupId, string represent, string sex, string birthDay, string identityNum, string identityDate, string identityPlace, string bankAccount, string bankBranch, string address, string mobile, string telephone, string email)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!name.IsNullOrWhiteSpace() && groupId != 0 && !represent.IsNullOrWhiteSpace() && !mobile.IsNullOrWhiteSpace() && !address.IsNullOrWhiteSpace() && regex.IsMatch(mobile)) 
            {
                bool isValid = true;
                if(!identityNum.IsNullOrWhiteSpace() && !regex.IsMatch(identityNum))
                {
                    isValid = false;
                }
                if(!bankAccount.IsNullOrWhiteSpace() && !regex.IsMatch(bankAccount))
                {
                    isValid = false;
                }
                if(!telephone.IsNullOrWhiteSpace() && !regex.IsMatch(telephone))
                {
                    isValid = false;
                }
                if (isValid)
                {
                    int cusId = customerDAO.AddCustomer(taxCode, name, groupId, represent, sex, birthDay, identityNum, identityDate, identityPlace, bankAccount, bankBranch, address, mobile, telephone, email);
                    return cusId;
                }
            }
            return 0;
        }

        public string UpdateCustomer(int id, string taxCode, string name, int groupId, string represent, string sex, string birthDay, string identityNum, string identityDate, string identityPlace, string bankAccount, string bankBranch, string address, string mobile, string telephone, string email)
        {
            Regex regex = new Regex("^[0-9]+$");
            if (!name.IsNullOrWhiteSpace() && groupId != 0 && !represent.IsNullOrWhiteSpace() && !mobile.IsNullOrWhiteSpace() && !address.IsNullOrWhiteSpace() && regex.IsMatch(mobile))
            {
                bool isValid = true;
                if(!identityNum.IsNullOrWhiteSpace() && !regex.IsMatch(identityNum))
                {
                    isValid = false;
                }
                if(!bankAccount.IsNullOrWhiteSpace() && !regex.IsMatch(bankAccount))
                {
                    isValid = false;
                }
                if(!telephone.IsNullOrWhiteSpace() && !regex.IsMatch(telephone))
                {
                    isValid = false;
                }
                if (isValid)
                {
                    bool result = customerDAO.UpdateCustomer(id, taxCode, name, groupId, represent, sex, birthDay, identityNum, identityDate, identityPlace, bankAccount, bankBranch, address, mobile, telephone, email);
                    if (result)
                    {
                        return "success";
                    }
                    return "fail";
                }
            }
            return "fail";
        }

        public string DeleteCustomer(int id)
        {
            bool result = customerDAO.DeleteCustomer(id);
            if (result)
            {
                return "success";
            }
            return "fail";
        }

        public List<CustomerGroup> GetAllCustomerGroup()
        {
            return customerDAO.GetAllCustomerGroup();
        }

        public List<Object> GetAllCustomerGroupJson()
        {
            return customerDAO.GetAllCustomerGroupJson();
        }

        public CustomerGroup GetCustomerGroupByID(int id)
        {
            return customerDAO.GetCustomerGroupByID(id);
        }

        public string AddCustomerGroup(string name, string description)
        {
            if(name != null)
            {
                bool result = customerDAO.AddCustomerGroup(name, description);
                if(result)
                {
                    return "success";
                }
                return "fail";
            }
            return "fail";
        }

        public string UpdateCustomerGroup(int id, string name, string description)
        {
            if (name != null)
            {
                bool result = customerDAO.UpdateCustomerGroup(id, name, description);
                if (result)
                {
                    return "success";
                }
                return "fail";
            }
            return "fail";
        }

        public string DeleteCustomerGroup(int id)
        {
            bool result = customerDAO.DeleteCustomerGroup(id);
            if (result)
            {
                return "success";
            }
            return "fail";
        }
    }
}