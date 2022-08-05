namespace PayStackMiddleWare.API.models.txns
{
    public class Card
    {
        public string mode{get;set;}
        public string number{get;set;}
        public string cvv{get;set;}
        public string expiry{get;set;}
        public decimal amount{get;set;}
        public string status{get;set;}
        public DateTime date{get;set;}
    }
}