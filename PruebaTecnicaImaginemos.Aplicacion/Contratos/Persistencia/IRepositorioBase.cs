using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<T> Get(int id);
        Task Add(T entidad);
        Task Update(T entidad);
        Task Delete(int id);
    }
}
