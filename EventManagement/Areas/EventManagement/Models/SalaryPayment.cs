using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class SalaryPayment
    {
        [Key]
        public int SalaryPaymentId { get; set; }
        public int SalaryTypeId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PaymentDate { get; set; }
        public int TransactionTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime SalaryMonth { get; set; }
        public int ConcernId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
        public string Description { get; set; }

    }
}