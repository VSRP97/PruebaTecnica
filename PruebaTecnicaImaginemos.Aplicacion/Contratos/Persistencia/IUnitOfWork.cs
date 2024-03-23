using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepositorioProducto RepositorioProducto { get; }
        IRepositorioUsuario RepositorioUsuario { get; }
        IRepositorioVenta RepositorioVenta { get; }
        IRepositorioDetalleVenta RepositorioDetalleVenta { get; }

        Task CompleteAsync();
    }
}
