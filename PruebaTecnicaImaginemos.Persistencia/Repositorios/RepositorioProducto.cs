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
    public class RepositorioProducto : RepositorioBase<Producto>, IRepositorioProducto
    {
        private readonly PruebaTecnicaContext _context;

        public RepositorioProducto(PruebaTecnicaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetList(string nombre)
        {
            var query = _context.Set<Producto>().Where(e => e.Nombre.Contains(nombre));
            return await query.ToListAsync();
        }
    }
}
