using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaImaginemos.Aplicacion.Contratos;
using PruebaTecnicaImaginemos.Aplicacion.DTOs;

namespace PruebaTecnicaImaginemos.Controllers
{
    [ApiController]
    [Route("ventas/{ventaId}/detalles")]
    public class DetallesVenta : ControllerBase
    {
        private readonly ILogger<DetallesVenta> _logger;
        private readonly IServicioDetalleVenta _servicioDetalleVenta;

        public DetallesVenta(
            ILogger<DetallesVenta> logger,
            IServicioDetalleVenta servicioDetalleVenta)
        {
            _logger = logger;
            _servicioDetalleVenta = servicioDetalleVenta;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVentaDTO>>> Get(int ventaId)
        {
            try
            {
                var detallesVentaDTO = await _servicioDetalleVenta.GetDetalleVentas(ventaId);
                return Ok(detallesVentaDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleVentaDTO>> Get(int ventaId, int id)
        {
            try
            {
                var detalleVentaDTO = await _servicioDetalleVenta.GetDetalleVenta(ventaId, id);
                return detalleVentaDTO is null ? NotFound(detalleVentaDTO) : Ok(detalleVentaDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DetalleVentaDTO detalleVentaDTO)
        {
            try
            {
                detalleVentaDTO.Id = id;
                await _servicioDetalleVenta.PutDetalleVenta(detalleVentaDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int ventaId, int id)
        {
            try
            {
                await _servicioDetalleVenta.DeleteDetalleVenta(ventaId, id);
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
