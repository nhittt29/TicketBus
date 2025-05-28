using TicketBus.Models;

namespace TicketBus.Services.Momo
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(OrderInfoModel model);

        Task<MomoExecuteResponseModel> PaymentExecuteAsync(IQueryCollection collection);
    }
}