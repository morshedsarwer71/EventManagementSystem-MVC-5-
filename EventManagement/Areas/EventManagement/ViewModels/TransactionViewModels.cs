using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ViewModels
{
    public class TransactionViewModels
    {
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<SalaryType> SalaryTypes { get; set; }
        public IEnumerable<TransactionType> TransactionTypes { get; set; }
        public IEnumerable<SalaryPayment> SalaryPayments { get; set; }
        public IEnumerable<ExpenditureHead> ExpenditureHeads { get; set; }
    }
}