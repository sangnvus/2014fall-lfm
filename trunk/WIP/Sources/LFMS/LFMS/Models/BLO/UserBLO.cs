using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using LFMS.Models.DAO;
using LFMS.Utilities;

namespace LFMS.Models.BLO
{
    public class UserBLO
    {
        private UserDAO userDAO;
        public UserBLO()
        {
            userDAO = new UserDAO();
        }

        public Account GetUser(string username, string password)
        {
            var acc = userDAO.GetUser(username, password);
            if (acc != null)
            {
                return acc;
            }
            return null;
        }

        public List<String> GetAuthorize(int staffId)
        {
            var role = userDAO.GetRole(staffId);
            if (role == 1)
            {
                return new List<string> {"All"};
            }
            var listAuthorize = userDAO.GetAuthorize(staffId);
            var list = new List<String>();
            foreach (var var in listAuthorize)
            {
                list.Add(var.CaseId+"");
            }
            return list;
        }

        public int GetRole(int staffId)
        {
            return userDAO.GetRole(staffId);
        }

    }
}