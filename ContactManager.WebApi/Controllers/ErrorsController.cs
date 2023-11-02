using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.WebApi.Controllers;

public class ErrorsController : ApiController
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    
        return Problem(
            title: exception?.Message,
            statusCode:500);
    }
}
