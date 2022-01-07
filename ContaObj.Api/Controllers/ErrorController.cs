using ContaObj.Domain.ViewModel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContaObj.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            //TODO tratar exceções

            Response.StatusCode = 500;

            var idError = Activity.Current?.Id ?? HttpContext?.TraceIdentifier;
            return new ErrorResponse(idError, HttpContext?.TraceIdentifier);
        }
    }
}
