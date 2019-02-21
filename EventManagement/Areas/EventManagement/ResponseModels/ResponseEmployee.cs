using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseEmployee
    {
        public int EmployeeId { get; set; }
        public int SerialNumber { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string EmployeeEmail { get; set; }
        public string ContactNumber { get; set; }
        public int EmergencyNumber { get; set; }
        public string PermanentAddress { get; set; }
        public string PresentAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PassportNumber { get; set; }
        public DateTime PassportExpiratationDate { get; set; }
        public string NIDNumber { get; set; }
        public string VisaNumber { get; set; }
        public DateTime VisaExpirationDate { get; set; }
        public string EmployeeEntitlement { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal RatePerHour { get; set; }
        public string IsActive { get; set; }
        public string ConcernName { get; set; }
        public string CreatorName { get; set; }
        public DateTime CreationDate { get; set; }
        public string  EmployeeImage{ get; set; }
        public string  PassportImage{ get; set; }
        public int  NumberOfRow{ get; set; }
    }
}