using PruebaTecnicaImaginemos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia
{
    public interface IRepositorioDetalleVenta : IRepositorioBase<DetalleVenta>
    {
        Task<DetalleVenta> Get(int idVenta, int idDetalle);
        Task<IEnumerable<DetalleVenta>> GetList(int idVenta);
        Task Delete(int idVenta, int idDetalle);
    }
}
