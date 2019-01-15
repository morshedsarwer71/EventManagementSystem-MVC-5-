using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Global.Models
{
    public class SystemUser
    {
        [Key]
        public int UserID { get; set; }

        //[Required(AllowEmptyStrings = false,ErrorMessage = "User Name is Required")]
        //[Display(Name="User Name")]
        public string Code { get; set; }

        //[Required(AllowEmptyStrings =false,ErrorMessage ="Full name is required")]
        //[Display(Name="Full Name")]
        public string Name { get; set; }

        //[Required(AllowEmptyStrings =false,ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public Boolean Disabled { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LoginDate { get; set; }
        //[Required(AllowEmptyStrings =false,ErrorMessage ="Email Address is requird")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public int LoginFailedCount { get; set; }
        public string Language { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ActivationDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        public string HashSalt { get; set; }

        public int CreatorId { get; set; }
        public int ConcernID { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime ModificationDate { get; set; }
        public int ModifierId { get; set; }
        public int IsDelete { get; set; }
    }
}