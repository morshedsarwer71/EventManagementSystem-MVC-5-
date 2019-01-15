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
                            FromDate = workOrderManpower.FromDate,
                            ToDate = workOrderManpower.ToDate
                        });
                    }

                }
                foreach (var item in ManpowerList)
                {
                    _context.WorkOrderChildManpowers.Add(item);
                }
                _context.SaveChanges();
                workOrder.Status = 2;
                _context.SaveChanges();
                transaction.Commit();
            }


        }
    }
}