using EventManagement.Areas.EventManagement.Models;
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
        void AddEventPayment(EventPayment eventPayment, int concernId, string userName, int userId);
        void AddExpenditureHead(ExpenditureHead expenditureHead, int concernId, string userName, int userId);
        IEnumerable<ExpenditureHead> ExpenditureHeads(int concernId, string userName, int userId);
        IEnumerable<TransactionType> TransactionTypes();
        IEnumerable<SalaryType> SalaryTypes();
    }
}
