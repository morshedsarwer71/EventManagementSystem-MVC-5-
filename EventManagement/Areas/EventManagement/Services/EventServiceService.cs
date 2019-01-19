using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class EventServiceService: IEventService
    {
        private readonly DataContext _context;
        public EventServiceService(DataContext context)
        {
            _context = context;
        }
        public void AddEventService(EventService eventService, int userId, string userName, int ConcernId)
        {
            eventService.CreationDate = DateTime.Now;
            eventService.CreatorId = userId;
            eventService.ConcernId = ConcernId;
            eventService.ModificationDate = DateTime.Now;
            eventService.ModifierId = userId;
            _context.EventServices.Add(eventService);
            _context.SaveChanges();
        }

        public void Delete(int userId, string userName, int ServiceId)
        {
            var eventService = EventServiceById(userId, userName, ServiceId);
            eventService.IsDelete = 1;
            eventService.ModifierId = userId;
            eventService.ModificationDate = DateTime.Now;
            _context.SaveChanges();
        }

        public EventService EventServiceById(int userId, string userName, int ServiceId)
        {
            var eventService = _context.EventServices.FirstOrDefault(x => x.EventServiceId == ServiceId && x.IsDelete == 0);
            return eventService;
        }

        public IEnumerable<EventService> EventServices(int userId, string userName, int ConcernId)
        {
            var EventService = _context.EventServices.Where(x => x.ConcernId == ConcernId && x.IsDelete == 0).ToList();
            return EventService;
        }

        public void Update(EventService eventService, int userId, string userName, int ServiceId)
        {
            var Service = EventServiceById(userId, userName, ServiceId);
            Service.EventServiceEN = eventService.EventServiceEN;
            Service.EventServiceAR = eventService.EventServiceAR;
            Service.ModifierId = userId;
            Service.ModificationDate = DateTime.Now;
            _context.SaveChanges();
        }
    }
}