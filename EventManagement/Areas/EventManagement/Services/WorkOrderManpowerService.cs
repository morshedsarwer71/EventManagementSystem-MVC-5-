using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
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
    public class WorkOrderManpowerService: IWorkOrderManpower
    {
        private readonly DataContext _context;
        public WorkOrderManpowerService(DataContext context)
        {
            _context = context;
        }
        public void AddWorkOrderManpower(WorkOrderChildManpower workOrderManpower, int workOrderId, int userId, string userName)
        {
            var workOrder = _context.WorkOrderParents.FirstOrDefault(x => x.WorkOrderId == workOrderId);
            List<WorkOrderChildManpower> ManpowerList = new List<WorkOrderChildManpower>();

            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                foreach (var item in workOrderManpower.Employee)
                {
                    if (item.IsChecked == true)
                    {
                        ManpowerList.Add(new WorkOrderChildManpower()
                        {
                            WorkOrderId = workOrderId,
                            EmployeeId = item.EmployeeId,
                            FromDate = workOrder.StartingDate,
                            ToDate = workOrder.EndingDate
                        });
                    }

                }
                _context.WorkOrderChildManpowers.AddRange(ManpowerList);
                _context.SaveChanges();
                transaction.Commit();
            }


        }

        public void Delete(int WOCMID, int userId, string userName, int concernId)
        {
            var manpower = _context.WorkOrderChildManpowers.FirstOrDefault(x=>x.WOCManpowerId==WOCMID);
            _context.WorkOrderChildManpowers.Remove(manpower);
            _context.SaveChanges();
        }

        public IEnumerable<WorkOrderAssigned> WorkOrderAssigneds(int workOrderId, int userId, string userName, int concernId)
        {
            List<WorkOrderAssigned> assigneds = new List<WorkOrderAssigned>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_AssignedMenpower @ConcerId,@OrderId");
                command.Parameters.Add(new SqlParameter("ConcerId", concernId));
                command.Parameters.Add(new SqlParameter("OrderId", workOrderId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            assigneds.Add(new WorkOrderAssigned()
                            {
                                Serial=Convert.ToInt32(result[0]),
                                Name=Convert.ToString(result[1]),
                                FromDate=Convert.ToDateTime(result[2]),
                                Todate=Convert.ToDateTime(result[3]),
                                OrderId=Convert.ToInt32(result[4]),
                                OrderCode=Convert.ToString(result[5]),
                                DateOfBirth=Convert.ToDateTime(result[6]),
                                Nationality=Convert.ToString(result[7]),
                                NIDNumber=Convert.ToString(result[8]),
                                PassNumber=Convert.ToString(result[9]),
                                ImagePath=Convert.ToString(result[10]),
                                PassPath=Convert.ToString(result[11]),
                                WOCMid=Convert.ToInt32(result[12])
                            });
                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return assigneds;
        }
    }
}