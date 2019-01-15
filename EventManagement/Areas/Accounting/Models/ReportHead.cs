using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.Models
{
    public class ReportHead
    {
        [Key]
        public int ReportHeadId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Account Head reqiured in English")]
        public string ReportHeadNameEN { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Account Head reqiured in Arabic")]
        public string ReportHeadNameAR { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Report type required")]
        public int ReportType { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public int ConcernId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
    }
}