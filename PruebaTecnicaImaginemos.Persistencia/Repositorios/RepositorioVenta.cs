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
    public class RepositorioVenta : RepositorioBase<Venta>, IRepositorioVenta
    {
        private readonly PruebaTecnicaContext _context;

        public RepositorioVenta(PruebaTecnicaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venta>> GetList(string search, DateTime fechaInicio, DateTime fechaFin)
        {
            var query = _context.Set<Venta>()
                                .Include(e => e.UsuarioNavigation)
                                .Where(e => e.UsuarioNavigation.Nombre.Contains(search) || e.UsuarioNavigation.DNI.Contains(search))
                                .Where(e => e.Fecha >= fechaInicio && e.Fecha <= fechaFin);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Venta>> GetList(string search)
        {
            var query = _context.Set<Venta>()
                                .Include(e => e.UsuarioNavigation)
                                .Where(e => e.UsuarioNavigation.Nombre.Contains(search) || e.UsuarioNavigation.DNI.Contains(search));

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Venta>> GetList(DateTime fechaInicio, DateTime fechaFin)
        {
            var query = _context.Set<Venta>()
                                .Include(e => e.UsuarioNavigation)
                                .Where(e => e.Fecha >= fechaInicio && e.Fecha <= fechaFin);

            return await query.ToListAsync();
        }
    }
}
