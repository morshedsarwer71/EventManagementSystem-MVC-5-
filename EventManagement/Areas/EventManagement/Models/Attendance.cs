using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Attendance Date required")]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AttendanceDate { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "In time required")]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime InTime { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Out time required ")]
        [DataType(DataType.DateTime)]
        //[DisplayFormat(DataFormatString = "{0:yyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime OutTime { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " Employee name required")]
        public int EmployeeId { get; set; }
        public int ConcernId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
        [NotMapped]
        public List<Employee> Employee { get; set; }
    }
}