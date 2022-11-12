using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionalOutBoxPattern.Application.Commands.CreateDepartment;
using TransactionalOutBoxPattern.Application.Commands.CreateEmployee;

namespace TransactionalOutBoxPattern.WebApi.Controllers;

[ApiController]
[Route("api/v1/departments")]
public class DepartmentController : ControllerBase
{
    private readonly ISender _sender;

    public DepartmentController(ISender sender)
        => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> CreateDepartment(
        CreateDepartmentCommand command,
        CancellationToken token)
    {
        var result = await _sender.Send(command, token);

        return Created("", result);
    }

    [HttpPost("{departmentId:guid}/employees")]
    public async Task<IActionResult> CreateEmployee(
        CreateEmployeeCommand command,
        Guid departmentId,
        CancellationToken token)
    {
        command.DepartmentId = departmentId;
        var result = await _sender.Send(command, token);

        return Created("", result);
    }
}
