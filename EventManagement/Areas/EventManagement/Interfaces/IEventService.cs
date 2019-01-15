using EventManagement.Areas.EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IEventService
    {
        void AddEventService(EventService eventService, int userId, string userName, int ConcernId);
        IEnumerable<EventService> EventServices(int userId, string userName, int ConcernId);
        EventService EventServiceById(int userId, string userName, int ServiceId);
        void Update(EventService eventService, int userId, string userName, int ServiceId);
        void Delete(int userId, string userName, int ServiceId);
    }
}
