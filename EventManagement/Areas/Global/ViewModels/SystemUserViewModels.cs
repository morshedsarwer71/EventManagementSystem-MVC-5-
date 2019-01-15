using EventManagement.Areas.Global.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Global.ViewModels
{
    public class SystemUserViewModels
    {
        public IEnumerable<SystemUser> SystemUsers { get; set; }
    }
}