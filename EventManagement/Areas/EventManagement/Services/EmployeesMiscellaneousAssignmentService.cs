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
    public class EmployeesMiscellaneousAssignmentService: IEmployeesMiscellaneousAssignment
    {
        private readonly DataContext _context;
        public EmployeesMiscellaneousAssignmentService(DataContext context)
        {
            _context = context;
        }
        public void AddEmployeesMiscellaneousAssignment(int workOrderId, EmployeesMiscellaneousAssignment employeesMiscellaneousAssignment)
        {
            List<EmployeesMiscellaneousAssignment> EmployeesMiscellaneousAssignmentList = new List<EmployeesMiscellaneousAssignment>();
            foreach (var item in employeesMiscellaneousAssignment.WorkOrderChildManpower)
            {
                if (item.IsChecked == true)
                {
                    EmployeesMiscellaneousAssignmentList.Add(new EmployeesMiscellaneousAssignment()
                    {
                        WorkOrderId = workOrderId,
                        EmployeeId = item.EmployeeId

                    });
                }
            }
            foreach (var item in EmployeesMiscellaneousAssignmentList)
            {
                _context.EmployeesMiscellaneousAssignments.Add(item);
            }

            _context.SaveChanges();
        }

        public EmployeesMiscellaneousAssignment EmployeesMiscellaneousAssignment(int workOrderId, int userId, string userName)
        {
            var workOrderChildManpower = _context.WorkOrderChildManpowers.Where(x => x.WorkOrderId == workOrderId).ToList();
            EmployeesMiscellaneousAssignment employeesMiscellaneousAssignment = new EmployeesMiscellaneousAssignment();
            employeesMiscellaneousAssignment.WorkOrderChildManpower = workOrderChildManpower;
            return employeesMiscellaneousAssignment;
        }

        public List<ResponseEmployeesMiscellaneousAssignment> ResponseEmployeesMiscellaneousAssignments(int workOrderId, int userId, string userName)
        {
            List<ResponseEmployeesMiscellaneousAssignment> ResponseEmployeesMiscellaneousAssignment = new List<ResponseEmployeesMiscellaneousAssignment>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_GetEmployeesMiscellaneousAssignment @workOrderId");
                command.Parameters.Add(new SqlParameter("workOrderId", workOrderId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ResponseEmployeesMiscellaneousAssignment responseEmployeesMiscellaneousAssignment = new ResponseEmployeesMiscellaneousAssignment();
                            responseEmployeesMiscellaneousAssignment.MiscellaneousAssignmentSerialNumber = Convert.ToInt32(result[0]);
                            responseEmployeesMiscellaneousAssignment.MiscellaneousAssignmentEmployeeId = Convert.ToInt32(result[1]);
                            responseEmployeesMiscellaneousAssignment.MiscellaneousAssignmentEmployeeFirstName = Convert.ToString(result[2]);
                            responseEmployeesMiscellaneousAssignment.MiscellaneousAssignmentEmployeeLastName = Convert.ToString(result[3]);
                            ResponseEmployeesMiscellaneousAssignment.Add(responseEmployeesMiscellaneousAssignment);
                        }
                    }
                }
            }
            return ResponseEmployeesMiscellaneousAssignment;
        }
    }
}