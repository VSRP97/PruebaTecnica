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
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ToTable("productos");

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .UseIdentityColumn();

            builder.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");

            builder.Property(e => e.Precio)
                .HasColumnName("precio");

            builder.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .HasColumnName("descripcion");
        }
    }
}
