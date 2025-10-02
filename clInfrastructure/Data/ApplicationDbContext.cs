using clDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace clInfrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DetalleOrden> DetalleOrdenes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("Usuarios");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id).HasColumnName("Id"); 
                entity.Property(u => u.Nombre).HasMaxLength(200);
                entity.Property(u => u.Clave).HasMaxLength(100);
                entity.Property(u => u.IdRol).HasColumnName("IdRol"); 

                
                entity.HasOne(u => u.Rol)
                    .WithMany(r => r.Usuarios)
                    .HasForeignKey(u => u.IdRol)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Roles");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Id).HasColumnName("Id");
                entity.Property(r => r.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Orden>(entity =>
            {
                entity.ToTable("Ordenes");
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Id).HasColumnName("Id");
                entity.Property(o => o.Fecha).HasColumnType("datetime");
                entity.Property(o => o.Cliente).HasMaxLength(500);
                entity.Property(o => o.Total).HasColumnType("decimal(12,2)");

                entity.HasMany(o => o.Detalles)
                    .WithOne(d => d.Orden)
                    .HasForeignKey(d => d.IdOrden)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DetalleOrden>(entity =>
            {
                entity.ToTable("DetalleOrdenes");
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Id).HasColumnName("Id");
                entity.Property(d => d.IdOrden).HasColumnName("IdOrden");
                entity.Property(d => d.Producto).HasMaxLength(500);
                entity.Property(d => d.Cantidad).HasColumnType("decimal(12,2)");
                entity.Property(d => d.PrecioUnitario).HasColumnType("decimal(12,2)");
                entity.Property(d => d.SubTotal).HasColumnType("decimal(12,2)");
            });
        }
    }
}
