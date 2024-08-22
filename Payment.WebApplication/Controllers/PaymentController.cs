using Asp.Versioning;
using Microsoft.AspNetCore.Http.Features;
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
    private readonly IPaymentCancelService _paymentCancelService;

    public PaymentController(IRequestPaymentService requestPaymentService,
        IPaymentCancelService paymentCancelService)
    {
        _requestPaymentService = requestPaymentService;
        _paymentCancelService = paymentCancelService;
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

        var paymentId = await _requestPaymentService.HandleAsync(parameter.OrderId, parameter.Amount);
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
    /// <param name="cancelReason"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CancelAsync(Guid paymentId, string cancelReason)
    {
        if (paymentId == Guid.Empty)
        {
            return BadRequest();
        }
        
        var cancelSuccess = await _paymentCancelService.HandleAsync(paymentId, cancelReason);
        return Ok(cancelSuccess);
    }
    
}






















