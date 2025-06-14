using Ecomerce.share.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce.backend.Data
{
    public class SeedDb(DataContext context)
    {
        private readonly DataContext _context = context;

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCategoriasAsync();
            await CheckUsersAsync("Super Admin", "admin@gmail.com", "admin123", "Administrador", "+51926649317");
        }

        private async Task<Usuario> CheckUsersAsync(string nombre, string correo, string password, string perfil, string telefono)
        {
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuarioExistente != null)
            {
                return usuarioExistente!;
            }

            Usuario usuario = new()
            {
                Correo = correo,
                Nombre = nombre,
                Perfil = perfil,
                URLFoto = "https://res.cloudinary.com/dwexvcqmn/image/upload/v1749831146/user_uorb9w.png",
                Password = password,
                Telefono = telefono,
            };

            usuario.Password = BCrypt.Net.BCrypt.HashPassword(usuario.Password);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        private async Task CheckCategoriasAsync()
        {
            if (!_context.Categorias.Any())
            {
                _context.Categorias.Add(new Categoria { Nombre = "Tecnologia" });
                _context.Categorias.Add(new Categoria { Nombre = "Deportes" });
                _context.Categorias.Add(new Categoria { Nombre = "Hogar" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
