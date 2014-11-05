using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LFMS.Models.DAO
{
    public class ServiceTypeDAO
    {
        LFMSEntities db;
        public ServiceTypeDAO()
        {
            db = new LFMSEntities();
        }

        public List<ServiceType> GetAllServiceType()
        {
            var serviceTypeList = db.ServiceTypes.ToList();
            return serviceTypeList;
        }

        public ServiceType GetServiceTypeByID(int id)
        {
            var serviceType = db.ServiceTypes.Where(c => c.ServiceTypeId == id).FirstOrDefault();
            return serviceType;
        }

        public bool AddServiceType(string name, string description)
        {
            ServiceType serviceType = new ServiceType();
            serviceType.ServiceTypeName = name;
            serviceType.Description = description;
          
            try
            {
                db.ServiceTypes.Add(serviceType);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateServiceType(int id, string name, string description)
        {
            ServiceType serviceType = GetServiceTypeByID(id);
            if (serviceType==null)
            {
                return false;
            }
            else
            {
                try
                {
                    serviceType.ServiceTypeName = name;
                    serviceType.Description = description;
                    
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

        public bool DeleteServiceType(int id)
        {
            ServiceType serviceType = GetServiceTypeByID(id);
            if (serviceType == null)
            {
                return false;
            }
            else
            {
                try
                {
                    db.ServiceTypes.Remove(serviceType);
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