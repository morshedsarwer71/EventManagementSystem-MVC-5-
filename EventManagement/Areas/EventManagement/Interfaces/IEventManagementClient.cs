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
        void AddManpowerSupplier(ManpowerSupplier manpowerSupplier, string UserName, int UserId, int concernId);
        IEnumerable<ManpowerSupplier> ManpowerSuppliers(int ConcernId);
        void UpdateManpowerSupplier(ManpowerSupplier manpowerSupplier,int id, string UserName, int UserId, int concernId);
    }
}
