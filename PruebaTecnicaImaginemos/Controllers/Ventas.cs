using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaImaginemos.Aplicacion.Contratos;
using PruebaTecnicaImaginemos.Aplicacion.DTOs.Venta;
using PruebaTecnicaImaginemos.Aplicacion.Servicios;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PruebaTecnicaImaginemos.Controllers
{
    [ApiController]
    [Route("ventas")]
    public class Ventas : ControllerBase
    {
        private readonly ILogger<Productos> _logger;
        private readonly IServicioVenta _servicioVenta;

        public Ventas(
            ILogger<Productos> logger,
            IServicioVenta servicioVenta)
        {
            _logger = logger;
            _servicioVenta = servicioVenta;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VentaDTO>>> Get([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] string search = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var ventasDTO = await _servicioVenta.GetVentas(fechaInicio, fechaFin, search);
                var cantidadVentas = ventasDTO.Count();
                var cantidadPaginas = (int)Math.Ceiling((double)cantidadVentas / pageSize);
                var ventasPaginadas = ventasDTO.Skip((page - 1) * pageSize).Take(pageSize);

                return Ok(ventasPaginadas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDTO>> Get(int id)
        {
            try
            {
                var ventaDTO = await _servicioVenta.GetVenta(id);
                return ventaDTO is null ? NotFound(ventaDTO) : Ok(ventaDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PostVentaDTO>> Post(PostVentaRequestDTO postVentaRequest)
        {
            try
            {
                var postVentaDTO = await _servicioVenta.PostVenta(postVentaRequest);
                return StatusCode(201, postVentaDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] VentaDTO ventaDTO)
        {
            try
            {
                ventaDTO.Id = id;
                await _servicioVenta.PutVenta(ventaDTO);
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
                await _servicioVenta.DeleteVenta(id);
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
