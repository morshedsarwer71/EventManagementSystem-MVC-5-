using EventManagement.Areas.Accounting.Models;
using EventManagement.Areas.Accounting.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.ViewModels
{
    public class AccountHeadsViewModels
    {
        public IEnumerable<AccountsHead> AccountsHeads { get; set; }
        public IEnumerable<ResponseAccountHeads> ResponseAccountHeads { get; set; }
    }
}