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
                                OrderCode = Convert.ToString(result[1]),
                                EmployeeId = Convert.ToInt32(result[2]),
                                OrderId= Convert.ToInt32(result[3]),
                                PerHourRate= Convert.ToDecimal(result[4]),
                                VatValue= Convert.ToDecimal(result[5]),
                                VatNumber= Convert.ToString(result[6]),
                                ClientName= Convert.ToString(result[7]),
                                SheetCode= Convert.ToString(result[8]),
                                EmployeeName= Convert.ToString(result[9]),
                                Date= Convert.ToString(result[10]),
                                InTimeDate= Convert.ToString(result[11]),
                                InTime= Convert.ToString(result[12]),
                                OutTimeDate= Convert.ToString(result[13]),
                                OutTime= Convert.ToString(result[14]),
                                TotalHour= Convert.ToDecimal(result[15]),
                                TotalWithoutVat= Convert.ToDecimal(result[16]),
                                VatAmount= Convert.ToDecimal(result[17]),
                                TotalAfterVat= Convert.ToDecimal(result[18]),
                                ConcernName = Convert.ToString(result[19]),
                                ClientTaxNumber = Convert.ToString(result[20]),
                                VatName = Convert.ToString(result[21]),
                                TotalBeforeVat=Convert.ToDecimal(result[22])
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