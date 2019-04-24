using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.PlainModels;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ViewModels
{
    public class EmployeeViewModels
    {
        public IEnumerable<ResponseEmployee> Employees { get; set; }
        public Employee Employee { get; set; }
        public IEnumerable<EmployeeEntitlement> Entitlements { get; set; }
        public IEnumerable<IsActive> IsActives { get; set; }
        public IEnumerable<ManpowerSupplier> ManpowerSuppliers { get; set; }
        public IEnumerable<ResponseEmployeeWork> EmployeeWorks { get; set; }
    }
}