using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Xemio.Api.Controllers
{
    [Route("debug")]
    [Authorize]
    public class DebugController : ControllerBase
    {
        public async Task<IActionResult> GetAsync()
        {
            return this.Ok(new
            {
                Swag = "jo"
            });
        }
    }
}