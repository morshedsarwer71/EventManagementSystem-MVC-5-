using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class WorkOrderParentService: IWorkOrderParent
    {
        private readonly DataContext _context;
        public WorkOrderParentService(DataContext context)
        {
            _context = context;
        }        

        public void AddWorkOrder(WorkOrderParent workOrder, string UserName, int UserId, int concernId)
        {
            workOrder.CreationDate = DateTime.Now;
            workOrder.CreatorId = UserId;
            workOrder.ModificationDate = DateTime.Now;
            workOrder.ModifierId = UserId;
            workOrder.ConcernId = concernId;
            _context.WorkOrderParents.Add(workOrder);
            _context.SaveChanges();
        }

        public void Delete(int id, string UserName, int UserId)
        {
            var workOrder = workOrderById(id, UserName, UserId);
            var dateTime = DateTime.Now;
            workOrder.ModificationDate = dateTime;
            workOrder.ModifierId = UserId;
            workOrder.IsDelete = 1;
            _context.SaveChanges();
        }

        public IEnumerable<ResponseWorkOrderParent> GetCompletedWorkOrders(string culture, string UserName, int UserId, int? ConcernId)
        {
            var defaultValue = "";
            if (culture != null)
            {
                defaultValue = culture;
            }
            else { defaultValue = "en-US"; }
            List<ResponseWorkOrderParent> ResponseWorkOrder = new List<ResponseModels.ResponseWorkOrderParent>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_GetWorkOrdersCompletedIndexByLang @lang,@ConcernId");
                command.Parameters.Add(new SqlParameter("lang", defaultValue));
                command.Parameters.Add(new SqlParameter("ConcernId", ConcernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ResponseWorkOrderParent responseWorkOrder = new ResponseModels.ResponseWorkOrderParent();
                            responseWorkOrder.OrderId = Convert.ToInt32(result[0]);
                            responseWorkOrder.WorkOrderCode = Convert.ToString(result[1]);
                            responseWorkOrder.ClientName = Convert.ToString(result[2]);
                            responseWorkOrder.StartingDate = Convert.ToString(result[3]);
                            responseWorkOrder.EndingDate = Convert.ToString(result[4]);
                            responseWorkOrder.NumberOfManpower = Convert.ToInt32(result[5]);
                            responseWorkOrder.PerHourRate = Convert.ToDecimal(result[6]);
                            //responseWorkOrder.Status = Convert.ToString(result[7]);
                            responseWorkOrder.Description = Convert.ToString(result[8]);
                            responseWorkOrder.SerialNumber = Convert.ToInt32(result[10]);
                            responseWorkOrder.Notes = Convert.ToString(result[11]);
                            responseWorkOrder.NoOfPax = Convert.ToInt32(result[12]);
                            responseWorkOrder.NoOfService = Convert.ToInt32(result[13]);
                            responseWorkOrder.NoOfSetup = Convert.ToInt32(result[14]);
                            responseWorkOrder.TotalDays = Convert.ToString(result[15]);

                            ResponseWorkOrder.Add(responseWorkOrder);

                        }
                    }
                }
                _context.Database.Connection.Open();
            }
            return ResponseWorkOrder;
        }

        public IEnumerable<ResponseWorkOrderParent> GetUpcomingWorkOrders(string culture, string UserName, int UserId, int? ConcernId)
        {
            var defaultValue = "";
            if (culture != null)
            {
                defaultValue = culture;
            }
            else { defaultValue = "en-US"; }
            List<ResponseWorkOrderParent> ResponseWorkOrder = new List<ResponseModels.ResponseWorkOrderParent>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_GetWorkOrdersUpcomingIndexByLang @lang,@ConcernId");
                command.Parameters.Add(new SqlParameter("lang", defaultValue));
                command.Parameters.Add(new SqlParameter("ConcernId", ConcernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ResponseWorkOrderParent responseWorkOrder = new ResponseModels.ResponseWorkOrderParent();
                            responseWorkOrder.OrderId = Convert.ToInt32(result[0]);
                            responseWorkOrder.WorkOrderCode = Convert.ToString(result[1]);
                            responseWorkOrder.ClientName = Convert.ToString(result[2]);
                            responseWorkOrder.StartingDate = Convert.ToString(result[3]);
                            responseWorkOrder.EndingDate = Convert.ToString(result[4]);
                            responseWorkOrder.NumberOfManpower = Convert.ToInt32(result[5]);
                            responseWorkOrder.PerHourRate = Convert.ToDecimal(result[6]);
                            responseWorkOrder.Status = Convert.ToString(result[7]);
                            responseWorkOrder.Description = Convert.ToString(result[8]);
                            responseWorkOrder.SerialNumber = Convert.ToInt32(result[10]);
                            responseWorkOrder.Notes = Convert.ToString(result[11]);
                            responseWorkOrder.NoOfPax = Convert.ToInt32(result[12]);
                            responseWorkOrder.NoOfService = Convert.ToInt32(result[13]);
                            responseWorkOrder.NoOfSetup = Convert.ToInt32(result[14]);
                            responseWorkOrder.TotalDays = Convert.ToString(result[15]);
                            responseWorkOrder.NumberOfRows = Convert.ToInt32(result[16]);

                            ResponseWorkOrder.Add(responseWorkOrder);

                        }
                    }
                }
            }
            return ResponseWorkOrder;
        }

        public void Update(string UserName, int UserId, WorkOrderParent workOrder, int id)
        {
            var work = workOrderById(id, UserName, UserId);
            work.ClientId = workOrder.ClientId;
            work.ModifierId = UserId;
            work.ModificationDate = DateTime.Now;
            work.Description = workOrder.Description;
            work.StartingDate = workOrder.StartingDate;
            work.EndingDate = workOrder.EndingDate;
            work.NoOfManpower = workOrder.NoOfManpower;
            work.PerHourRate = workOrder.PerHourRate;
            work.NoOfService = workOrder.NoOfService;
            work.NoOfSetup = workOrder.NoOfSetup;
            work.NoOfPax = workOrder.NoOfPax;
            work.Notes = workOrder.Notes;
            work.Status = workOrder.Status;
            work.VATCode = workOrder.VATCode;
            _context.SaveChanges();
        }

        public Models.WorkOrderParent workOrderById(int id, string UserName, int UserId)
        {
            var getWorkOrderbyId = _context.WorkOrderParents.FirstOrDefault(w => w.WorkOrderId == id && w.IsDelete == 0);
            return (getWorkOrderbyId);
        }

        public IEnumerable<ResponseWorkOrderParent> WorkOrderDetailsById(string culture, string userName, int UserId, int OrderId)
        {
            var defaultValue = "";
            if (culture != null)
            {
                defaultValue = culture;
            }
            else { defaultValue = "en-US"; }
            List<ResponseWorkOrderParent> ResponseWorkOrder = new List<ResponseModels.ResponseWorkOrderParent>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_GetWorkOrderById @lang,@WorkOderId");
                command.Parameters.Add(new SqlParameter("lang", defaultValue));
                command.Parameters.Add(new SqlParameter("WorkOderId", OrderId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())

                        {
                            ResponseWorkOrderParent responseWorkOrder = new ResponseModels.ResponseWorkOrderParent();
                            responseWorkOrder.OrderId = Convert.ToInt32(result[0]);
                            responseWorkOrder.WorkOrderCode = Convert.ToString(result[1]);
                            responseWorkOrder.ClientName = Convert.ToString(result[2]);
                            responseWorkOrder.StartingDate = Convert.ToString(result[3]);
                            responseWorkOrder.EndingDate = Convert.ToString(result[4]);
                            responseWorkOrder.NumberOfManpower = Convert.ToInt32(result[5]);
                            responseWorkOrder.PerHourRate = Convert.ToDecimal(result[6]);
                            responseWorkOrder.Status = Convert.ToString(result[7]);
                            responseWorkOrder.CreatorName = Convert.ToString(result[8]);
                            responseWorkOrder.CreationDate = Convert.ToDateTime(result[9]);
                            responseWorkOrder.ModifierName = Convert.ToString(result[10]);
                            responseWorkOrder.ModificationDate = Convert.ToDateTime(result[11]);
                            responseWorkOrder.Description = Convert.ToString(result[12]);
                            responseWorkOrder.Notes = Convert.ToString(result[13]);
                            responseWorkOrder.NoOfPax = Convert.ToInt32(result[14]);
                            responseWorkOrder.NoOfService = Convert.ToInt32(result[15]);
                            responseWorkOrder.NoOfSetup = Convert.ToInt32(result[16]);

                            ResponseWorkOrder.Add(responseWorkOrder);

                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return (ResponseWorkOrder);
        }

        IEnumerable<ResponseWorkOrderParent> IWorkOrderParent.GetWorkOrders(string culture, string UserName, int UserId, int? ConcernId)
        {
            var defaultValue = "";
            if (culture != null)
            {
                defaultValue = culture;
            }
            else { defaultValue = "en-US"; }
            List<ResponseWorkOrderParent> ResponseWorkOrder = new List<ResponseModels.ResponseWorkOrderParent>();
            using (var command = _context.Database.Connection.CreateCommand())
            {
                command.CommandText = ("usp_EventManagement_GetWorkOrdersIndexByLang @lang,@ConcernId");
                command.Parameters.Add(new SqlParameter("lang", defaultValue));
                command.Parameters.Add(new SqlParameter("ConcernId", ConcernId));
                _context.Database.Connection.Open();
                using (var result = command.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        while (result.Read())
                        {
                            ResponseWorkOrderParent responseWorkOrder = new ResponseModels.ResponseWorkOrderParent();
                            responseWorkOrder.SerialNumber = Convert.ToInt32(result[0]);
                            responseWorkOrder.OrderId = Convert.ToInt32(result[1]);
                            responseWorkOrder.WorkOrderCode = Convert.ToString(result[2]);
                            responseWorkOrder.ClientName = Convert.ToString(result[3]);
                            responseWorkOrder.StartingDate = Convert.ToString(result[4]);
                            responseWorkOrder.EndingDate = Convert.ToString(result[5]);
                            responseWorkOrder.NumberOfManpower = Convert.ToInt32(result[6]);
                            responseWorkOrder.PerHourRate = Convert.ToDecimal(result[7]);
                            responseWorkOrder.Status = Convert.ToString(result[8]);
                            responseWorkOrder.Description = Convert.ToString(result[9]);                            
                            responseWorkOrder.Notes = Convert.ToString(result[10]);
                            responseWorkOrder.NoOfPax = Convert.ToInt32(result[11]);
                            responseWorkOrder.NoOfService = Convert.ToInt32(result[12]);
                            responseWorkOrder.NoOfSetup = Convert.ToInt32(result[13]);
                            responseWorkOrder.TotalDays = Convert.ToString(result[14]);
                            responseWorkOrder.NumberOfRows = Convert.ToInt32(result[15]);

                            ResponseWorkOrder.Add(responseWorkOrder);

                        }
                    }
                }
                _context.Database.Connection.Close();
            }
            return ResponseWorkOrder;
        }
    }
}