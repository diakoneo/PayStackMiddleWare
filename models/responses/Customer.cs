namespace PayStackMiddleWare.API.models.responses{
    
    public class Customer{
        public int id { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string email { get; set; }
            public string customer_code { get; set; }
            public string phone { get; set; }
            public CustomerMetaData metadata { get; set; }
            public string risk_action { get; set; }
            public string international_format_phone { get; set; }
    }

    public class CustomerMetaData
    {
        public string calling_code { get; set; }
    }

}