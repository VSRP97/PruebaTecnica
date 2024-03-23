using PruebaTecnicaImaginemos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia
{
    public interface IRepositorioVenta : IRepositorioBase<Venta>
    {
        Task<IEnumerable<Venta>> GetList(string search, DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<Venta>> GetList(string search);
        Task<IEnumerable<Venta>> GetList(DateTime fechaInicio, DateTime fechaFin);
    }
}
