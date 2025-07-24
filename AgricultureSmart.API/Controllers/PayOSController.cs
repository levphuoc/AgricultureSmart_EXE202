using AgricultureSmart.Services.Extension;
using AgricultureSmart.Services.Interfaces;
using AgricultureSmart.Services.Models.OrderModels;
using AgricultureSmart.Services.Models.PayOSModels;
using AgricultureSmart.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Net.payOS.Types;
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AgricultureSmart.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayOSController(IPayOSService payOSService) : ControllerBase
    {
        private readonly IPayOSService _payOSService = payOSService;

        [HttpPost("create-link/{orderId}")]
        public async Task<IActionResult> CreatePaymentLink([FromRoute] int orderId)
        {
            var paymentLink = await _payOSService.CreatePaymentLink(orderId);
            return Ok(paymentLink);
        }

        [HttpPost("confirm-webhook")]
        public async Task<IActionResult> ConfirmWebhook(WebhookURL body)
        {
            return Ok(await _payOSService.ConfirmWebhook(body));
        }

        [HttpPost("Webhook")]
        public async Task HandleWebhook([FromBody] WebhookType webhookData)
        {
            try
            {
                await _payOSService.HandlePaymentWebhook(webhookData);
            }
            catch (Exception ex)
            {
                throw new Exception("Error handling webhook", ex);
            }
        }
    }
}
