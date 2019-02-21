using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ReportObject;
using EventManagement.Areas.EventManagement.ResponseModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
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

        public void AddClientPayment(ClientPayment clientPayment, int concernId, string userName, int userId)
        {
            var date = DateTime.Now;
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                clientPayment.CreationDate = date;
                clientPayment.CreatorId = userId;
                clientPayment.ModificationDate = date;
                clientPayment.ModifierId = userId;
                clientPayment.ConcernId = concernId;
                _context.ClientPayments.Add(clientPayment);
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

        public IEnumerable<Bank> Banks(int concernId, string userName, int userId)
        {
            var bank = _context.Banks.Where(x=>x.ConcernId== concernId).ToList();
            return bank;
        }

        public ClientPayment ClientPaymentById(int id, int concernId, string userName, int userId)
        {
            return _context.ClientPayments.FirstOrDefault(x=>x.ClientPaymentId==id);
        }

        public IEnumerable<ResponseClientPayment> ClientPayments(int concernId, string userName, int userId, string culture)
        {
            List<ResponseClientPayment> payments = new List<ResponseClientPayment>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_ClientPaymentIndexByConcernId @concernId,@culture");
                command.Parameters.Add(new SqlParameter("@concernId", concernId));
                command.Parameters.Add(new SqlParameter("@culture", culture));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            payments.Add(new ResponseClientPayment()
                            {
                                Serial = Convert.ToInt32(result[0]),
                                PaymentId = Convert.ToInt32(result[1]),
                                ClientName = Convert.ToString(result[2]),
                                Date = Convert.ToString(result[3]),
                                PaymentStatus = Convert.ToString(result[4]),
                                Amount = Convert.ToDecimal(result[5]),
                                PayeesName = Convert.ToString(result[6]),
                                Notes = Convert.ToString(result[7]),
                                Description = Convert.ToString(result[8])
                            });
                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return payments;
        }

        public IEnumerable<EventManagementClient> Clients(int concernId, string userName, int userId)
        {
            var client = _context.EventManagementClients.Where(x => x.ConcernId == concernId).ToList();
            return client;
        }

        public IEnumerable<ExpenditureHead> ExpenditureHeads(int concernId, string userName, int userId)
        {
            return _context.ExpenditureHeads.Where(x => x.ConcernId == concernId).ToList();
        }

        public IEnumerable<ResponseExpenditure> ReportExpenditures(int concernId, ExpenditureReport expenditure, string userName, int userId)
        {
            List<ResponseExpenditure> payments = new List<ResponseExpenditure>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_eventmanagement_TransactionReport @concernId,@fromDate,@toDate,@expenditureHead,@transactionType");
                command.Parameters.Add(new SqlParameter("concernId", concernId));
                command.Parameters.Add(new SqlParameter("fromDate", expenditure.FromDate));
                command.Parameters.Add(new SqlParameter("toDate", expenditure.ToDate));
                command.Parameters.Add(new SqlParameter("expenditureHead", expenditure.Expenditure));
                command.Parameters.Add(new SqlParameter("transactionType", expenditure.TransType));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            payments.Add(new ResponseExpenditure()
                            {
                                Serial = Convert.ToInt32(result[0]),
                                ExpenditureHead = Convert.ToString(result[3]),
                                Amount = Convert.ToDecimal(result[4]),
                                ExpenditureDate = Convert.ToString(result[5]),
                                TransactionType = Convert.ToString(result[6]),
                                Description = Convert.ToString(result[7])
                            });
                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return payments;
        }

        public IEnumerable<ResponseExpenditure> ResponseExpenditures(int concernId, string userName, int userId)
        {
            List<ResponseExpenditure> payments = new List<ResponseExpenditure>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_eventmanagement_getExpenditures @concernId");
                command.Parameters.Add(new SqlParameter("concernId", concernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            payments.Add(new ResponseExpenditure()
                            {
                                Serial=Convert.ToInt32(result[0]),
                                ExpenditureHead= Convert.ToString(result[1]),
                                Amount= Convert.ToDecimal(result[2]),
                                ExpenditureDate= Convert.ToString(result[3]),
                                TransactionType= Convert.ToString(result[4]),
                                Description= Convert.ToString(result[5]),
                                Rows= Convert.ToInt32(result[6])
                            });
                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return payments;
        }

        public IEnumerable<ResponseSalaryPayments> ResponseSalaries(int concernId, string userName, int userId)
        {
            List<ResponseSalaryPayments> payments = new List<ResponseSalaryPayments>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_eventmanagement_getsalarypayments @concernId");                
                command.Parameters.Add(new SqlParameter("concernId", concernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            payments.Add(new ResponseSalaryPayments() {
                                Serial=Convert.ToInt32(result[0]),
                                SalaryPaymentId=Convert.ToInt32(result[1]),
                                EmployeeId=Convert.ToInt32(result[2]),
                                Employee=Convert.ToString(result[3]),
                                SalaryType= Convert.ToString(result[4]),
                                Amount = Convert.ToDecimal(result[5]),
                                TransactionType = Convert.ToString(result[6]),
                                PaymentDate = Convert.ToString(result[7]),
                                SalaryMonth = Convert.ToString(result[8]),
                                Rows = Convert.ToInt32(result[9])
                            });
                        }
                    }
                }
                _context.Database.Connection.Close();
            }

            return payments;
        }

        public IEnumerable<ResponseSalaryPayments> ResponseSalariesRepor(int concernId, string userName, int userId, string fromDate, string toDate, int employeeId, int transTypeId)
        {
            List<ResponseSalaryPayments> payments = new List<ResponseSalaryPayments>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_eventmanagement_GetsalaryPaymentsReport @concernId,@fromDate,@toDate,@employeeId,@transType");
                command.Parameters.Add(new SqlParameter("concernId", concernId));
                command.Parameters.Add(new SqlParameter("fromDate", fromDate));
                command.Parameters.Add(new SqlParameter("toDate", toDate));
                command.Parameters.Add(new SqlParameter("employeeId", employeeId));
                command.Parameters.Add(new SqlParameter("transType", transTypeId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            var sal=new ResponseSalaryPayments();
                            sal.Serial = Convert.ToInt32(result[0]);
                            sal.Employee = Convert.ToString(result[4]);
                            sal.SalaryType = Convert.ToString(result[5]);
                            sal.Amount = Convert.ToDecimal(result[6]);
                            sal.TransactionType = Convert.ToString(result[7]);
                            sal.PaymentDate = Convert.ToString(result[8]);
                            sal.SalaryMonth = Convert.ToString(result[9]);
                            sal.Description = Convert.ToString(result[10]);
                            payments.Add(sal);
                        }
                    }
                }
                _context.Database.Connection.Close();
            }

            return payments;
        }

        public IEnumerable<SalaryType> SalaryTypes()
        {
            List<SalaryType> salaries = new List<SalaryType>() {
                new SalaryType{
                    Id=1,
                    Name="Salary"
                },
                new SalaryType{
                    Id=2,
                    Name="Advance Salary"
                },
                new SalaryType{
                    Id=3,
                    Name="Wages"
                },
                new SalaryType{
                    Id=4,
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

        public void UpdateClientPayment(ClientPayment clientPayment, int id, int concernId, string userName, int userId)
        {
            var client = _context.ClientPayments.FirstOrDefault(x=>x.ClientPaymentId==id);
            client.ClientId = clientPayment.ClientId;
            client.PaymentDate = clientPayment.PaymentDate;
            client.PaymentAmount = clientPayment.PaymentAmount;
            client.PaymentType = clientPayment.PaymentType;
            client.BankId = clientPayment.BankId;
            client.PayeesName = clientPayment.PayeesName;
            client.Notes = clientPayment.Notes;
            client.Description = clientPayment.Description;
            client.ModificationDate = DateTime.Now;
            client.ModifierId = userId;
            _context.SaveChanges();
        }
    }
}