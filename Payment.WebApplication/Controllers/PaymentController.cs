using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Payment.Entities;
using Payment.UseCase.Port.In;
using Payment.WebApplication.Models.Parameters;

namespace Payment.WebApplication.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PaymentController : ControllerBase
{
    private readonly IRequestPaymentService _requestPaymentService;
    private readonly IPaymentFailedService _paymentFailedService;
    private readonly ICompletePaymentService _completePaymentService;

    public PaymentController(IRequestPaymentService requestPaymentService, IPaymentFailedService paymentFailedService, ICompletePaymentService completePaymentService)
    {
        _requestPaymentService = requestPaymentService;
        _paymentFailedService = paymentFailedService;
        _completePaymentService = completePaymentService;
    }

    /// <summary>
    /// 建立付款
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] PaymentParameter parameter)
    {
        if (parameter.OrderId == Guid.Empty)
        {
            return BadRequest();
        }
        
        var paymentId = await _requestPaymentService.HandleAsync(parameter.OrderId,parameter.Amount);
        if (paymentId == Guid.Empty)
        {
            return BadRequest();
        }

        return Ok(paymentId);
    }

    /// <summary>
    /// 取消付款
    /// </summary>
    /// <param name="paymentId"></param>
    /// <param name="failedReason"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CancelAsync(Guid paymentId,string failedReason)
    {
        if (paymentId == Guid.Empty)
        {
            return BadRequest();
        }

        var cancel = await _paymentFailedService.HandleAsync(paymentId, failedReason);
        
        return Ok(cancel);
    }
    
    [HttpPost]
    public async Task<IActionResult> CompleteAsync(Guid paymentId, string transactionId)
    {
        if (paymentId == Guid.Empty)
        {
            return BadRequest();
        }

        var complete = await _completePaymentService.HandleAsync(paymentId, transactionId);

        return Ok(complete);
    }
}






















