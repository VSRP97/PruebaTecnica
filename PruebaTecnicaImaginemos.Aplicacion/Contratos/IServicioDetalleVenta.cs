using PruebaTecnicaImaginemos.Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Contratos
{
    public interface IServicioDetalleVenta
    {
        Task<IEnumerable<DetalleVentaDTO>> GetDetalleVentas(int ventaId);
        Task<DetalleVentaDTO> GetDetalleVenta(int ventaId, int id);
        Task<DetalleVentaDTO> PostDetalleVenta(DetalleVentaDTO detalleVenta);
        Task<DetalleVentaDTO> PutDetalleVenta(DetalleVentaDTO detalleVenta);
        Task DeleteDetalleVenta(int ventaId, int id);
    }
}
