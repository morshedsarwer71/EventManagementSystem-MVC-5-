using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class EmployeeEntitlement
    {
        [Key]
        public int EntitlementId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee Type required in english")]
        public string EmployeeTypeEN { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Employee Type required in arabic")]
        public string EmployeeTypeAR { get; set; }
        public int ConcernId { get; set; }
    }
}