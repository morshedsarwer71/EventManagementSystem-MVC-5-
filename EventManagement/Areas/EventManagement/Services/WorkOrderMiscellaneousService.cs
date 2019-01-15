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
    public class WorkOrderMiscellaneousService: IWorkOrderMiscellaneous
    {
        private readonly DataContext _context;
        public WorkOrderMiscellaneousService(DataContext context)
        {
            _context = context;
        }
        public void AddWorkOrderMiscellaneous(int workOrederId, WorkOrderChildMiscellaneous workOrderChildMiscellaneous)
        {
            List<WorkOrderChildMiscellaneous> MiscellaneousList = new List<WorkOrderChildMiscellaneous>();
            var WorkOrder = _context.WorkOrderParents.FirstOrDefault(x => x.WorkOrderId == workOrederId);
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

                foreach (var item in MiscellaneousList)
                {
                    _context.WorkOrderChildMiscellaneouses.Add(item);
                }

                _context.SaveChanges();

                WorkOrder.Status = 2;
                _context.SaveChanges();

                transaction.Commit();
            }
        }
    }
}