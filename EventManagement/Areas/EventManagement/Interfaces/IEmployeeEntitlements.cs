using EventManagement.Areas.EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IEmployeeEntitlements
    {
        IEnumerable<EmployeeEntitlement> GetAll(int concernId);
        void Add(EmployeeEntitlement empEnt, int concernId);
        EmployeeEntitlement GetEmployeeEntitlement(int id);
        void Update(EmployeeEntitlement entitlementUpdate, int id);
        void Delete(int id);
    }
}
