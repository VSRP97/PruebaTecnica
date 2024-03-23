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
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public RepositorioUsuario(PruebaTecnicaContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Usuario>> GetList(string search)
        {
            var query = _context.Set<Usuario>()
                                .Where(e => e.Nombre.ToLower().Contains(search.ToLower()) || e.DNI.ToLower().Contains(search.ToLower()));

            return await query.ToListAsync();
        }
    }
}
