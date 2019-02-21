using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.PlainModels;
using EventManagement.Areas.EventManagement.ResponseModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class EmployeeService : IEmployee
    {
        private DataContext _context;
        public EmployeeService(DataContext context)
        {
            _context = context;
        }

        public void AddEmployee(Employee employee, int userId, string userName, int ConcernId)
        {
            var date = DateTime.Now;
            employee.CreatorId = userId;
            employee.CreationDate = date;
            employee.ConcernId = ConcernId;
            employee.ModificationDate = date;
            employee.ModifierId = userId;
            _context.Employees.Add(employee);
            _context.SaveChanges();
        }

        public void DeleteEmployee(int EmployeeId, int userId, string userName)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == EmployeeId);
            employee.IsDelete = 1;
            _context.SaveChanges();
        }

        public Employee EmployeeById(int employeeId, int userId, string userName)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId == employeeId);
            return employee;
        }

        public ResponseEmployee EmployeeDetails(int EmployeeId, int userId, string userName, int concernId)
        {
            ResponseEmployee responseEmployee = new ResponseEmployee();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_EmployeeDetailsById @employeeId,@concernId");
                command.Parameters.Add(new SqlParameter("employeeId", EmployeeId));
                command.Parameters.Add(new SqlParameter("concernId", concernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            responseEmployee.EmployeeCode = Convert.ToString(result[0]);
                            responseEmployee.FirstName = Convert.ToString(result[1]);
                            responseEmployee.LastName = Convert.ToString(result[2]);
                            responseEmployee.NickName = Convert.ToString(result[3]);
                            responseEmployee.EmployeeEmail = Convert.ToString(result[4]);
                            responseEmployee.ContactNumber = Convert.ToString(result[5]);
                            responseEmployee.EmergencyNumber = Convert.ToInt32(result[6]);
                            responseEmployee.PermanentAddress = Convert.ToString(result[7]);
                            responseEmployee.PresentAddress = Convert.ToString(result[8]);
                            responseEmployee.DateOfBirth = Convert.ToDateTime(result[9]);
                            responseEmployee.PassportNumber = Convert.ToString(result[10]);
                            responseEmployee.PassportExpiratationDate = Convert.ToDateTime(result[11]);
                            responseEmployee.NIDNumber = Convert.ToString(result[12]);
                            responseEmployee.VisaNumber = Convert.ToString(result[13]);
                            responseEmployee.VisaExpirationDate = Convert.ToDateTime(result[14]);
                            responseEmployee.EmployeeEntitlement = Convert.ToString(result[15]);
                            responseEmployee.MonthlySalary = Convert.ToDecimal(result[16]);
                            responseEmployee.RatePerHour = Convert.ToDecimal(result[17]);
                            responseEmployee.IsActive = Convert.ToString(result[18]);
                            responseEmployee.CreatorName = Convert.ToString(result[19]);
                            responseEmployee.CreationDate = Convert.ToDateTime(result[20]);
                        }
                    }
                }
                _context.Database.Connection.Close();
            }

            return responseEmployee;
        }

        public IEnumerable<ResponseEmployee> Employees(int userId, string userName, int ConcernId, int Page)
        {
            List<ResponseEmployee> ResponseEmployeeList = new List<ResponseEmployee>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_EmployeeIndexByConcernId @concernId,@Page");
                command.Parameters.Add(new SqlParameter("concernId", ConcernId));
                command.Parameters.Add(new SqlParameter("Page", Page));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ResponseEmployee responseEmployee = new ResponseEmployee();
                            responseEmployee.SerialNumber = Convert.ToInt32(result[0]);
                            responseEmployee.FirstName = Convert.ToString(result[1]);
                            responseEmployee.LastName = Convert.ToString(result[2]);
                            responseEmployee.EmployeeEntitlement = Convert.ToString(result[3]);
                            responseEmployee.MonthlySalary = Convert.ToDecimal(result[4]);
                            responseEmployee.ContactNumber = Convert.ToString(result[5]);
                            responseEmployee.EmployeeCode = Convert.ToString(result[6]);
                            responseEmployee.EmployeeId = Convert.ToInt32(result[7]);
                            responseEmployee.NumberOfRow = Convert.ToInt32(result[8]);
                            responseEmployee.EmployeeImage = Convert.ToString(result[9]);
                            responseEmployee.PassportImage = Convert.ToString(result[10]);
                            responseEmployee.PassportNumber = Convert.ToString(result[11]);

                            ResponseEmployeeList.Add(responseEmployee);
                        }
                    }
                }
            }
            return ResponseEmployeeList;
        }

        public IEnumerable<IsActive> IsActive()
        {
            List<IsActive> isActives = new List<IsActive>()
            {
                new IsActive()
                {
                    Id=1,
                    Name="Active"
                },
                new IsActive()
                {
                    Id=0,
                    Name="Deactive"
                }
            };
            return isActives;
        }

        public void Update(Employee employee, int Employeeid, int userId, string userName, string image, string passport)
        {

            var employeeById = _context.Employees.FirstOrDefault(x => x.EmployeeId == Employeeid);
            employeeById.FirstName = employee.FirstName;
            employeeById.LastName = employee.LastName;
            employeeById.NickName = employee.NickName;
            employeeById.EmployeeEmail = employee.EmployeeEmail;
            employeeById.ContactNumber = employee.ContactNumber;
            employeeById.EmergencyNumber = employee.EmergencyNumber;
            employeeById.DateOfBirth = employee.DateOfBirth;
            employeeById.PassportNumber = employee.PassportNumber;
            employeeById.PassportExpiratationDate = employee.PassportExpiratationDate;
            employeeById.NIDNumber = employee.NIDNumber;
            employeeById.VisaNumber = employee.VisaNumber;
            employeeById.VisaExpirationDate = employee.VisaExpirationDate;
            employeeById.Nationality = employee.Nationality;
            employeeById.EmployeeEntitlementId = employee.EmployeeEntitlementId;
            employeeById.MonthlySalary = employee.MonthlySalary;
            employeeById.RatePerHour = employee.RatePerHour;
            employeeById.IsActive = employee.IsActive;
            employeeById.ModificationDate = DateTime.Now;
            employeeById.ModifierId = userId;
            _context.SaveChanges();


        }

        public void UpdateEmployee(Employee employee, int EmployeeId, int userId, string userName, string image, string passport)
        {
            var employeeById = _context.Employees.FirstOrDefault(x => x.EmployeeId == EmployeeId);
            employeeById.FirstName = employee.FirstName;
            employeeById.LastName = employee.LastName;
            employeeById.NickName = employee.NickName;
            employeeById.EmployeeEmail = employee.EmployeeEmail;
            employeeById.ContactNumber = employee.ContactNumber;
            employeeById.EmergencyNumber = employee.EmergencyNumber;
            employeeById.PermanentAddress = employee.PermanentAddress;
            employeeById.PresentAddress = employee.PresentAddress;
            employeeById.DateOfBirth = employee.DateOfBirth;
            employeeById.PassportNumber = employee.PassportNumber;
            employeeById.PassportExpiratationDate = employee.PassportExpiratationDate;
            employeeById.NIDNumber = employee.NIDNumber;
            employeeById.VisaNumber = employee.VisaNumber;
            employeeById.VisaExpirationDate = employee.VisaExpirationDate;
            employeeById.Nationality = employee.Nationality;
            employeeById.EmployeeEntitlementId = employee.EmployeeEntitlementId;
            employeeById.MonthlySalary = employee.MonthlySalary;
            employeeById.RatePerHour = employee.RatePerHour;
            employeeById.IsActive = employee.IsActive;
            employeeById.EmployeeImagePath = image;
            employeeById.PassportImagePath = passport;
            employeeById.ModificationDate = DateTime.Now;
            employeeById.ModifierId = userId;
            _context.SaveChanges();
        }

        public List<Employee> WorkOrderEmployee(int userId, string userName, int ConcernId)
        {
            List<Employee> Employeelist = new List<Employee>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_WorkOrderEmployeeId @concernId");
                command.Parameters.Add(new SqlParameter("concernId", ConcernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            Employee employee = new Employee();
                            employee.EmployeeId = Convert.ToInt32(result[0]);
                            employee.FirstName = Convert.ToString(result[1]);
                            employee.LastName = Convert.ToString(result[2]);
                            employee.MonthlySalary = Convert.ToDecimal(result[3]);
                            employee.EmployeeCode = Convert.ToString(result[4]);
                            employee.SerialNumber = Convert.ToInt32(result[5]);

                            Employeelist.Add(employee);
                        }
                    }

                }
            }

            return Employeelist;
        }
    }
}