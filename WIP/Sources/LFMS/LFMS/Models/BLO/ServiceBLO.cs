using LFMS.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LFMS.Utilities;
using Microsoft.Ajax.Utilities;

namespace LFMS.Models.BLO
{
    public class ServiceBLO
    {
        private ServiceDAO serviceDAO;

        public ServiceBLO()
        {
            serviceDAO = new ServiceDAO();
        }

        public List<Service> GetAllService()
        {
            return serviceDAO.GetAllService();
        }

        public List<Object> GetAllServiceJson()
        {
            return serviceDAO.GetAllServiceJson();
        }

        public Service GetServiceByID(int id)
        {
            return serviceDAO.GetServiceByID(id);
        }

        public string AddService(string name, string description, int typeId)
        {
            if (!name.IsNullOrWhiteSpace() && typeId != 0)
            {
                bool result = serviceDAO.AddService(name, description, typeId);
                if (result)
                {
                    return "success";
                }
                return "fail";
            }
            return "fail";
        }

  

        public string UpdateService(int id, string name, string description, int typeId)
        {
            if (!name.IsNullOrWhiteSpace() && typeId != 0)
            {
                bool result = serviceDAO.UpdateService(id, name, description, typeId);
                if (result)
                {
                    return "success";
                }
                return "fail";
            }
            return "fail";
        }

        public string DeleteService(int id)
        {
            bool result = serviceDAO.DeleteService(id);
            if (result)
            {
                return "success";
            }
            return "fail";
        }
  


        public List<ServiceType> GetAllServiceType()
        {
            return serviceDAO.GetAllServiceType();
        }

        public List<Object> GetAllServiceTypeJson()
        {
            return serviceDAO.GetAllServiceTypeJson();
        }

        public ServiceType GetServiceTypeByID(int id)
        {
            return serviceDAO.GetServiceTypeByID(id);
        }

        public string AddServiceType(string name, string description)
        {
            bool result = serviceDAO.AddServiceType(name, description);
            if (result)
            {
                return "success";
            }
            return "fail";
        }

        public string UpdateServiceType(int id, string name, string description)
        {
            bool result = serviceDAO.UpdateServiceType(id, name, description);
            if(result)
            {
                return "success";
            }
            return "fail";
        }

        public string DeleteServiceType(int id)
        {
            bool result = serviceDAO.DeleteServiceType(id);
            if (result)
            {
                return "success";
            }
            return "fail";
        }
   
    }
}