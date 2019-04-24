using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class Loan
    {
        [Key]
        public int LoanId { get; set; }
        public int LenderId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime LoanPaidDate { get; set; }
        public decimal LoanAmount { get; set; }
        public int NumberOfIntallment { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public int ConcernId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
        public string Description { get; set; }
    }
}