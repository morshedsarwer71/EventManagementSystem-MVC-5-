using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ReportObject;
using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface ITransaction
    {
        void AddExpenditure(Expenditure expenditure, int concernId, string userName, int userId);
        void AddSalaryPayment(SalaryPayment salaryPayment, int concernId, string userName, int userId);
        void AddExpenditureHead(ExpenditureHead expenditureHead, int concernId, string userName, int userId);
        void AddClientPayment(ClientPayment clientPayment, int concernId, string userName, int userId);
        ClientPayment ClientPaymentById(int id,int concernId, string userName, int userId);
        void UpdateClientPayment(ClientPayment clientPayment,int id,int concernId, string userName, int userId);
        IEnumerable<ExpenditureHead> ExpenditureHeads(int concernId, string userName, int userId);
        IEnumerable<TransactionType> TransactionTypes();
        IEnumerable<SalaryType> SalaryTypes();
        IEnumerable<ResponseSalaryPayments> ResponseSalaries(int concernId, string userName, int userId);
        IEnumerable<ResponseSalaryPayments> ResponseSalariesRepor(int concernId, string userName, int userId,string fromDate,string toDate,int employeeId,int transTypeId);
        IEnumerable<ResponseExpenditure> ResponseExpenditures(int concernId, string userName, int userId);        
        IEnumerable<ResponseExpenditure> ReportExpenditures(int concernId, ExpenditureReport expenditure, string userName, int userId);
        IEnumerable<Bank> Banks(int concernId, string userName, int userId);
        IEnumerable<EventManagementClient> Clients(int concernId, string userName, int userId);
        IEnumerable<ResponseClientPayment> ClientPayments(int concernId, string userName, int userId,string culture);
    }
}
