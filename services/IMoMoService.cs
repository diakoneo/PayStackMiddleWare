using PayStackMiddleWare.API.models.txns;
using PayStackMiddleWare.API.models.responses;

namespace PayStackMiddleWare.API.services{

    public interface IMoMoService{
        Task<AmtTransferPayloadResponse> makePayment(AmtTransferPayload payLoad);
        Task<VerifyPayloadResponse> VerifyTransaction(string txnReferenceID);

        Task<TransactionPayloadResponse> listTransactions();
        Task<VerifyPayloadResponse> listTransactions(decimal amount);
        Task<TransactionPayloadResponse> listTransactions(string status);
        Task<TransactionPayloadResponse> listTransactions(int CustomerID);
        Task<TransactionPayloadResponse> listTransactionWithAmount(decimal amount);
        Task<TransactionPayloadResponse> listTransactionWithinDateRange(DateTime dfrom, DateTime dTo);

    }

}