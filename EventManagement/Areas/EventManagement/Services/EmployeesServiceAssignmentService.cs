using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class EmployeesServiceAssignmentService : IEmployeesServiceAssignment
    {
        private readonly DataContext _context;
        public EmployeesServiceAssignmentService(DataContext context)
        {
            _context = context;
        }
        public void AddEmployeesServiceAssignment(int workOrderId, EmployeesServiceAssignment employeesServiceAssignment)
        {
            List<EmployeesServiceAssignment> EmployeesServiceAssignmentList = new List<EmployeesServiceAssignment>();
            foreach (var item in employeesServiceAssignment.WorkOrderChildManpower)
            {
                if (item.IsChecked == true)
                {
                    EmployeesServiceAssignmentList.Add(new EmployeesServiceAssignment()
                    {
                        WorkOrderId = workOrderId,
                        EmployeeId = item.EmployeeId
                    });
                }
            }
            foreach (var item in EmployeesServiceAssignmentList)
            {
                _context.EmployeesServiceAssignments.Add(item);
            }

            _context.SaveChanges();
        }

        public EmployeesServiceAssignment EmployeesService(int workOrderId, int userId, string userName)
        {
            var workOrderChildManpower = _context.WorkOrderChildManpowers.Where(x => x.WorkOrderId == workOrderId).ToList();
            EmployeesServiceAssignment employeesServiceAssignment = new EmployeesServiceAssignment();
            employeesServiceAssignment.WorkOrderChildManpower = workOrderChildManpower;
            return employeesServiceAssignment;
        }

        public List<ResponseEmployeesServiceAssignment> ResponseEmployeesServiceAssignments(int workOrderId, int userId, string userName)
        {
            List<ResponseEmployeesServiceAssignment> responseEmployeesServiceAssignments = new List<ResponseEmployeesServiceAssignment>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_GetEmployeesServiceAssignment @workOrderId");
                command.Parameters.Add(new SqlParameter("workOrderId", workOrderId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ResponseEmployeesServiceAssignment responseEmployeesSetup = new ResponseEmployeesServiceAssignment();
                            responseEmployeesSetup.ServiceAssignmentSerialNumber = Convert.ToInt32(result[0]);
                            responseEmployeesSetup.ServiceAssignmentEmployeeId = Convert.ToInt32(result[1]);
                            responseEmployeesSetup.ServiceAssignmentEmployeeFirstName = Convert.ToString(result[2]);
                            responseEmployeesSetup.ServiceAssignmentEmployeeLastName = Convert.ToString(result[3]);
                            responseEmployeesServiceAssignments.Add(responseEmployeesSetup);
                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return responseEmployeesServiceAssignments;
        }
    }
}