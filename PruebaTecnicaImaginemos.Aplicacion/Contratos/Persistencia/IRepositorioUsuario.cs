using PruebaTecnicaImaginemos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        Task<IEnumerable<Usuario>> GetList(string search);
    }
}
