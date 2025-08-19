using AgricultureSmart.Repositories.DbAgriContext;
using AgricultureSmart.Repositories.Entities;
using AgricultureSmart.Repositories.Repositories;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.PayOSModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Net.payOS;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json; 
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AgricultureSmart.Services.Services
{
    public class PayOSService : IPayOSService
    {
        private readonly PayOS _payOS;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly AgricultureSmartDbContext _context;

        public PayOSService(
            PayOS payOS,
            IMapper mapper,
            IConfiguration configuration,
            AgricultureSmartDbContext context)
        {
            _payOS = payOS;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
        }

        public async Task<PaymentLinkResponse> CreatePaymentLink(int orderID)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var orderRepo = new GenericRepository<Order>(_context);

                    var order = await orderRepo.GetByIdAsync(orderID);
                    if (order == null)
                    {
                        throw new ArgumentException("Order not found");
                    }

                    long orderCode = long.Parse(DateTimeOffset.Now.ToString("ffffff"));
                    var description = "Thanh toán đơn hàng";

                    ItemData item = new ItemData("Order Package", 1, (int)order.TotalAmount);
                    List<ItemData> items = new List<ItemData> { item };

                    var baseUrl = _configuration["FrontendUrl"];
                    /*var returnSuccessUrl = $"{baseUrl}/success?orderCode={orderCode}";
                    var returnCancelledUrl = $"{baseUrl}/cancel?orderCode={orderCode}";*/
                    var returnSuccessUrl = $"{_configuration["FrontendUrl_Success"]}?orderCode={orderCode}";
                    var returnCancelledUrl = $"{_configuration["FrontendUrl_Cancel"]}?orderCode={orderCode}";


                    PaymentData paymentData = new PaymentData(
                        orderCode,
                        (int)order.TotalAmount,
                        description,
                        items,
                        returnCancelledUrl,
                        returnSuccessUrl
                    );

                    CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

                    order.OrderNumber = orderCode.ToString();
                    await orderRepo.UpdateAsync(order);

                    await transaction.CommitAsync();

                    return new PaymentLinkResponse
                    {
                        PaymentUrl = createPayment.checkoutUrl
                    };
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

    public async Task HandlePaymentWebhook(WebhookType webhookData)
        {
            var strategy = _context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    WebhookData data = _payOS.verifyPaymentWebhookData(webhookData);
                    var orderRepo = new GenericRepository<Order>(_context);

                    var order = await orderRepo.FirstOrDefaultAsync(x => x.OrderNumber == data.orderCode.ToString());

                    if (order != null)
                    {
                        order.PaymentStatus = data.code != "00" ? "FAILED" : "SUCCESS";

                        if (data.code == "00")
                        {
                            order.PaidAt = DateTime.Now;
                            order.Status = "pended";
                        }

                        await orderRepo.UpdateAsync(order);
                    }

                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            });
        }

        public async Task<string> ConfirmWebhook(WebhookURL body)
        {
            try
            {
                return await _payOS.confirmWebhook(body.webhook_url);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return exception.Message;
            }
        }
    }
}
