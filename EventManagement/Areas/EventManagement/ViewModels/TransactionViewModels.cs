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
        //public IEnumerable<SalaryPayment> SalaryPayments { get; set; }
        public IEnumerable<ResponseSalaryPayments> SalaryPayments { get; set; }
        public IEnumerable<ExpenditureHead> ExpenditureHeads { get; set; }
        public IEnumerable<ResponseExpenditure> ResponseExpenditures { get; set; }
        public IEnumerable<Bank> Banks { get; set; }
        public IEnumerable<ResponsePettyCash> ResponsePettyCash { get; set; }
        public IEnumerable<EventManagementClient> Clients { get; set; }
        public IEnumerable<ResponseClientPayment> ClientPayments { get; set; }
        public IEnumerable<ResponsePaymentReport> PaymentReport { get; set; }
        public ClientPayment ClientPayment { get; set; }
        public PettyCash PettyCash { get; set; }
    }
}