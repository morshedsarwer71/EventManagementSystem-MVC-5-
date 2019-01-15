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
            eventManagementClient.CreatorId = UserId;
            eventManagementClient.IsDelete = 0;
            eventManagementClient.ConcernId = concernId;
            _context.EventManagementClients.Add(eventManagementClient);
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

        public void Update(int ClientId, EventManagementClient eventManagementClient)
        {
            var Client = EventClientById(ClientId);
            Client.AccountHeadId = eventManagementClient.AccountHeadId;
            Client.ClientAddress = eventManagementClient.ClientAddress;
            Client.ClientContactInfo = eventManagementClient.ClientContactInfo;
            Client.ClientNameAR = eventManagementClient.ClientNameAR;
            Client.ClientNameEN = eventManagementClient.ClientNameEN;
            _context.SaveChanges();
        }
    }
}