using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaImaginemos.Aplicacion.Contratos;
using PruebaTecnicaImaginemos.Aplicacion.DTOs;

namespace PruebaTecnicaImaginemos.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class Usuarios : ControllerBase
    {
        private readonly ILogger<Usuarios> _logger;
        private readonly IServicioUsuario _servicioUsuario;

        public Usuarios(
            ILogger<Usuarios> logger,
            IServicioUsuario servicioUsuario)
        {
            _logger = logger;
            _servicioUsuario = servicioUsuario;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> Get([FromQuery] string search)
        {
            try
            {
                var usuariosDTO = await _servicioUsuario.GetUsuarios(search);
                return Ok(usuariosDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDTO>> Get(int id)
        {
            try
            {
                var usuarioDTO = await _servicioUsuario.GetUsuario(id);
                return usuarioDTO is null ? NotFound(usuarioDTO) : Ok(usuarioDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                await _servicioUsuario.PutUsuario(usuarioDTO);
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
                await _servicioUsuario.DeleteUsuario(id);
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
