using AgricultureSmart.Services.Models.PayOSModels;
using Microsoft.AspNetCore.Http;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Interfaces
{
    public interface IPayOSService
    {
        /*Task<string> CreatePaymentUrl(PayOSCreatePaymentRequest request);
        Task<PayOSPaymentCallbackModel> ProcessPaymentCallback(IQueryCollection collection);

        WebhookData VerifyPaymentWebhookData(WebhookType webhookBody);*/

        Task<string> CreatePaymentLink(int orderID);
        Task HandlePaymentWebhook(WebhookType webhookData);
        Task<string> ConfirmWebhook(WebhookURL body);

    }
}
