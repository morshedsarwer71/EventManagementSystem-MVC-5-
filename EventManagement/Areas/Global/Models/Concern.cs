using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Global.Models
{
    public class Concern
    {
        [Key]
        public int ConcernId { get; set; }
        //[Required]
        public string ConcernNameEN { get; set; }
        //[Required]
        public string ConcernNameAR { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
    }
}