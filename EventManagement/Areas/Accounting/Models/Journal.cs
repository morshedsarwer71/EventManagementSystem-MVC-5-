using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventManagement.Areas.Accounting.Models
{
    public class Journal
    {
        [Key]
        public int JournalId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Journal Entry Date Must need")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime JournalEntryDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "ConcernID Required")]
        public int ConcernId { get; set; }

        [Required]
        public string VoucherCode { get; set; }

        //[Compare("DebitJournalAmount", ErrorMessage = "Credit Does Not Match")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debit Amount Must Need")]
        public decimal DebitJournalAmount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Account Head Need")]
        public int DebitAccountsHeadId { get; set; }

        [Compare("DebitJournalAmount", ErrorMessage = "Debit Does Not Match with credit journal amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Credit Amount Must Need")]
        public decimal CreditJournalAmount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Account Head Need")]
        public int CreditAccountsHeadId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Need Description")]
        public string Description { get; set; }

        public DateTime JournalCreationDate { get; set; }
        public int JournalCreator { get; set; }
        public DateTime JournalModificationDate { get; set; }
        public int JournalModifierId { get; set; }
        public int IsDelete { get; set; }
    }
}