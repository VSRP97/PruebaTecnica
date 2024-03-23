using PruebaTecnicaImaginemos.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Contratos
{
    public interface IServicioUsuario
    {
        Task<IEnumerable<UsuarioDTO>> GetUsuarios(string search);
        Task<UsuarioDTO> GetUsuario(int id);
        Task<UsuarioDTO> PostUsuario(UsuarioDTO usuarioDTO);
        Task PutUsuario(UsuarioDTO usuarioDTO);
        Task DeleteUsuario(int id);
    }
}
