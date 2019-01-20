using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class EventMiscellaneousService: IEventMiscellaneous
    {
        private readonly DataContext _context;
        public EventMiscellaneousService(DataContext context)
        {
            _context = context;
        }
        public void AddEventMiscellaneous(EventMiscellaneous eventMiscellaneous, int userId, string userName, int ConcernId)
        {
            eventMiscellaneous.CreationDate = DateTime.Now;
            eventMiscellaneous.CreatorId = userId;
            eventMiscellaneous.ConcernId = ConcernId;
            eventMiscellaneous.ModificationDate = DateTime.Now;
            eventMiscellaneous.ModifierId = userId;
            _context.EventMiscellaneouses.Add(eventMiscellaneous);
            _context.SaveChanges();
        }

        public void Delete(int userId, string userName, int EventId)
        {
            var Event = EventMiscellaneousById(userId, userName, EventId);
            Event.IsDelete = 1;
            Event.ModificationDate = DateTime.Now;
            Event.ModifierId = userId;
            _context.SaveChanges();
        }

        public EventMiscellaneous EventMiscellaneousById(int userId, string userName, int EventId)
        {
            var Event = _context.EventMiscellaneouses.FirstOrDefault(x => x.EventMiscellaneousId == EventId && x.IsDelete == 0);
            return Event;
        }

        public IEnumerable<EventMiscellaneous> EventMiscellaneouses(int userId, string userName, int ConcernId)
        {
            var events = _context.EventMiscellaneouses.Where(x => x.ConcernId == ConcernId && x.IsDelete == 0).ToList();
            return events;
        }

        public void Update(EventMiscellaneous eventMiscellaneous, int userId, string userName, int EventId)
        {
            var Event = EventMiscellaneousById(userId, userName, EventId);
            Event.EventMiscellaneousNameEN = eventMiscellaneous.EventMiscellaneousNameEN;
            Event.EventMiscellaneousNameAR = eventMiscellaneous.EventMiscellaneousNameAR;
            Event.ModificationDate = DateTime.Now;
            Event.ModifierId = userId;
            _context.SaveChanges();
        }
    }
}