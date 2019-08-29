using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class EventManagementClientService: IEventManagementClient
    {
        private readonly DataContext _context;
        public EventManagementClientService(DataContext context)
        {
            _context = context;
        }

        public void Add(EventManagementClient eventManagementClient, string UserName, int UserId, int concernId)
        {
            var dateTime = DateTime.Now;
            eventManagementClient.CreationDate = dateTime;
            eventManagementClient.ModificationDate = dateTime;
            eventManagementClient.CreatorId = UserId;
            eventManagementClient.IsDelete = 0;
            eventManagementClient.ConcernId = concernId;
            _context.EventManagementClients.Add(eventManagementClient);
            _context.SaveChanges();
        }

        public void AddManpowerSupplier(ManpowerSupplier manpowerSupplier, string UserName, int UserId, int concernId)
        {
            manpowerSupplier.ConcernId = concernId;
            manpowerSupplier.CreatorId = UserId;
            manpowerSupplier.ModificationDate = DateTime.Now;
            manpowerSupplier.CreationDate = DateTime.Now;
            manpowerSupplier.ModifierId = UserId;
            _context.ManpowerSuppliers.Add(manpowerSupplier);
            _context.SaveChanges();
        }

        public void UpdateManpowerSupplier(ManpowerSupplier manpowerSupplier, int id, string UserName, int UserId, int concernId)
        {
            var supplier = _context.ManpowerSuppliers.FirstOrDefault(x=>x.ManpowerSupplierId==id);
            supplier.Name = manpowerSupplier.Name;
            supplier.Address = manpowerSupplier.Address;
            supplier.ContactNumber = manpowerSupplier.ContactNumber;
            supplier.Description = manpowerSupplier.Description;
            supplier.ModificationDate = DateTime.Now;
            supplier.ModifierId = UserId;
            _context.SaveChanges();
        }

        public void Delete(int ClientId)
        {
            var Client = EventClientById(ClientId);
            Client.IsDelete = 1;
            _context.SaveChanges();
        }

        public EventManagementClient EventClientById(int ClientId)
        {
            var EventClient = _context.EventManagementClients.FirstOrDefault(x => x.ClientId == ClientId);
            return EventClient;
        }

        public IEnumerable<EventManagementClient> EventClients(int ConcernId)
        {
            var EventClient = _context.EventManagementClients.Where(x => x.ConcernId == ConcernId && x.IsDelete == 0).ToList();
            return EventClient;
        }

        public IEnumerable<ManpowerSupplier> ManpowerSuppliers(int ConcernId)
        {
            var suppliers = _context.ManpowerSuppliers.ToList();
            return suppliers;
        }

        public void Update(int ClientId, EventManagementClient eventManagementClient, string UserName, int UserId)
        {
            var Client = EventClientById(ClientId);
            var dateTime = DateTime.Now;
            eventManagementClient.ModificationDate = dateTime;
            eventManagementClient.ModifierId = UserId;
            Client.AccountHeadId = eventManagementClient.AccountHeadId;
            Client.ClientAddress = eventManagementClient.ClientAddress;
            Client.ClientContactInfo = eventManagementClient.ClientContactInfo;
            Client.ClientNameAR = eventManagementClient.ClientNameAR;
            Client.ClientNameEN = eventManagementClient.ClientNameEN;
            Client.TaxRegNumber = eventManagementClient.TaxRegNumber;
            _context.SaveChanges();
        }
    }
}