using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IEmployeesServiceAssignment
    {
        void AddEmployeesServiceAssignment(int workOrderId, EmployeesServiceAssignment employeesServiceAssignment);
        EmployeesServiceAssignment EmployeesService(int workOrderId, int userId, string userName);
        List<ResponseEmployeesServiceAssignment> ResponseEmployeesServiceAssignments(int workOrderId, int userId, string userName);
    }
}
