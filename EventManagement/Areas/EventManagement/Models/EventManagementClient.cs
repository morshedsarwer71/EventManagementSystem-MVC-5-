using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.EventManagement.Models
{
    public class EventManagementClient
    {
        [Key]
        public int ClientId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Concer Name Required")]
        public int ConcernId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Client Name Required in english")]
        public string ClientNameEN { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Client Name Required in arabic")]
        public string ClientNameAR { get; set; }
        [Required]
        public string ClientCode { get; set; }
        public int AccountHeadId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Client contact information required")]
        public string ClientContactInfo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Client address required")]
        public string ClientAddress { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatorId { get; set; }
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
    }
}