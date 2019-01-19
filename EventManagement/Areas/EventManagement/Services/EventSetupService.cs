using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class EventSetupService: IEventSetup
    {
        private readonly DataContext _context;
        public EventSetupService(DataContext context)
        {
            _context = context;
        }
        public void AddEventSetup(EventSetup eventSetup, int userId, string userName, int ConcernId)
        {
            eventSetup.CreatorId = userId;
            eventSetup.CreationDate = DateTime.Now;
            eventSetup.IsDelete = 0;
            eventSetup.ConcernId = ConcernId;
            eventSetup.ModificationDate = DateTime.Now;
            eventSetup.ModifierId = userId;
            _context.EventSetups.Add(eventSetup);
            _context.SaveChanges();
        }

        public void Delete(int userId, string userName, int setupId)
        {
            var eventSetup = EventSetupById(userId, userName, setupId);
            eventSetup.IsDelete = 1;
            eventSetup.ModifierId = userId;
            eventSetup.ModificationDate = DateTime.Now;
            _context.SaveChanges();
        }

        public EventSetup EventSetupById(int userId, string userName, int setupId)
        {
            var EventSetup = _context.EventSetups.FirstOrDefault(x => x.EventSetupId == setupId);
            return EventSetup;
        }

        public IEnumerable<EventSetup> EventSetups(int userId, string userName, int ConcernId)
        {
            var Setups = _context.EventSetups.Where(x => x.ConcernId == ConcernId && x.IsDelete == 0).ToList();
            return Setups;
        }

        public void Update(EventSetup eventSetup, int userId, string userName, int setupId)
        {
            var Setup = EventSetupById(userId, userName, setupId);
            Setup.EventSetupEN = eventSetup.EventSetupEN;
            Setup.EventSetupAR = eventSetup.EventSetupAR;
            Setup.ModifierId = userId;
            Setup.ModificationDate = DateTime.Now;
            _context.SaveChanges();
        }
    }
}