namespace PayStackMiddleWare.API.models.txns{

    public class MoMo{
        public string mode{get;set;}
        public string provider{get;set;}
        public string number{get;set;}
        public decimal amount{get;set;}
        public string status{get;set;}
        public DateTime date{get;set;}
    }

}