using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.Models
{
    public class AccountsHead
    {
        [Key]
        public int AccountsHeadId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Account Head reqiured in English")]
        public string AccountsHeadNameEN { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Account Head reqiured in Arabic")]
        public string AccountsHeadNameAR { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Transaction Type required")]
        public int TransactionType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Report head name required")]
        public int ReportHeadId { get; set; }
        public int ConcernId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
    }
}