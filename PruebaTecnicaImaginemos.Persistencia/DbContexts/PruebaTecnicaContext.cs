using Microsoft.EntityFrameworkCore;
using PruebaTecnicaImaginemos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Persistencia.DbContexts
{
    public class PruebaTecnicaContext : DbContext
    {
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        public PruebaTecnicaContext(DbContextOptions<PruebaTecnicaContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PruebaTecnicaContext).Assembly);
        }
    }
}
