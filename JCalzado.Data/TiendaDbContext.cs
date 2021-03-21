using System;
using JCalzado.Data.Configuracion;
using JCalzado.Models;
using Microsoft.EntityFrameworkCore;

namespace JCalzado.Data
{
    public partial class TiendaDbContext : DbContext
    {
        public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base(options){}
        public TiendaDbContext()
        {
        }
        public virtual DbSet<DetalleOrden> DetallesOrden { get; set; }
        public virtual DbSet<Orden> Ordenes { get; set; }
        public virtual DbSet<Perfil> Perfiles { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
