using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class TransactionService: ITransaction
    {
        private readonly DataContext _context;
        public TransactionService(DataContext context)
        {
            _context = context;
        }

        public void AddEventPayment(EventPayment eventPayment, int concernId, string userName, int userId)
        {
            var date = DateTime.Now;
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                eventPayment.CreationDate = date;
                eventPayment.CreatorId = userId;
                eventPayment.ModificationDate = date;
                eventPayment.ModifierId = userId;
                eventPayment.ConcernId = concernId;
                _context.EventPayments.Add(eventPayment);
                _context.SaveChanges();
                transaction.Commit();
            };
            
        }

        public void AddExpenditure(Expenditure expenditure, int concernId, string userName, int userId)
        {
            var date = DateTime.Now;
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                expenditure.CreationDate = date;
                expenditure.CreatorId = userId;
                expenditure.ModificationDate = date;
                expenditure.ModifierId = userId;
                expenditure.ConcernId = concernId;
                _context.Expenditures.Add(expenditure);
                _context.SaveChanges();
                transaction.Commit();
            };
        }

        public void AddExpenditureHead(ExpenditureHead expenditureHead, int concernId, string userName, int userId)
        {
            var date = DateTime.Now;
            expenditureHead.CreationDate = date;
            expenditureHead.CreatorId = userId;
            expenditureHead.ModificationDate = date;
            expenditureHead.ModifierId = userId;
            expenditureHead.ConcernId = concernId;
            _context.ExpenditureHeads.Add(expenditureHead);
            _context.SaveChanges();
        }

        public void AddSalaryPayment(SalaryPayment salaryPayment, int concernId, string userName, int userId)
        {
            var date = DateTime.Now;
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                salaryPayment.CreationDate = date;
                salaryPayment.CreatorId = userId;
                salaryPayment.ModificationDate = date;
                salaryPayment.ModifierId = userId;
                salaryPayment.ConcernId = concernId;
                _context.SalaryPayments.Add(salaryPayment);
                _context.SaveChanges();
                transaction.Commit();
            };
        }

        public IEnumerable<ExpenditureHead> ExpenditureHeads(int concernId, string userName, int userId)
        {
            return _context.ExpenditureHeads.Where(x => x.ConcernId == concernId).ToList();
        }

        public IEnumerable<SalaryType> SalaryTypes()
        {
            List<SalaryType> salaries = new List<SalaryType>() {
                new SalaryType{
                    Id=1,
                    Name="Salary"
                },
                new SalaryType{
                    Id=1,
                    Name="Advance Salary"
                },
                new SalaryType{
                    Id=1,
                    Name="Wages"
                },
                new SalaryType{
                    Id=1,
                    Name="Advance Wages"
                }
            };
            return salaries;
        }

        public IEnumerable<TransactionType> TransactionTypes()
        {
            List<TransactionType> transactions = new List<TransactionType>()
            {
                new TransactionType{
                    Id=1,
                    Name="Cash"
                },
                new TransactionType{
                    Id=2,
                    Name="Bank"
                }
            };
            return transactions;
        }
    }
}