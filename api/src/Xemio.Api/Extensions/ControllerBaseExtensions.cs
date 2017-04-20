using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Xemio.Api.Extensions
{

    public static class ControllerBaseExtensions
    {
        public static StatusCodeResult Conflict(this ControllerBase self)
        {
            return self.StatusCode(StatusCodes.Status409Conflict);
        }
    }
}
