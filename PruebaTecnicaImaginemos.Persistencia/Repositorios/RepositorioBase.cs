using Microsoft.EntityFrameworkCore;
using PruebaTecnicaImaginemos.Aplicacion.Contratos.Persistencia;
using PruebaTecnicaImaginemos.Persistencia.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Persistencia.Repositorios
{
    public abstract class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        protected readonly PruebaTecnicaContext _context;

        public RepositorioBase(PruebaTecnicaContext context)
        {
            _context = context;
        }

        public async Task Add(T entidad)
        {
            await _context.AddAsync(entidad);
        }

        public async Task Delete(int id)
        {
            _context.Set<T>().Remove(await Get(id));
        }

        public async Task<T> Get(int id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task Update(T entidad)
        {
            _context.Entry(entidad).State = EntityState.Modified;
        }
    }
}
