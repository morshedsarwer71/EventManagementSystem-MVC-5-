using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class EmployeeEntitlementsService: IEmployeeEntitlements
    {
        private DataContext _contextEntitle;
        public EmployeeEntitlementsService(DataContext context)
        {
            _contextEntitle = context;
        }

        public void Add(EmployeeEntitlement empEnt, int concernId)
        {
            empEnt.ConcernId = concernId;
            _contextEntitle.EmployeeEntitlements.Add(empEnt);
            _contextEntitle.SaveChanges();

        }

        public void Delete(int id)
        {
            var delete = GetEmployeeEntitlement(id);
            _contextEntitle.EmployeeEntitlements.Remove(delete);
            _contextEntitle.SaveChanges();
        }

        public IEnumerable<EmployeeEntitlement> GetAll(int concernId)
        {
            return _contextEntitle.EmployeeEntitlements.Where(x => x.ConcernId == concernId);
        }

        public EmployeeEntitlement GetEmployeeEntitlement(int id)
        {
            return _contextEntitle.EmployeeEntitlements.FirstOrDefault(x => x.EntitlementId == id);
        }

        public void Update(EmployeeEntitlement entitlementUpdate, int id)
        {
            var employeeEntitlement = GetEmployeeEntitlement(id);
            employeeEntitlement.EmployeeTypeEN = entitlementUpdate.EmployeeTypeEN;
            employeeEntitlement.EmployeeTypeAR = entitlementUpdate.EmployeeTypeAR;
            _contextEntitle.SaveChanges();
        }
    }
}