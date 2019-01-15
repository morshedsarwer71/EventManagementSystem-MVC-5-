using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseCategory
    {
        public int CategoryId { get; set; }
        public int SerialNumber { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryNameEN { get; set; }
        public string CategoryNameAR { get; set; }
    }
}