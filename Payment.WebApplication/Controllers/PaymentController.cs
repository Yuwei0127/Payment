using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Payment.UseCase.Port.In;
using Payment.WebApplication.Models.Parameters;

namespace Payment.WebApplication.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PaymentController : ControllerBase
{
    private readonly ICreatePaymentService _createPaymentService;
    private readonly ICancelPaymentService _cancelPaymentService;

    public PaymentController(ICreatePaymentService createPaymentService, ICancelPaymentService cancelPaymentService)
    {
        _createPaymentService = createPaymentService;
        _cancelPaymentService = cancelPaymentService;
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
        
        var paymentId = await _createPaymentService.HandlerAsync(parameter.OrderId,parameter.Amount);
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

        var cancel = await _cancelPaymentService.HandlerAsync(paymentId, failedReason);
        
        return Ok(cancel);
    }
}






















