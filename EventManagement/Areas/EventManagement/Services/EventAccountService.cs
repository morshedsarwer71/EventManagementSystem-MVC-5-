using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class EventAccountService: IEventAccount
    {
        private DataContext _context;
        public EventAccountService(DataContext context)
        {
            _context = context;
        }

        public void AddLender(Lender lender, int userId, string userName, int concernId)
        {
            var date = DateTime.Now;
            lender.ConcernId = concernId;
            lender.CreatorId = userId;
            lender.CreationDate = date;
            lender.ModificationDate = date;
            lender.ModifierId = userId;
            _context.Lender.Add(lender);
            _context.SaveChanges();
        }

        public void AddLoan(Loan loan, int userId, string userName, int concernId)
        {
            var date = DateTime.Now;
            loan.ConcernId = concernId;
            loan.CreatorId = userId;
            loan.CreationDate = date;
            loan.ModificationDate = date;
            loan.ModifierId = userId;
            _context.Loans.Add(loan);
            _context.SaveChanges();
        }

        public void AddLoanInstallment(LoanInstallment loanInstallment, int userId, string userName, int concernId)
        {
            var date = DateTime.Now;
            loanInstallment.ConcernId = concernId;
            loanInstallment.CreatorId = userId;
            loanInstallment.CreationDate = date;
            loanInstallment.ModificationDate = date;
            loanInstallment.ModifierId = userId;
            _context.LoanInstallments.Add(loanInstallment);
            _context.SaveChanges();
        }

        public Lender Lender(int userId, string userName, int concernId, int id)
        {
            return _context.Lender.FirstOrDefault(x=>x.LenderId==id && x.IsDelete==0);
        }

        public IEnumerable<Lender> Lenders(int userId, string userName, int concernId)
        {
            return _context.Lender.Where(x => x.ConcernId == concernId && x.IsDelete == 0);
        }

        public Loan Loan(int userId, string userName, int concernId, int id)
        {
            return _context.Loans.FirstOrDefault(x => x.LoanId == id && x.IsDelete == 0);
        }

        public LoanInstallment LoanInstallment(int userId, string userName, int concernId, int id)
        {
            return _context.LoanInstallments.FirstOrDefault(x => x.LoanInstallmentId == id && x.IsDelete == 0);
        }

        public IEnumerable<ResponseLoan> Loans(int userId, string userName, int concernId)
        {
            List<ResponseLoan> responses = new List<ResponseLoan>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("EventManagement_Loan @concernId");
                command.Parameters.Add(new SqlParameter("@concernId", concernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if(result.HasRows)
                    {
                        while (result.Read())
                        {
                            responses.Add(new ResponseLoan()
                            {
                                Id= Convert.ToInt32(result[0]),
                                Name= Convert.ToString(result[1]),
                                Date= Convert.ToDateTime(result[2]),
                                LoanPaidDate= Convert.ToDateTime(result[3]),
                                Amont= Convert.ToDecimal(result[4]),
                                NumOfInstallment= Convert.ToInt32(result[5])
                            });
                        }

                    }
                }
                    _context.Database.Connection.Close();
            }
                return responses;
        }

        public IEnumerable<ResponseLoan> LoanInstallments(int userId, string userName, int concernId)
        {
            List<ResponseLoan> responses = new List<ResponseLoan>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("EventManagement_LoanInstallment @concernId");
                command.Parameters.Add(new SqlParameter("@concernId", concernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            responses.Add(new ResponseLoan()
                            {
                                Id = Convert.ToInt32(result[0]),
                                Name = Convert.ToString(result[1]),
                                Date = Convert.ToDateTime(result[2]),
                                Amont = Convert.ToDecimal(result[3])
                            });
                        }

                    }
                }
                _context.Database.Connection.Close();
            }
            return responses;
        }

        public void UpdateLender(Lender lender, int userId, string userName, int concernId, int id)
        {
            var getLender = Lender(userId, userName, concernId,id);
            getLender.Description = lender.Description;
            getLender.LenderAddress = lender.LenderAddress;
            getLender.LenderMobile = lender.LenderMobile;
            getLender.LenderName = lender.LenderName;
            getLender.ModificationDate = DateTime.Now;
            getLender.ModifierId = userId;
            _context.SaveChanges();
        }

        public void UpdateLoan(Loan loan, int userId, string userName, int concernId, int id)
        {
            var getLoan = Loan(userId, userName, concernId,id);
            getLoan.Description = loan.Description;
            getLoan.LenderId = loan.LenderId;
            getLoan.LoanAmount = loan.LoanAmount;
            getLoan.LoanDate = loan.LoanDate;
            getLoan.LoanPaidDate = loan.LoanPaidDate;
            getLoan.ModificationDate = DateTime.Now;
            getLoan.ModifierId = userId;
            getLoan.NumberOfIntallment = loan.NumberOfIntallment;
            _context.SaveChanges();

        }

        public void UpdateLoanInstallment(LoanInstallment loanInstallment, int userId, string userName, int concernId, int id)
        {
            var getIoanInstallment = LoanInstallment(userId, userName, concernId,id);
            getIoanInstallment.Description = loanInstallment.Description;
            getIoanInstallment.LenderId = loanInstallment.LenderId;
            getIoanInstallment.InstallmentAmount = loanInstallment.InstallmentAmount;
            getIoanInstallment.InstallmentDate = loanInstallment.InstallmentDate;
            getIoanInstallment.ModificationDate = DateTime.Now;
            getIoanInstallment.ModifierId = userId;
            _context.SaveChanges();


        }

        public IEnumerable<ResponseLoan> LoanInstallmentsReport(int userId, string userName, int concernId, string fromDate, string toDate,int lenderId)
        {
            List<ResponseLoan> responses = new List<ResponseLoan>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("EventManagement_Installment_Report @fromDate,@toDate,@lenderId,@concernId");                
                command.Parameters.Add(new SqlParameter("@fromDate", fromDate));
                command.Parameters.Add(new SqlParameter("@toDate", toDate));
                command.Parameters.Add(new SqlParameter("@lenderId", lenderId));
                command.Parameters.Add(new SqlParameter("@concernId", concernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            responses.Add(new ResponseLoan()
                            {
                                Name = Convert.ToString(result[0]),                                
                                Amont = Convert.ToDecimal(result[1]),
                                InstallmentDate = Convert.ToString(result[2]),
                                Id= Convert.ToInt32(result[3]),
                            });
                        }

                    }
                }
                _context.Database.Connection.Close();
            }
            return responses;
        }

        public IEnumerable<ResponseLoan> TotalDueLoan(int concernId)
        {
            List<ResponseLoan> responses = new List<ResponseLoan>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("Eventmanagement_LoanDueReport @concernId");
                command.Parameters.Add(new SqlParameter("@concernId", concernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            responses.Add(new ResponseLoan()
                            {
                                Id = Convert.ToInt32(result[0]),
                                Name = Convert.ToString(result[1]),
                                Amont = Convert.ToDecimal(result[2]),
                                PaidAmount = Convert.ToDecimal(result[3]),
                                DueAmount = Convert.ToDecimal(result[4])
                            });
                        }

                    }
                }
                _context.Database.Connection.Close();
            }
            return responses;
        }

        public IEnumerable<ResponsePaymentReport> ClientPaymentReport(int concernId)
        {
            List<ResponsePaymentReport> responses = new List<ResponsePaymentReport>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_ClientWorkPaymentReport @concernId");
                command.Parameters.Add(new SqlParameter("@concernId", concernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            responses.Add(new ResponsePaymentReport() {
                                Id= Convert.ToInt32(result[0]),
                                Name= Convert.ToString(result[1]),
                                TotalWorkBill= Convert.ToDecimal(result[2]),
                                TotalPayment= Convert.ToDecimal(result[3]),
                                Total= Convert.ToDecimal(result[4])
                            });
                        }

                    }
                }
                _context.Database.Connection.Close();
            }
            return responses;
        }

        public IEnumerable<ResponsePaymentReport> WorkerPaymentReport(int concernId)
        {
            List<ResponsePaymentReport> responses = new List<ResponsePaymentReport>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_PaymentReport @concernId");
                command.Parameters.Add(new SqlParameter("@concernId", concernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            responses.Add(new ResponsePaymentReport()
                            {
                                Id = Convert.ToInt32(result[0]),
                                Name = Convert.ToString(result[1]),
                                TotalWorkBill = Convert.ToDecimal(result[2]),
                                TotalPayment = Convert.ToDecimal(result[3]),
                                Total = Convert.ToDecimal(result[4])
                            });
                        }

                    }
                }
                _context.Database.Connection.Close();
            }
            return responses;
        }
    }
}