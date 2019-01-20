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
    public class WorkOrderServiceService: IWorkOrderService
    {
        private readonly DataContext _context;
        public WorkOrderServiceService(DataContext context)
        {
            _context = context;
        }
        public void AddWorkOrderService(WorkOrderChildService workOrderChildService, int workOrderId, int userId, string userName)
        {
            List<WorkOrderChildService> WorkOrderServicelist = new List<WorkOrderChildService>();
            //var WorkOreder = _context.WorkOrderParents.FirstOrDefault(x => x.WorkOrderId == workOrderId);
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                foreach (var item in workOrderChildService.EventService)
                {
                    if (item.IsChecked == true)
                        WorkOrderServicelist.Add(new WorkOrderChildService()
                        {
                            WorkOrderId = workOrderId,
                            EventServiceId = item.EventServiceId
                        });
                }
                _context.WorkOrderChildServices.AddRange(WorkOrderServicelist);
                _context.SaveChanges();
                transaction.Commit();
            }

        }
        public void Delete(int WOCID, int userId, string userName, int concernId)
        {
            var miscell = _context.WorkOrderChildServices.FirstOrDefault(x => x.WOCServiceId == WOCID);
            _context.WorkOrderChildServices.Remove(miscell);
            _context.SaveChanges();
        }

        public IEnumerable<WorkOrderAssigned> WorkOrderAssigneds(int workOrederId, int userId, string userName, int concernId)
        {
            List<WorkOrderAssigned> assigneds = new List<WorkOrderAssigned>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_eventmanagement_workorder_service @concernId,@orderId");
                command.Parameters.Add(new SqlParameter("concernId", concernId));
                command.Parameters.Add(new SqlParameter("orderId", workOrederId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            assigneds.Add(new WorkOrderAssigned()
                            {
                                Serial = Convert.ToInt32(result[0]),
                                OrderId = Convert.ToInt32(result[1]),
                                WOCMid = Convert.ToInt32(result[2]),
                                Name = Convert.ToString(result[3]),
                                OrderCode = Convert.ToString(result[4])
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