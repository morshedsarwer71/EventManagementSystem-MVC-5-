using EventManagement.Areas.Accounting.Interfaces;
using EventManagement.Areas.Accounting.Models;
using EventManagement.Areas.Accounting.ResponseModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.Services
{
    public class SettingsServices : ISettings
    {
        private readonly DataContext _context;
        public SettingsServices(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountsHead> AccountsHeads(int concernId)
        {
            return _context.AccountsHeads.Where(m=>m.ConcernId==concernId);
        }

        public void AddAccountHead(AccountsHead accountsHead, int userId, int concernId)
        {
            accountsHead.CreationDate = DateTime.Now;
            accountsHead.IsDelete = 0;
            accountsHead.CreatorId = userId;
            accountsHead.ConcernId = concernId;
            accountsHead.ModificationDate = DateTime.Now;
            accountsHead.ModifierId = userId;
            _context.AccountsHeads.Add(accountsHead);
            _context.SaveChanges();
        }

        public void AddReportHead(ReportHead reportHead, int userId, int concernId)
        {
            reportHead.CreationDate = DateTime.Now;
            reportHead.IsDelete = 0;
            reportHead.CreatorId = userId;
            reportHead.ConcernId = concernId;
            reportHead.ModificationDate = DateTime.Now;
            reportHead.ModifierId = userId;
            _context.ReportHeads.Add(reportHead);
            _context.SaveChanges();
        }

        public IEnumerable<ReportHead> ReportHeads(int concernId)
        {
            return _context.ReportHeads.Where(m=>m.ConcernId==concernId);
        }

        public IEnumerable<ResponseAccountHeads> ResponseAccountHeads(string culture, int page, int concernId)
        {
            List<ResponseAccountHeads> accountHeads = new List<ResponseAccountHeads>();
            using (var command=_context.Database.Connection.CreateCommand())
            {
                command.CommandText = "usp_Accounting_GetAccountsHeadIndexBylang @lang,@PageNumber,@concernId";
                command.Parameters.Add(new SqlParameter("@lang",culture));
                command.Parameters.Add(new SqlParameter("@PageNumber", page));
                command.Parameters.Add(new SqlParameter("@concernId", concernId));
                _context.Database.Connection.Open();
                using (var result=command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            accountHeads.Add(new ResponseAccountHeads()
                            {
                                Serial=Convert.ToInt32(result[0]),
                                AccountHeadId=Convert.ToInt32(result[1]),
                                AccountHeadName=Convert.ToString(result[2]),
                                TransType=Convert.ToString(result[3]),
                                ReportHeadName=Convert.ToString(result[4]),
                                Rows=Convert.ToInt32(result[7])
                            });
                        }
                    }
                }
                    _context.Database.Connection.Close();
            }
                return accountHeads;
        }
    }
}