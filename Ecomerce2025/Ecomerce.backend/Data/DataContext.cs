using Ecomerce.share.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce.backend.Data
{
    public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
    {
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Categoria>().HasIndex(c => c.Nombre).IsUnique();
            modelBuilder.Entity<Categoria>().HasKey(c => c.Id);
            modelBuilder.Entity<Categoria>().Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Usuario>().HasIndex(c => c.Correo).IsUnique();

        }
    }

}
