using System;
namespace PayStackMiddleWare.API.models.txns
{
    public class Txn
    {
        public string customer { get; set; } //customer id
        public string status { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public decimal amount { get; set; }
    }
}

