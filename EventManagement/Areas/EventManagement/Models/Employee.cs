using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee Code required")]
        public string EmployeeCode { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "First Name required")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name required")]
        public string LastName { get; set; }
        public string NickName { get; set; }
        //[Required(AllowEmptyStrings = false, ErrorMessage = "required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string EmployeeEmail { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Contact Number required")]
        public string ContactNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Emergency Number required")]
        public string EmergencyNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Permanent Address required")]
        public string PermanentAddress { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Present Address required")]
        public string PresentAddress { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date Of Birth Date required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Passport Number required")]
        public string PassportNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Passport Exp. Date required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PassportExpiratationDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "NID Number required")]
        public string NIDNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Visa Number required")]
        public string VisaNumber { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Visa Exp. Date required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime VisaExpirationDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee Ent. required")]
        public int EmployeeEntitlementId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Monthly Salary required")]
        public decimal MonthlySalary { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Rate Per Hour required")]
        public decimal RatePerHour { get; set; }
        public string EmployeeImagePath { get; set; }
        public string PassportImagePath { get; set; }
        public string Nationality { get; set; }
        [Required]
        public int IsActive { get; set; }
        [Required]
        public int ConcernId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
        [NotMapped]
        public int SerialNumber { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageUrl { get; set; }
        [NotMapped]
        public HttpPostedFileBase PassportUrl { get; set; }
    }
}