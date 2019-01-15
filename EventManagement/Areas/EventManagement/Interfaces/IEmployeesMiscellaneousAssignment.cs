using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IEmployeesMiscellaneousAssignment
    {
        void AddEmployeesMiscellaneousAssignment(int workOrderId, EmployeesMiscellaneousAssignment employeesMiscellaneousAssignment);
        EmployeesMiscellaneousAssignment EmployeesMiscellaneousAssignment(int workOrderId, int userId, string userName);
        List<ResponseEmployeesMiscellaneousAssignment> ResponseEmployeesMiscellaneousAssignments(int workOrderId, int userId, string userName);
    }
}
