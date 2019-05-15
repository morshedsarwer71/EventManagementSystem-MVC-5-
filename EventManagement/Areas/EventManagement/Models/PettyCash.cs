using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class PettyCash
    {
        [Key]
        public int PettyCasId { get; set; }
        public DateTime CashDate { get; set; }
        public int TransactionType { get; set; }
        public int BankId { get; set; }
        public decimal Amount { get; set; }
        public int ConcernId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
        public string Description { get; set; }

    }
}