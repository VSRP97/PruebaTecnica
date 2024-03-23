using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaImaginemos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Persistencia.Configs.Entidades
{
    public class DetalleVentaConfiguration : IEntityTypeConfiguration<DetalleVenta>
    {
        public void Configure(EntityTypeBuilder<DetalleVenta> builder)
        {
            builder.ToTable("detalles_ventas");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.ProductoId)
                .HasColumnName("producto_id");

            builder.Property(e => e.VentaId)
                .HasColumnName("venta_id");

            builder.Property(e => e.Cantidad)
                .HasColumnName("cantidad");

            builder.Property(e => e.PrecioUnitario)
                .HasColumnName("precio_unitario");

            builder.Property(e => e.Total)
                .HasColumnName("total");

            builder.HasOne(e => e.ProductoNavigation)
                .WithMany(f => f.DetallesVenta)
                .HasForeignKey(e => e.ProductoId);

            builder.HasOne(e => e.VentaNavigation)
                .WithMany(f => f.DetallesVenta)
                .HasForeignKey(e => e.VentaId);
        }
    }
}
