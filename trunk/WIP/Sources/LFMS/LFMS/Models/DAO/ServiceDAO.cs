using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.SqlServer.Server;
using LFMS.Utilities;

namespace LFMS.Models.DAO
{
    public class ServiceDAO
    {
        LFMSEntities db;
        public ServiceDAO()
        {
            db = new LFMSEntities();
        }

        public List<Service> GetAllService()
        {
            var serviceList = db.Services.ToList();
            return serviceList;
        }

        public List<Object> GetAllServiceJson()
        {
            List<Service> listService = GetAllService();
            var level1 = new List<string>();
            level1.Add("ServiceType");
            var list = new List<object>();

            foreach (var ser in listService)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(ser, level1);
                list.Add(result);
            }
            return list;
        }

        public Service GetServiceByID(int id)
        {
            var service = db.Services.Where(c => c.ServiceId == id).FirstOrDefault();
            return service;
        }

        public bool AddService(string name, string description, int typeId)
        {
            Service service = new Service();
            service.ServiceName = name;
            service.Description = description;
            service.ServiceTypeId = typeId;
          
            try
            {
                db.Services.Add(service);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool UpdateService(int id, string name, string description, int typeId)
        {
            Service service = GetServiceByID(id);
            if (service!=null)
            {
                try
                {
                    service.ServiceName = name;
                    service.Description = description;
                    service.ServiceTypeId = typeId;
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

        public bool DeleteService(int id)
        {
            Service service = GetServiceByID(id);
            if (service != null)
            {
                try
                {
                    db.Services.Remove(service);
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

   

        public List<ServiceType> GetAllServiceType()
        {
            var serviceTypeList = db.ServiceTypes.ToList();
            return serviceTypeList;
        }

        public List<Object> GetAllServiceTypeJson()
        {
            List<ServiceType> listService = GetAllServiceType();
            var level1 = new List<string>();
            level1.Add("Services");
            var list = new List<object>();

            foreach (var ser in listService)
            {
                var result = UtilityClass.ConvertDynamicObjectWithFullAttr(ser, level1);
                list.Add(result);
            }
            return list;
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
            if (serviceType != null)
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
            return false;
        }

        public bool DeleteServiceType(int id)
        {
            ServiceType serviceType = GetServiceTypeByID(id);
            if (serviceType != null)
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
            return false;
        }

   
    }
}