using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TransactionalOutBoxPattern.WebApi.Controllers;

[ApiController]
[Route("api/v1/employee")]
public class EmployeeController : ControllerBase
{
    private readonly ISender _sender;

    public EmployeeController(ISender sender)
        => _sender = sender;
}
