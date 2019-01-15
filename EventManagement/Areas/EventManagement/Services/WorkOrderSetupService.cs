using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class WorkOrderSetupService: IWorkOrderSetup
    {
        private readonly DataContext _context;
        public WorkOrderSetupService(DataContext context)
        {
            _context = context;
        }
        public void AddWorkOrderSetup(WorkOrderChildSetup workOrderChildSetup, int workOrederId, int userId, string userName)
        {
            var orderId = workOrederId;
            var workOrder = _context.WorkOrderParents.FirstOrDefault(x => x.WorkOrderId == workOrederId);

            List<WorkOrderChildSetup> SetupList = new List<WorkOrderChildSetup>();
            using (DbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                foreach (var item in workOrderChildSetup.EventSetup)
                {
                    if (item.IsChecked == true)
                    {
                        SetupList.Add(new WorkOrderChildSetup()
                        {
                            WorkOrderId = orderId,
                            EventSetupId = item.EventSetupId
                        });
                    }
                }
                foreach (var item in SetupList)
                {
                    _context.WorkOrderChildSetups.Add(item);
                }
                _context.SaveChanges();

                workOrder.Status = 2;
                _context.SaveChanges();
                transaction.Commit();
            }
        }
    }
}