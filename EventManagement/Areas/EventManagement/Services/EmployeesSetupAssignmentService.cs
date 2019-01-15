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
    public class EmployeesSetupAssignmentService: IEmployeesSetupAssignment
    {
        private readonly DataContext _context;
        public EmployeesSetupAssignmentService(DataContext context)
        {
            _context = context;
        }
        public void AddEmployeesSetupAssignment(int workOrderId, EmployeesSetupAssignment employeesSetupAssignment)
        {
            List<EmployeesSetupAssignment> EmployeesSetupAssignmentList = new List<EmployeesSetupAssignment>();
            foreach (var item in employeesSetupAssignment.WorkOrderChildManpower)
            {
                if (item.IsChecked == true)
                {
                    EmployeesSetupAssignmentList.Add(new EmployeesSetupAssignment()
                    {
                        WorkOrderId = workOrderId,
                        EmployeeId = item.EmployeeId
                    });
                }
            }
            foreach (var item in EmployeesSetupAssignmentList)
            {
                _context.EmployeesSetupAssignments.Add(item);
            }

            _context.SaveChanges();
        }

        public EmployeesSetupAssignment EmployeesSetup(int workOrderId, int UserId, string UserName)
        {
            var workOrderChildManpower = _context.WorkOrderChildManpowers.Where(x => x.WorkOrderId == workOrderId).ToList();
            EmployeesSetupAssignment employeesSetupAssignment = new EmployeesSetupAssignment();
            employeesSetupAssignment.WorkOrderChildManpower = workOrderChildManpower;
            return employeesSetupAssignment;
        }

        public List<ResponseEmployeesSetupAssignment> ResponseEmployeesSetupAssignments(int workOrderId, int UserId, string UserName)
        {
            List<ResponseEmployeesSetupAssignment> responseEmployeesSetupAssignments = new List<ResponseEmployeesSetupAssignment>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_GetEmployeesSetupAssignment @workOrderId");
                command.Parameters.Add(new SqlParameter("workOrderId", workOrderId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ResponseEmployeesSetupAssignment responseEmployeesSetup = new ResponseEmployeesSetupAssignment();
                            responseEmployeesSetup.SetupAssignmentSerialNumber = Convert.ToInt32(result[0]);
                            responseEmployeesSetup.SetupAssignmentEmployeeId = Convert.ToInt32(result[1]);
                            responseEmployeesSetup.SetupAssignmentEmployeeFirstName = Convert.ToString(result[2]);
                            responseEmployeesSetup.SetupAssignmentEmployeeLastName = Convert.ToString(result[3]);
                            responseEmployeesSetupAssignments.Add(responseEmployeesSetup);
                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return responseEmployeesSetupAssignments;
        }
    }
}