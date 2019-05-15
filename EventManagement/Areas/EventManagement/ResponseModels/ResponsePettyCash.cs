using System;
namespace EventManagement.Areas.EventManagement.ResponseModels
{
    public class ResponsePettyCash
    {
        public string Date { get; set; }
        public string BankName { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int id { get; set; }

    }
}
