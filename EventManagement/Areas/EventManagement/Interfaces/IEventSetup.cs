using EventManagement.Areas.EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IEventSetup
    {
        void AddEventSetup(EventSetup eventSetup, int userId, string userName, int ConcernId);
        void Update(EventSetup eventSetup, int userId, string userName, int setupId);
        void Delete(int userId, string userName, int setupId);
        EventSetup EventSetupById(int userId, string userName, int setupId);
        IEnumerable<EventSetup> EventSetups(int userId, string userName, int ConcernId);
    }
}
