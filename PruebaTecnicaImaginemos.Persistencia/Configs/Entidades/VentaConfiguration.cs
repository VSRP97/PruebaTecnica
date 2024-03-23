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
    public class VentaConfiguration : IEntityTypeConfiguration<Venta>
    {
        public void Configure(EntityTypeBuilder<Venta> builder)
        {
            builder.ToTable("ventas");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.UsuarioId)
                .HasColumnName("id_usuario");

            builder.Property(e => e.Fecha)
                .HasColumnName("fecha");

            builder.Property(e => e.Total)
                .HasColumnName("total");

            builder.HasOne(e => e.UsuarioNavigation)
                .WithMany(f => f.Ventas)
                .HasForeignKey(e => e.UsuarioId);
        }
    }
}
