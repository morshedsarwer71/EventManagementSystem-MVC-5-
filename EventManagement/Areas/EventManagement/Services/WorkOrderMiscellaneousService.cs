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
    public class WorkOrderMiscellaneousService: IWorkOrderMiscellaneous
    {
        private readonly DataContext _context;
        public WorkOrderMiscellaneousService(DataContext context)
        {
            _context = context;
        }
        public void AddWorkOrderMiscellaneous(int workOrederId, WorkOrderChildMiscellaneous workOrderChildMiscellaneous, int userId, string userName)
        {
            List<WorkOrderChildMiscellaneous> MiscellaneousList = new List<WorkOrderChildMiscellaneous>();
            //var WorkOrder = _context.WorkOrderParents.FirstOrDefault(x => x.WorkOrderId == workOrederId);
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                foreach (var item in workOrderChildMiscellaneous.EventMiscellaneous)
                {
                    if (item.IsChecked == true)
                    {
                        MiscellaneousList.Add(new WorkOrderChildMiscellaneous()
                        {
                            WorkOrderId = workOrederId,
                            EventMiscellaneousId = item.EventMiscellaneousId
                        });
                    }
                }
                _context.WorkOrderChildMiscellaneouses.AddRange(MiscellaneousList);
                _context.SaveChanges();
                transaction.Commit();
            }
        }

        public void Delete(int WOCID, int userId, string userName, int concernId)
        {
            var miscell = _context.WorkOrderChildMiscellaneouses.FirstOrDefault(x=>x.WOCMiscellaneousId==WOCID);
            _context.WorkOrderChildMiscellaneouses.Remove(miscell);
            _context.SaveChanges();
        }

        public IEnumerable<WorkOrderAssigned> WorkOrderAssigneds(int workOrederId, int userId, string userName,int concernId)
        {
            List<WorkOrderAssigned> assigneds = new List<WorkOrderAssigned>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_eventmanagement_workorder_miscellaneous @concernId,@orderId");
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