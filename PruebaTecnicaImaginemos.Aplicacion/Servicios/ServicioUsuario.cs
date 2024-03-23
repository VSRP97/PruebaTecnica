using AutoMapper;
using PruebaTecnicaImaginemos.Aplicacion.Contratos;
using PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia;
using PruebaTecnicaImaginemos.Aplicacion.DTOs;
using PruebaTecnicaImaginemos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Servicios
{
    public class ServicioUsuario : IServicioUsuario
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServicioUsuario(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UsuarioDTO>> GetUsuarios(string search = "")
        {
            var usuarios = await _unitOfWork.RepositorioUsuario.GetList(search);
            var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);

            return usuariosDTO;
        }

        public async Task<UsuarioDTO> GetUsuario(int id)
        {
            var usuario = await _unitOfWork.RepositorioUsuario.Get(id);
            var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

            return usuarioDTO;
        }

        public Task<UsuarioDTO> PostUsuario(UsuarioDTO usuarioDTO)
        {
            throw new NotImplementedException();
        }

        public async Task PutUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            await _unitOfWork.RepositorioUsuario.Update(usuario);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteUsuario(int id)
        {
            await _unitOfWork.RepositorioUsuario.Delete(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
