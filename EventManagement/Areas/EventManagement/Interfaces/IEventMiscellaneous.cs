using EventManagement.Areas.EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IEventMiscellaneous
    {
        void AddEventMiscellaneous(EventMiscellaneous eventMiscellaneous, int userId, string userName, int ConcernId);
        EventMiscellaneous EventMiscellaneousById(int userId, string userName, int EventId);
        void Update(EventMiscellaneous eventMiscellaneous, int userId, string userName, int EventId);
        void Delete(int userId, string userName, int EventId);
        IEnumerable<EventMiscellaneous> EventMiscellaneouses(int userId, string userName, int ConcernId);
    }
}
