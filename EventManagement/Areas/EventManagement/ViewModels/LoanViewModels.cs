using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ViewModels
{
    public class LoanViewModels
    {
        public IEnumerable<ResponseLoan> ResponseLoans { get; set; }
        public IEnumerable<Lender> Lenders { get; set; }
        public Loan Loan { get; set; }
        public LoanInstallment LoanInstallment { get; set; }
    }
}