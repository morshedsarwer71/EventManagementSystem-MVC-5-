using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.ResponseModels
{
    public class ResponseAccountHeads
    {
        public int Serial { get; set; }
        public int AccountHeadId { get; set; }
        public string AccountHeadName { get; set; }
        public string TransType { get; set; }
        public string ReportHeadName { get; set; }
        public int Rows { get; set; }
    }
}