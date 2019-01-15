using EventManagement.Areas.Accounting.Interfaces;
using EventManagement.Areas.Accounting.ResponseModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.Services
{
    public class ReportService : IReport
    {
        private readonly DataContext _context;
        public ReportService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<BalanceSheet> BalanceSheets(int concernId, string culture)
        {
            List<BalanceSheet> balanceSheets = new List<BalanceSheet>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = "usp_Accounting_BalanceSheet @ConcernId,@Lang";
                command.Parameters.Add(new SqlParameter("@ConcernId", concernId));
                command.Parameters.Add(new SqlParameter("@Lang", culture));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            balanceSheets.Add(new BalanceSheet()
                            {
                                Particulars = Convert.ToString(result[0]),
                                ValueOne = Convert.ToDecimal(result[1]),
                                ValueTwo = Convert.ToDecimal(result[2]),
                                ValueThree = Convert.ToDecimal(result[3]),
                            });
                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return balanceSheets;
        }

        public IEnumerable<IncomeStatement> IncomeStatements(int concernId, string culture)
        {
            List<IncomeStatement> incomeStatements = new List<IncomeStatement>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = "usp_Accounting_IncomeStatement @ConcernId,@Lang";
                command.Parameters.Add(new SqlParameter("@ConcernId", concernId));
                command.Parameters.Add(new SqlParameter("@Lang", culture));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            incomeStatements.Add(new IncomeStatement()
                            {
                                Particulars = Convert.ToString(result[0]),
                                ValueOne = Convert.ToDecimal(result[1]),
                                ValueTwo = Convert.ToDecimal(result[2]),
                                ValueThree = Convert.ToDecimal(result[3]),
                            });
                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return incomeStatements;
        }

        public IEnumerable<ResponseLedger> ResponseLedgers(int headId, string culture, int concernId, string fromDate, string toDate)
        {
            List<ResponseLedger> responses = new List<ResponseLedger>();
            var fdate = "";
            var tdate = "";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(toDate))
            {
                fdate = date;
            }
            else
            {
                fdate = fromDate;
            }
            if (string.IsNullOrEmpty(toDate))
            {
                tdate = date;
            }
            else
            {
                tdate = toDate;
            }
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = "usp_Accounting_GetLedger @AccountHeadId,@Lang,@ConcernId,@fromDate,@toDate";
                command.Parameters.Add(new SqlParameter("@AccountHeadId", headId));
                command.Parameters.Add(new SqlParameter("@Lang", culture));
                command.Parameters.Add(new SqlParameter("@ConcernId", concernId));
                command.Parameters.Add(new SqlParameter("@fromDate", fdate));
                command.Parameters.Add(new SqlParameter("@toDate", tdate));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            responses.Add(new ResponseLedger()
                            {
                                JournalId = Convert.ToInt32(result[0]),
                                AcountHeadName = Convert.ToString(result[1]),
                                DebitJournalAmount = Convert.ToDecimal(result[2]),
                                CreditJournalAmount = Convert.ToDecimal(result[3]),
                                Debit = Convert.ToDecimal(result[4]),
                                Credit = Convert.ToDecimal(result[5]),
                                Date = Convert.ToString(result[6]),
                                VoucherCode = Convert.ToString(result[7])
                            });
                        }
                    }
                }
                _context.Database.Connection.Close();

            }
            return responses;
        }

        public IEnumerable<TrialBalance> TrialBalances(int concernId, string culture)
        {
            List<TrialBalance> trialBalances = new List<TrialBalance>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = "usp_Accounting_TrialBalance @ConcernId,@Lang";
                command.Parameters.Add(new SqlParameter("@ConcernId", concernId));
                command.Parameters.Add(new SqlParameter("@Lang", culture));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            trialBalances.Add(new TrialBalance()
                            {
                                Particulars = Convert.ToString(result[0]),
                                ValueOne = Convert.ToDecimal(result[1]),
                                ValueTwo = Convert.ToDecimal(result[2]),
                            });
                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return trialBalances;
        }
    }
}