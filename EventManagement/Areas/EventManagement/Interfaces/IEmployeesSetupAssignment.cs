using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IEmployeesSetupAssignment
    {
        void AddEmployeesSetupAssignment(int workOrderId, EmployeesSetupAssignment employeesSetupAssignment);
        List<ResponseEmployeesSetupAssignment> ResponseEmployeesSetupAssignments(int workOrderId, int UserId, string UserName);
        EmployeesSetupAssignment EmployeesSetup(int workOrderId, int UserId, string UserName);
    }
}
