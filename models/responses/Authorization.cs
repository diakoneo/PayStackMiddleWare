namespace PayStackMiddleWare.API.models.responses{
    
    public class Authorization{
        public string authorization_code { get; set; }
        public string bin { get; set; }
        public string last4 { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string channel { get; set; }
        public string card_type { get; set; }
        public string bank { get; set; }
        public string country_code { get; set; }
        public string brand { get; set; }
        public string reusable { get; set; }
        public string signature { get; set; }
        public string account_name { get; set; }
    }
}