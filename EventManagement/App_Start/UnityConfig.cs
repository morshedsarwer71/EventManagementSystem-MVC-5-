using EventManagement.Areas.Accounting.Interfaces;
using EventManagement.Areas.Accounting.Services;
using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Services;
using EventManagement.Areas.Global.Interfaces;
using EventManagement.Areas.Global.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace EventManagement
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            //global
            container.RegisterType<ISystemUser, SystemUserService>();
            container.RegisterType<ISettings, SettingsServices>();
            container.RegisterType<IJournal, JournalService>();
            container.RegisterType<IReport, ReportService>();
            container.RegisterType<IEventReport, EventReportService>();
            container.RegisterType<IAttendance, AttendanceService>();
            container.RegisterType<IEmployeeEntitlements, EmployeeEntitlementsService>();
            container.RegisterType<IEmployee, EmployeeService>();
            container.RegisterType<IEmployeesMiscellaneousAssignment, EmployeesMiscellaneousAssignmentService>();
            container.RegisterType<IEmployeesServiceAssignment, EmployeesServiceAssignmentService>();
            container.RegisterType<IEmployeesSetupAssignment, EmployeesSetupAssignmentService>();
            container.RegisterType<IEventManagementClient, EventManagementClientService>();
            container.RegisterType<IEventMiscellaneous, EventMiscellaneousService>();
            container.RegisterType<IEventService, EventServiceService>();
            container.RegisterType<IEventSetup, EventSetupService>();
            container.RegisterType<IWorkOrderManpower, WorkOrderManpowerService>();
            container.RegisterType<IWorkOrderMiscellaneous, WorkOrderMiscellaneousService>();
            container.RegisterType<IWorkOrderParent, WorkOrderParentService>();
            container.RegisterType<IWorkOrderService, WorkOrderServiceService>();
            container.RegisterType<IWorkOrderSetup, WorkOrderSetupService>();
            container.RegisterType<IDefaultSetting, DefaultSettingService>();
            container.RegisterType<ITransaction, TransactionService>();
            container.RegisterType<IEventAccount, EventAccountService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}