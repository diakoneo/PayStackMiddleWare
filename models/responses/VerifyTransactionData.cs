namespace PayStackMiddleWare.API.models.responses
{
    public class  VerifyTransactionData
    {
        public int? id { get; set; }
        public string? domain { get; set; }
        public string? status { get; set; }
        public string? reference { get; set; }
        public double? amount { get; set; }
        public string? message { get; set; }
        public string? gateway_response { get; set; }
        public string? paid_at { get; set; }
        public string? created_at { get; set; }
        public string? channel { get; set; }
        public string? currency { get; set; }
        public string? ip_address { get; set; }
        public MetaData? metadata { get; set; }
        public Log log { get; set; }
        public double? fees { get; set; }
        public double? fees_split { get; set; }
        public Authorization? authorization { get; set; }
        public Customer? customer { get; set; }
        public Plan plan { get; set; }
        public Split split { get; set; }
        public int? order_id { get; set; }
        public string? paidAt { get; set; }
        public string? createdAt { get; set; }
        public double? requested_amount { get; set; }
        public string? pos_transaction_data { get; set; }
        public Source? source { get; set; }
        public string? fees_breakdown { get; set; }
        public string? transaction_date { get; set; }
        public PlanObject? plan_object { get; set; }
        public SubAccount? subaccount { get; set; }
    }

    public class Log
    {
        public string? start_time { get; set; }
        public int? time_spent { get; set; }
        public int? attempts { get; set; }
        public string? errors { get; set; }
        public bool? success { get; set; }
        public bool? mobile { get; set; }
        public List<Inputs>? input { get; set; }
        public List<History>? history { get; set; }

    }
    public class History
    {
        public string? type { get; set; }
        public string? message { get; set; }
        public int? time { get; set; }
    }
    public class Plan
    {

    }
    public class Inputs
    {

    }
    public class Split
    {

    }
    public class Source
    {
        public string? source { get; set; }
        public string? type { get; set; }
        public string? identifier { get; set; }
        public string? entry_point { get; set; }
    }
}