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
    public class ServicioDetalleVenta : IServicioDetalleVenta
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServicioDetalleVenta(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DetalleVentaDTO>> GetDetalleVentas(int ventaId)
        {
            var detallesVenta = await _unitOfWork.RepositorioDetalleVenta.GetList(ventaId);
            var detallesVentaDTO = _mapper.Map<IEnumerable<DetalleVentaDTO>>(detallesVenta);

            return detallesVentaDTO;
        }

        public async Task<DetalleVentaDTO> GetDetalleVenta(int ventaId, int id)
        {
            var detalleVenta = await _unitOfWork.RepositorioDetalleVenta.Get(ventaId, id);
            var detalleVentaDTO = _mapper.Map<DetalleVentaDTO>(detalleVenta);

            return detalleVentaDTO;
        }

        public Task<DetalleVentaDTO> PostDetalleVenta(DetalleVentaDTO detalleVenta)
        {
            throw new NotImplementedException();
        }

        public async Task<DetalleVentaDTO> PutDetalleVenta(DetalleVentaDTO detalleVentaDTO)
        {
            var detalleVenta = _mapper.Map<DetalleVenta>(detalleVentaDTO);
            await _unitOfWork.RepositorioDetalleVenta.Update(detalleVenta);
            await _unitOfWork.CompleteAsync();
            detalleVentaDTO = _mapper.Map<DetalleVentaDTO>(detalleVenta);

            return detalleVentaDTO;
        }

        public async Task DeleteDetalleVenta(int ventaId, int id)
        {
            await _unitOfWork.RepositorioDetalleVenta.Delete(ventaId, id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
