using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactVentas.Models;
using ReactVentas.Models.DTO;
using ReactVentas.Utils;

namespace ReactVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly DBREACT_VENTAContext _context;
        public SessionController(DBREACT_VENTAContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Dtosesion request)
        {
            Usuario usuario = new Usuario();
            try
            {
                if (string.IsNullOrWhiteSpace(request.correo) || string.IsNullOrWhiteSpace(request.clave))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, usuario);
                }

                usuario = _context.Usuarios
                    .Include(u => u.IdRolNavigation)
                    .FirstOrDefault(u => u.Correo == request.correo);

                if (usuario != null && PasswordHelper.Verify(request.clave, usuario.Clave))
                {
                    return StatusCode(StatusCodes.Status200OK, usuario);
                }

                return StatusCode(StatusCodes.Status401Unauthorized, new Usuario());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, usuario);
            }
        }
    }
}
