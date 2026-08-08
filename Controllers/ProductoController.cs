using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactVentas.Models;

namespace ReactVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly DBREACT_VENTAContext _context;
        public ProductoController(DBREACT_VENTAContext context)
        {
            _context = context;

        }
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                lista = await _context.Productos.Include(c => c.IdCategoriaNavigation).OrderByDescending(c => c.IdProducto).ToListAsync();

                return StatusCode(StatusCodes.Status200OK, lista);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, lista);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] Producto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Codigo) || string.IsNullOrWhiteSpace(request.Descripcion) || string.IsNullOrWhiteSpace(request.Marca))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Código, marca y descripción son obligatorios");
                }

                if (request.Precio is null || request.Precio <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "El precio debe ser mayor a cero");
                }

                if (request.Stock is null || request.Stock < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "El stock no puede ser negativo");
                }

                await _context.Productos.AddAsync(request);
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
        public async Task<IActionResult> Editar([FromBody] Producto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Codigo) || string.IsNullOrWhiteSpace(request.Descripcion) || string.IsNullOrWhiteSpace(request.Marca))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Código, marca y descripción son obligatorios");
                }

                if (request.Precio is null || request.Precio <= 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "El precio debe ser mayor a cero");
                }

                if (request.Stock is null || request.Stock < 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "El stock no puede ser negativo");
                }

                _context.Productos.Update(request);
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
                Producto usuario = _context.Productos.Find(id);
                _context.Productos.Remove(usuario);
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
