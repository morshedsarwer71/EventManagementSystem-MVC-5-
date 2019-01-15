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
            var WorkOreder = _context.WorkOrderParents.FirstOrDefault(x => x.WorkOrderId == workOrderId);
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

                foreach (var item in WorkOrderServicelist)
                {
                    _context.WorkOrderChildServices.Add(item);
                }

                _context.SaveChanges();
                WorkOreder.Status = 2;
                _context.SaveChanges();
                transaction.Commit();
            }

        }
    }
}