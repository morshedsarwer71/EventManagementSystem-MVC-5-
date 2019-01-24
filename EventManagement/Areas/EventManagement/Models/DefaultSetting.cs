using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class DefaultSetting
    {
        [Key]
        public int SettingId { get; set; }
        public string VatName { get; set; }
        public decimal VatValue { get; set; }
        public string VatNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public int ConcernId { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
    }
}