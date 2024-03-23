using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaImaginemos.Aplicacion.Contratos;
using PruebaTecnicaImaginemos.Aplicacion.DTOs.Producto;
using PruebaTecnicaImaginemos.Dominio.Entidades;

namespace PruebaTecnicaImaginemos.Controllers
{
    [ApiController]
    [Route("productos")]
    public class Productos : ControllerBase
    {
        private readonly ILogger<Productos> _logger;
        private readonly IServicioProducto _servicioProducto;

        public Productos(
            ILogger<Productos> logger,
            IServicioProducto servicioProducto)
        {
            _logger = logger;
            _servicioProducto = servicioProducto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> Get([FromQuery] string nombre)
        {
            try
            {
                var productosDTO = await _servicioProducto.GetProductos(nombre);
                return Ok(productosDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            try
            {
                var productoDTO = await _servicioProducto.GetProducto(id);
                return productoDTO is null ? NotFound(productoDTO) : Ok(productoDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ProductoDTO>> Post([FromBody] PostProductoRequestDTO productoRequestDTO)
        {
            try
            {
                var productoDTO = await _servicioProducto.PostProducto(productoRequestDTO);
                return StatusCode(201, productoDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductoDTO productoDTO)
        {
            try
            {
                productoDTO.Id = id;
                await _servicioProducto.PutProducto(productoDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _servicioProducto.DeleteProducto(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
