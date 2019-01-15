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
    public class JournalService : IJournal
    {
        private readonly DataContext _context;
        public JournalService(DataContext context)
        {
            _context = context;
        }
        public void AddJournal(Journal journal, int userId, int concernId)
        {
            journal.JournalModificationDate = DateTime.Now;
            journal.JournalModifierId = userId;
            journal.ConcernId = concernId;
            journal.JournalCreationDate = DateTime.Now;
            journal.JournalCreator = userId;
            journal.IsDelete = 0;
            _context.Journals.Add(journal);
            _context.SaveChanges();
        }

        public IEnumerable<ResponseJournal> ResponseJournals(string culture, int concenId,int page)
        {
            List<ResponseJournal> responses = new List<ResponseJournal>();
            using (var command=_context.Database.Connection.CreateCommand())
            {
                command.CommandText = "usp_Accounting_GetJournalsIndexByLang @lang,@PageNumber,@concernId";
                command.Parameters.Add(new SqlParameter("@lang",culture));
                command.Parameters.Add(new SqlParameter("@PageNumber", page));
                command.Parameters.Add(new SqlParameter("@concernId", concenId));
                _context.Database.Connection.Open();
                using (var result=command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            responses.Add(new ResponseJournal()
                            {
                                Row=Convert.ToInt32(result[0]),
                                JournalId=Convert.ToInt32(result[1]),
                                CreditAccountHead=Convert.ToString(result[2]),
                                CreditAmount=Convert.ToDecimal(result[3]),
                                DebitAccountHead=Convert.ToString(result[4]),
                                DebitAmount=Convert.ToDecimal(result[5]),
                                Description=Convert.ToString(result[6]),
                                Date=Convert.ToString(result[7]),
                                Creator=Convert.ToString(result[9]),
                                VoucherCode=Convert.ToString(result[10]),
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