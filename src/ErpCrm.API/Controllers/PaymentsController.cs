using ErpCrm.Application.Features.Payments.Commands.CompletePayment;
using ErpCrm.Application.Features.Payments.Commands.CreatePayment;
using ErpCrm.Application.Features.Payments.Commands.DeletePayment;
using ErpCrm.Application.Features.Payments.Commands.UpdatePayment;
using ErpCrm.Application.Features.Payments.Queries.GetPaymentById;
using ErpCrm.Application.Features.Payments.Queries.GetPayments;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ErpCrm.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] GetPaymentsQuery query)
    {
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetPaymentByIdQuery(id));
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentCommand command)
    {
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentCommand command)
    {
        command.Id = id;
        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeletePaymentCommand(id));
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("{id:int}/complete")]
    public async Task<IActionResult> Complete(
    int id,
    [FromBody] CompletePaymentCommand command)
    {
        command.PaymentId = id;

        var result = await _mediator.Send(command);
        return StatusCode(result.StatusCode, result);
    }
}
