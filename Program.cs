using Microsoft.OpenApi.Models;
using PayStackMiddleWare.API.models.utils;
using PayStackMiddleWare.API.models.txns;
using PayStackMiddleWare.API.services;
using PayStackMiddleWare.API.models.responses;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

#region start of swagger documentation configuration

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1",new OpenApiInfo{
        Title = @"PayStackMiddleWare.API",
        Description = @"Integrates PayStack API with any client",
        Version = @"v1"
    });
});

#endregion

#region custom-repository

builder.Services.AddSingleton<IMoMoService,MoMoService>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();

#endregion

var app = builder.Build();

#region start of swaggerUI configuration 

app.UseSwagger();
app.UseSwaggerUI(c=>{
    c.SwaggerEndpoint("/swagger/v1/swagger.json","PayStackMiddleWare.API v1");
});

#endregion

#region start of settings configuration

var settings = builder.Configuration.GetSection("Settings").Get<Settings>();
ConfigObject.INITIALIZE_PAYMENT_ENDPOINT = settings.apiUrl;
ConfigObject.VERIFY_TXN_URL = settings.verifyTxnAPI;
ConfigObject.SECRET_API_KEY = settings.secretKey;
ConfigObject.CONTENT_TYPE = settings.contentType;
ConfigObject.TXN_CURRENCY = settings.TxnCurrency;

ConfigObject.WEBHOOK_URL = settings.webHookURL;
ConfigObject.GET_TRANSACTIONS = settings.gettransactions;
ConfigObject.GET_CUSTOMER = settings.getcustomer;


#endregion

#region API routes


app.MapPost("/MoMo/makePayment", async (MoMo momo, IMoMoService service) => await makeTransaction(momo,service)).Accepts<MoMo>("application/json")
 .Produces<AmtTransferPayloadResponse>(StatusCodes.Status200OK)
 .WithName("Initiate Payment")
 .WithTags("Setters");

app.MapGet("/MoMo/getTransactions", async(IMoMoService service) => await GetPayments(service));

app.MapGet("/MoMo/getTransaction/{CustomerID}", async(int CustomerID, IMoMoService service)=> await GetTransactionWithID(CustomerID, service));

app.MapGet("/MoMo/getTransactionByStatus/{status}",async(string status, IMoMoService service)=>await GetTransactionWithStatus(status,service));

app.MapGet("/MoMo/getTransactionByAmount/{amount}", async (decimal amount, IMoMoService service) => await GetTransactionWithAmount(amount, service));

app.MapGet("/MoMo/getTransactionWithDateRange/{datefrom}/{dateTo}", async (DateTime datefrom, DateTime dateTo, IMoMoService service) => await GetTransactionWithinPeriod(datefrom,dateTo, service));

app.MapPost("/MoMo/WebHookURL", async (IMoMoService service, string req, string resp) => await WebHookURL(service, req, resp));
#endregion

#region Payment or Transactions Route-implementations

async Task<IResult> makeTransaction(MoMo momo, IMoMoService service){
    VerifyPayloadResponse vresp;

    var payLoad = new AmtTransferPayload()
    {
        amount = momo.amount.ToString(),
        currency = ConfigObject.TXN_CURRENCY,
        email = @"nanaofosuappiah@gmail.com",
        channels = new string[] {"mobile_money"},
        callback = ConfigObject.WEBHOOK_URL
    };

    var res = await service.makePayment(payLoad);
    if (res.status.ToLower() == @"true")
    {
        /* verify the txn */
         vresp = await service.VerifyTransaction(res.data.reference);
         return Results.Ok(vresp);
    }
    else
    {
        return Results.NotFound("An error occured");
    }
}

async Task<IResult> GetPayments(IMoMoService service){
    var oResponse = await service.listTransactions();
    return Results.Ok(oResponse);
}

async Task<IResult> GetTransactionWithID(int CustomerID, IMoMoService service){
    var apiResponse = await service.listTransactions(CustomerID);
    return Results.Ok(apiResponse);
}

async Task<IResult> GetTransactionWithStatus(string status, IMoMoService service)
{
    var oResponse = await service.listTransactions(status);
    return Results.Ok(oResponse);
}

async Task<IResult> GetTransactionWithAmount(decimal amount, IMoMoService service)
{
    var oResponse = await service.listTransactionWithAmount(amount);
    return Results.Ok(oResponse);
}

async Task<IResult> GetTransactionWithinPeriod(DateTime datefrom, DateTime dateTo, IMoMoService service)
{
    var response = await service.listTransactionWithinDateRange(datefrom, dateTo);
    return Results.Ok(response);
}


#endregion WebHookURL

#region Customer Route implementations

app.MapPost("/Customer/createCustomer", async(ICustomerService service, Customer customer)=>await CreateCustomer(customer,service));

async Task<IResult> CreateCustomer(Customer customer, ICustomerService service)
{
    var response = await service.CreateCustomer(customer);
    return Results.Ok(response);
}

#endregion

#region Card Route implementations


#endregion

#region

async Task<IResult> WebHookURL(IMoMoService service, string req, string resp)
{
    return Results.Ok();
}

#endregion


app.Run();
