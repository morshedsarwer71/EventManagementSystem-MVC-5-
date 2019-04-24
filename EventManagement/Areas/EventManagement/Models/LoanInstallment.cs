using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class LoanInstallment
    {
        [Key]
        public int LoanInstallmentId { get; set; }
        public int LenderId { get; set; }
        public decimal InstallmentAmount { get; set; }
        public DateTime InstallmentDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public int ConcernId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
        public string Description { get; set; }
    }
}