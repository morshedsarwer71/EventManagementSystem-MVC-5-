using EventManagement.Areas.EventManagement.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ViewModels
{
    public class ManpowerViewModels
    {
        public IEnumerable<ResponseDailyManPower> ResponseDailyManPowers { get; set; }
    }
}