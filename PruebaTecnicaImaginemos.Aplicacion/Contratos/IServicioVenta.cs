using PruebaTecnicaImaginemos.Aplicacion.DTOs.Venta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Contratos
{
    public interface IServicioVenta
    {
        Task<IEnumerable<VentaDTO>> GetVentas(DateTime? fechaInicio, DateTime? fechaFin, string search);
        Task<VentaDTO> GetVenta(int id);
        Task<PostVentaDTO> PostVenta(PostVentaRequestDTO postVentaRequest);
        Task PutVenta(VentaDTO ventaDTO);
        Task DeleteVenta(int id);
    }
}
