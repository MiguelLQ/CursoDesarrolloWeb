﻿using Ecomerce.backend.Data;
using Ecomerce.backend.Services;
using Ecomerce.share.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecomerce.backend.Repositories
{
    public class ProductoRepository(DataContext context, IFilesService filesService) : IProductoRepository
    {
        private readonly DataContext _context = context;
        private readonly IFilesService _service = filesService;


        public async Task AddAsync(Producto producto)
        {
           await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await _context.Productos.FindAsync(id) ?? throw new Exception($"Producto con id {id} no fue encontrado.");
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producto>> GetAllAsync()
        {
            var productos =await _context.Productos
                .Include(p => p.Categoria)
                .ToListAsync();

            return productos;
        }

        public async Task<Producto> GetByIdAsync(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id) ?? throw new Exception($"Producto con id {id} no fue encontrado.");
            return producto;
        }

        public async Task UpdateAsync(Producto producto)
        {
            var productoExistente = await _context.Productos.FindAsync(producto.Id) ?? throw new Exception($"Producto con id {producto.Id} no fue encontrado.");
            productoExistente.Nombre = producto.Nombre;
            productoExistente.Precio = producto.Precio;
            productoExistente.stock = producto.stock;
            productoExistente.Descripcion = producto.Descripcion;
            productoExistente.Categoria = producto.Categoria;
            if (!string.IsNullOrEmpty(producto.URLfoto) && producto.URLfoto != productoExistente.URLfoto)
            {
                productoExistente.URLfoto= producto.URLfoto;
            }
            else if(string.IsNullOrEmpty(productoExistente.URLfoto)&& !string.IsNullOrEmpty(producto.URLfoto))
            {
               productoExistente.URLfoto = await _service.UploadImage(producto.URLfoto);
            }
            _context.Entry(productoExistente).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception($"Categoria con id {id} no fue encontrada.");
            return categoria;
        }
        public async Task<(IEnumerable<Producto> Productos, int TotalCount)> GetPaginatedAsync(int page, int pageSize)
        {
            var totalCount = await _context.Productos.CountAsync();
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (productos, totalCount);
        }
    }
}
