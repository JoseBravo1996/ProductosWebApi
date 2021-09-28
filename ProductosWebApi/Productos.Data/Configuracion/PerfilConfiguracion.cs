using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Productos.Models;

namespace Productos.Data.Configuracion
{
    public class PerfilConfiguracion : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> entity)
        {
            entity.ToTable("Perfil", "tienda");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        }
    }
}
