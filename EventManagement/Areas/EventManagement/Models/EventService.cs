using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class EventService
    {
        [Key]
        public int EventServiceId { get; set; }
        [Required]
        public string EventServiceEN { get; set; }
        [Required]
        public string EventServiceAR { get; set; }
        public DateTime CreationDate { get; set; }
        public int ConcernId { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
        [NotMapped]
        public bool IsChecked { get; set; }
    }
}