﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ReportObject
{
    public class ExpenditureReport
    {
        public int TransType { get; set; }
        public int Expenditure { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }
}