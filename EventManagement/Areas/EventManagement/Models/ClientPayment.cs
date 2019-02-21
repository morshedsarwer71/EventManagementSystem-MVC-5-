using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class ClientPayment
    {
        public int ClientPaymentId { get; set; }
        public int ClientId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public int PaymentType { get; set; }
        public int BankId { get; set; }
        public string PayeesName { get; set; }
        public string Notes { get; set; }
        public int ConcernId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
        public string Description { get; set; }
    }
}