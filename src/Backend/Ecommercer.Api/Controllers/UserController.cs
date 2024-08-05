using Ecommercer.Aplication.Cases.User.Registrar;
using Ecommercer.Aplication.UseCases.User.Registrar;
using Ecommercer.Communictaion.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommercer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IRegistroUsaurio _registroUsaurio;

        public UserController(IRegistroUsaurio registroUsaurio)
        {
            _registroUsaurio = registroUsaurio;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RequestRegistrarUsuarioJson request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _registroUsaurio.Execute(request);
                return Created(string.Empty, response);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
