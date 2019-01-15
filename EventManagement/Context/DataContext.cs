using EventManagement.Areas.Accounting.Models;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.Global.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventManagement.Context
{
    public class DataContext:DbContext
    {
        public DataContext() : base("EventDBContext") { }
        //Accounting Data Context
        public DbSet<AccountsHead> AccountsHeads { get; set; }
        public DbSet<ReportHead> ReportHeads { get; set; }
        public DbSet<Journal> Journals { get; set; }


        //Global Data Context
        public DbSet<Concern> Concerns { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }


        //EventManagement Data Context

        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeEntitlement> EmployeeEntitlements { get; set; }
        public DbSet<EmployeesMiscellaneousAssignment> EmployeesMiscellaneousAssignments { get; set; }
        public DbSet<EmployeesServiceAssignment> EmployeesServiceAssignments { get; set; }
        public DbSet<EmployeesSetupAssignment> EmployeesSetupAssignments { get; set; }
        public DbSet<EventManagementClient> EventManagementClients { get; set; }
        public DbSet<EventMiscellaneous> EventMiscellaneouses { get; set; }
        public DbSet<EventService> EventServices { get; set; }
        public DbSet<EventSetup> EventSetups { get; set; }
        public DbSet<WorkOrderChildManpower> WorkOrderChildManpowers { get; set; }
        public DbSet<WorkOrderChildMiscellaneous> WorkOrderChildMiscellaneouses { get; set; }
        public DbSet<WorkOrderChildService> WorkOrderChildServices { get; set; }
        public DbSet<WorkOrderChildSetup> WorkOrderChildSetups { get; set; }
        public DbSet<WorkOrderParent> WorkOrderParents { get; set; }
        public DbSet<WorkOrderStatus> WorkOrderStatuses { get; set; }
        public DbSet<WorkOrderQuotation> WorkOrderQuotations { get; set; }

    }
}