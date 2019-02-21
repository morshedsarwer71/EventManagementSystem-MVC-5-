using EventManagement.Areas.EventManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Areas.EventManagement.Interfaces
{
    public interface IEventManagementClient
    {
        void Add(EventManagementClient eventManagementClient, string UserName, int UserId, int concernId);
        void Update(int ClientId, EventManagementClient eventManagementClient, string UserName, int UserId);
        void Delete(int ClientId);
        IEnumerable<EventManagementClient> EventClients(int ConcernId);
        EventManagementClient EventClientById(int ClientId);
    }
}
