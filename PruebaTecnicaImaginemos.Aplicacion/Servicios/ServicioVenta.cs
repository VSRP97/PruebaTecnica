using AutoMapper;
using PruebaTecnicaImaginemos.Aplicacion.Contratos;
using PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia;
using PruebaTecnicaImaginemos.Aplicacion.DTOs.Venta;
using PruebaTecnicaImaginemos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Servicios
{
    public class ServicioVenta : IServicioVenta
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServicioVenta(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VentaDTO>> GetVentas(DateTime? fechaInicio, DateTime? fechaFin, string search = "")
        {
            IEnumerable<Venta> ventas;

            if (fechaInicio == null && fechaFin == null)
            {
                ventas = await _unitOfWork.RepositorioVenta.GetList(search);
            }
            else
            {
                DateTime inicio = fechaInicio ?? DateTime.MinValue;
                DateTime fin = fechaFin ?? DateTime.MaxValue;
                ventas = await _unitOfWork.RepositorioVenta.GetList(search, inicio, fin);
            }            

            var ventasDTO = _mapper.Map<IEnumerable<VentaDTO>>(ventas);
            return ventasDTO;
        }

        public async Task<VentaDTO> GetVenta(int id)
        {
            var venta = await _unitOfWork.RepositorioVenta.Get(id);
            var ventaDTO = _mapper.Map<VentaDTO>(venta);

            return ventaDTO;
        }

        public async Task<PostVentaDTO> PostVenta(PostVentaRequestDTO postVentaRequest)
        {
            var venta = _mapper.Map<Venta>(postVentaRequest);
            venta.Fecha = DateTime.Now;
            venta.DetallesVenta = await venta.DetallesVenta.ToAsyncEnumerable().SelectAwait(async d =>
            {
                var producto = await _unitOfWork.RepositorioProducto.Get(d.ProductoId);
                _mapper.Map(producto, d);
                return d;
            }).ToListAsync();

            await _unitOfWork.RepositorioVenta.Add(venta);
            await _unitOfWork.CompleteAsync();

            var postVentaDTO = _mapper.Map<PostVentaDTO>(venta);
            return postVentaDTO;
        }

        public async Task PutVenta(VentaDTO ventaDTO)
        {
            var venta = _mapper.Map<Venta>(ventaDTO);
            await _unitOfWork.RepositorioVenta.Update(venta);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteVenta(int id)
        {
            await _unitOfWork.RepositorioVenta.Delete(id);
            await _unitOfWork.CompleteAsync();
        }
    }
}
