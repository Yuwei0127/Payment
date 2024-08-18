﻿using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Payment.UseCase.Port.In;
using Payment.WebApplication.Models.Parameters;

namespace Payment.WebApplication.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class PaymentController : ControllerBase
{
    private readonly IRequestPaymentService _requestPaymentService;
    private readonly ICancelPaymentService _cancelPaymentService;
    private readonly ICompletePaymentService _completePaymentService;

    public PaymentController(IRequestPaymentService requestPaymentService, ICancelPaymentService cancelPaymentService, ICompletePaymentService completePaymentService)
    {
        _requestPaymentService = requestPaymentService;
        _cancelPaymentService = cancelPaymentService;
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

        var cancel = await _cancelPaymentService.HandleAsync(paymentId, failedReason);
        
        return Ok(cancel);
    }
    
    [HttpPost]
    public async Task<IActionResult> CompleteAsync(Guid paymentId)
    {
        if (paymentId == Guid.Empty)
        {
            return BadRequest();
        }

        var complete = await _completePaymentService.HandleAsync(paymentId);

        return Ok(complete);
    }
}






















