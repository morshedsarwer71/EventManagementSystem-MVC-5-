using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponseProject
    {
        public int ProjectId { get; set; }
        public string SerialNumber { get; set; }
        public string ProjectCode { get; set; }
        public string ProjectNameEN { get; set; }
        public string ProjectNameAR { get; set; }
        public string ProjectIsActive { get; set; }
        public string ProjectIsVatable { get; set; }
        public string Description { get; set; }
    }
}