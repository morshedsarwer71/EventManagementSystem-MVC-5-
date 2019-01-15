using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.ResponseModels
{
    public class IncomeStatement
    {
        public string Particulars { get; set; }
        public decimal ValueOne { get; set; }
        public decimal ValueTwo { get; set; }
        public decimal ValueThree { get; set; }
    }
}