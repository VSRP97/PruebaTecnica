using PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia;
using PruebaTecnicaImaginemos.Persistencia.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Persistencia.Repositorios
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PruebaTecnicaContext _context;
        private bool _disposed;

        #region Implementaciones
        private RepositorioProducto _repoProducto;
        private RepositorioDetalleVenta _repoDetalleVenta;
        private RepositorioUsuario _repoUsuario;
        private RepositorioVenta _repoVenta;
        #endregion

        #region Interfases
        public IRepositorioProducto RepositorioProducto => _repoProducto ??= new RepositorioProducto(_context);
        public IRepositorioDetalleVenta RepositorioDetalleVenta => _repoDetalleVenta ??= new RepositorioDetalleVenta(_context);
        public IRepositorioUsuario RepositorioUsuario => _repoUsuario ??= new RepositorioUsuario(_context);
        public IRepositorioVenta RepositorioVenta => _repoVenta ??= new RepositorioVenta(_context);
        #endregion

        public UnitOfWork(PruebaTecnicaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CompleteAsync() => await _context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        protected virtual async ValueTask DisposeAsync(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    await _context.DisposeAsync();
                }
            }

            _disposed = true;
        }
    }
}
