using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactVentas.Models;
using ReactVentas.Utils;

namespace ReactVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly DBREACT_VENTAContext _context;
        public UsuarioController(DBREACT_VENTAContext context)
        {
            _context = context;

        }
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                lista = await _context.Usuarios.Include(r => r.IdRolNavigation).OrderByDescending(c => c.IdUsuario).ToListAsync();

                return StatusCode(StatusCodes.Status200OK, lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, lista);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Usuario request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Correo) || string.IsNullOrWhiteSpace(request.Clave))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Nombre, correo y contraseña son obligatorios");
                }

                var existeCorreo = await _context.Usuarios.AnyAsync(u => u.Correo == request.Correo);
                if (existeCorreo)
                {
                    return StatusCode(StatusCodes.Status409Conflict, "El correo ya está registrado");
                }

                request.Clave = PasswordHelper.Hash(request.Clave);
                await _context.Usuarios.AddAsync(request);
                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, "ok");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] Usuario request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Correo))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Nombre y correo son obligatorios");
                }

                var usuarioActual = await _context.Usuarios.FindAsync(request.IdUsuario);
                if (usuarioActual == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Usuario no encontrado");
                }

                var existeCorreo = await _context.Usuarios.AnyAsync(u => u.Correo == request.Correo && u.IdUsuario != request.IdUsuario);
                if (existeCorreo)
                {
                    return StatusCode(StatusCodes.Status409Conflict, "El correo ya está registrado");
                }

                if (!string.IsNullOrWhiteSpace(request.Clave))
                {
                    request.Clave = PasswordHelper.Hash(request.Clave);
                }
                else
                {
                    request.Clave = usuarioActual.Clave;
                }

                _context.Entry(usuarioActual).CurrentValues.SetValues(request);
                await _context.SaveChangesAsync();

                return StatusCode(StatusCodes.Status200OK, "ok");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                Usuario usuario = _context.Usuarios.Find(id);
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return StatusCode(StatusCodes.Status200OK, "ok");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
