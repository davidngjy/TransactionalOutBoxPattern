﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionalOutBoxPattern.Application.Commands.UpdateEmployee;
using TransactionalOutBoxPattern.Application.Queries.GetEmployee;

namespace TransactionalOutBoxPattern.WebApi.Controllers;

[ApiController]
[Route("api/v1/employees")]
public class EmployeeController : ControllerBase
{
    private readonly ISender _sender;

    public EmployeeController(ISender sender)
        => _sender = sender;

    [HttpGet("{employeeId:guid}")]
    public async Task<IActionResult> GetEmployee(Guid employeeId, CancellationToken token)
    {
        var query = new GetEmployeeQuery(employeeId);
        return Ok(await _sender.Send(query, token));
    }

    [HttpPost("{employeeId:guid}")]
    public async Task<IActionResult> UpdateEmployee(
        Guid employeeId, UpdateEmployeeCommand command, CancellationToken token)
    {
        command.EmployeeId = employeeId;
        await _sender.Send(command, token);
        return Ok();
    }
}
