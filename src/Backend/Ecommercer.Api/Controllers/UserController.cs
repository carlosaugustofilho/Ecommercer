using Ecommercer.Communictaion.Requests;
using Ecommercer.Communictaion.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommercer.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegistrarUsuarioJson), StatusCodes.Status201Created)]
        public IActionResult Register(RequestRegistrarUsuarioJson request)
        {
            return Ok();
        }

    }
}
