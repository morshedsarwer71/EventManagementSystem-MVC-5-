using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IEmployee
    {
        void AddEmployee(Employee employee, int userId, string userName, int ConcernId);
        void DeleteEmployee(int EmployeeId, int userId, string userName);
        void UpdateEmployee(Employee employee, int Employeeid, int userId, string userName);
        IEnumerable<ResponseEmployee> Employees(int userId, string userName, int ConcernId);
        List<Employee> WorkOrderEmployee(int userId, string userName, int ConcernId);
        ResponseEmployee EmployeeDetails(int EmployeeId, int userId, string userName, int concernId);
        Employee EmployeeById(int employeeId, int userId, string userName);
    }
}
