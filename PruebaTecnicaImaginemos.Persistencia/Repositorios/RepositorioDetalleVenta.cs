using Microsoft.EntityFrameworkCore;
using PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia;
using PruebaTecnicaImaginemos.Dominio.Entidades;
using PruebaTecnicaImaginemos.Persistencia.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Persistencia.Repositorios
{
    public class RepositorioDetalleVenta : RepositorioBase<DetalleVenta>, IRepositorioDetalleVenta
    {
        private readonly PruebaTecnicaContext _context;

        public RepositorioDetalleVenta(PruebaTecnicaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DetalleVenta> Get(int idVenta, int idDetalle)
        {
            return await _context.Set<DetalleVenta>()
                                .FirstOrDefaultAsync(e => e.VentaId == idVenta && e.Id == idDetalle);
        }

        public async Task<IEnumerable<DetalleVenta>> GetList(int idVenta)
        {
            var query = _context.Set<DetalleVenta>().Where(e => e.VentaId == idVenta);

            return await query.ToListAsync();
        }

        public async Task Delete(int idVenta, int idDetalle)
        {
            var result = await _context.Set<DetalleVenta>()
                .FirstOrDefaultAsync(e => e.VentaId == idVenta && e.Id == idDetalle);
            _context.Set<DetalleVenta>().Remove(result);
        }
    }
}
