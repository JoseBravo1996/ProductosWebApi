using Microsoft.EntityFrameworkCore;
using Productos.Data.Configuracion;
using Productos.Models;


#nullable disable

namespace Productos.Data
{
    public partial class TiendaDbContext : DbContext
    {
        public TiendaDbContext()
        {
        }

        public TiendaDbContext(DbContextOptions<TiendaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DetalleOrden> DetalleOrdens { get; set; }
        public virtual DbSet<Orden> Ordens { get; set; }
        public virtual DbSet<Perfil> Perfils { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=JOSEBRAVO1\\SQLEXPRESS;Initial Catalog=TiendaDb;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.ApplyConfiguration(new DetalleOrdenConfiguracion());

            modelBuilder.ApplyConfiguration(new OrdenConfiguracion());

            modelBuilder.ApplyConfiguration(new PerfilConfiguracion());

            modelBuilder.ApplyConfiguration(new ProductoConfiguracion());

            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
            
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
