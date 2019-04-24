using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IEventAccount
    {
        void AddLender(Lender lender,int userId,string userName,int concernId);
        void UpdateLender(Lender lender,int userId, string userName,int concernId,int id);
        Lender Lender(int userId, string userName,int concernId,int id);
        void AddLoan(Loan loan, int userId, string userName, int concernId);
        void UpdateLoan(Loan loan, int userId, string userName, int concernId,int id);
        Loan Loan(int userId, string userName, int concernId, int id);
        IEnumerable<ResponseLoan> Loans(int userId, string userName, int concernId);
        IEnumerable<Lender> Lenders(int userId, string userName, int concernId);
        IEnumerable<ResponseLoan> LoanInstallments(int userId, string userName, int concernId);
        IEnumerable<ResponseLoan> LoanInstallmentsReport(int userId, string userName, int concernId,string fromDate,string toDate,int lenderId);
        void AddLoanInstallment(LoanInstallment loanInstallment, int userId, string userName, int concernId);
        void UpdateLoanInstallment(LoanInstallment loanInstallment, int userId, string userName, int concernId,int id);
        LoanInstallment LoanInstallment(int userId, string userName, int concernId, int id);
        IEnumerable<ResponseLoan> TotalDueLoan(int concernId);
        IEnumerable<ResponsePaymentReport> ClientPaymentReport(int concernId);
        IEnumerable<ResponsePaymentReport> WorkerPaymentReport(int concernId);
    }
}
