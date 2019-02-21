using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.ResponseModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class EventReportService : IEventReport
    {
        private readonly DataContext _context;
        public EventReportService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<ResponseTimeSheet> DailyTimeSheet(string userName, int userId, int orderId, string Date, string culture)
        {
            List<ResponseTimeSheet> responses = new List<ResponseTimeSheet>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_Reporting_EventManagement_DailyTimeSheet @WorkOrderId,@Date,@Culture");
                command.Parameters.Add(new SqlParameter("WorkOrderId", orderId));                
                command.Parameters.Add(new SqlParameter("Date", Date));                
                command.Parameters.Add(new SqlParameter("Culture", culture));                
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            responses.Add(new ResponseTimeSheet()
                            {
                                Serial=Convert.ToInt32(result[0]),
                                OrderCode=Convert.ToString(result[1]),
                                EmployeeId=Convert.ToInt32(result[2]),
                                OrderId=Convert.ToInt32(result[3]),
                                ClientName=Convert.ToString(result[4]),
                                EmployeeName=Convert.ToString(result[5]),
                                Date=Convert.ToString(result[6]),
                                SheetCode=Convert.ToString(result[7]),
                                InTime=Convert.ToString(result[8]),
                                OutTime=Convert.ToString(result[9]),
                                TotalHour=Convert.ToDecimal(result[10]),
                                PerHourRate=Convert.ToDecimal(result[11]),
                                TotalWithoutVat=Convert.ToDecimal(result[12]),
                                VatValue=Convert.ToDecimal(result[13]),
                                VatAmount = Convert.ToDecimal(result[14]),
                                TotalAfterVat=Convert.ToDecimal(result[15])
                            });

                        }
                    }
                }
            }
            return responses;
        }

        public IEnumerable<ResponseTimeSheet> TimeSheetSummary(string userName, int userId, int orderId, string culture)
        {
            List<ResponseTimeSheet> responses = new List<ResponseTimeSheet>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_Reporting_EventManagement_DailyTimeSheetSummary @WorkOrderId,@Culture");
                command.Parameters.Add(new SqlParameter("WorkOrderId", orderId));
                command.Parameters.Add(new SqlParameter("Culture", culture));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            responses.Add(new ResponseTimeSheet()
                            {
                                OrderId = Convert.ToInt32(result[0]),
                                OrderCode = Convert.ToString(result[1]),                                
                                ClientName = Convert.ToString(result[2]),
                                SheetCode = Convert.ToString(result[3]),
                                Date = Convert.ToString(result[4]),                                
                                TotalHour = Convert.ToDecimal(result[5]),
                                PerHourRate = Convert.ToDecimal(result[6]),
                                TotalWithoutVat = Convert.ToDecimal(result[7]),
                                VatAmount = Convert.ToDecimal(result[8]),
                                TotalAfterVat = Convert.ToDecimal(result[9])
                            });

                        }
                    }
                }
            }
            return responses;
        }
    }
}