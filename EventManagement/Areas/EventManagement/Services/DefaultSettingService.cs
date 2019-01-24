using EventManagement.Areas.EventManagement.Interfaces;
using EventManagement.Areas.EventManagement.Models;
using EventManagement.Areas.EventManagement.ResponseModels;
using EventManagement.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Services
{
    public class DefaultSettingService : IDefaultSetting
    {
        private readonly DataContext _context;
        public DefaultSettingService(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<DefaultSetting> GetDefaultSetting(int concernId, int userId, string userName)
        {
            return _context.DefaultSettings.Where(x => x.ConcernId == concernId).ToList();
        }

        public IEnumerable<PaymentStatus> PaymentStatuses()
        {
            var payments = new List<PaymentStatus>() {
                new PaymentStatus {Id=1,Name="Cash" },
                new PaymentStatus {Id=2,Name="Installment" }
            };
           
            return payments;
        }
    }
}