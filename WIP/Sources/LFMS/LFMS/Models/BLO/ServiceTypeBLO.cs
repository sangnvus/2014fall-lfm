using LFMS.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFMS.Models.BLO
{
    public class ServiceTypeBLO
    {
        private ServiceTypeDAO serviceTypeDAO;

        public ServiceTypeBLO()
        {
            serviceTypeDAO = new ServiceTypeDAO();
        }

        public List<ServiceType> GetAllServiceType()
        {
            return serviceTypeDAO.GetAllServiceType();
        }

        public ServiceType GetServiceTypeByID(int id)
        {
            return serviceTypeDAO.GetServiceTypeByID(id);
        }

        public bool AddServiceType(string name, string description)
        {
            return serviceTypeDAO.AddServiceType(name, description);
        }

        public bool UpdateServiceType(int id, string name, string description)
        {
            return serviceTypeDAO.UpdateServiceType(id, name, description);
        }

        public bool DeleteServiceType(int id)
        {
            return serviceTypeDAO.DeleteServiceType(id);
        }
    }
}