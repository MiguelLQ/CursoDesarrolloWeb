using Ecomerce.share.Entities;

namespace Ecomerce.backend.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllAsync();
        Task<Producto> GetByIdAsync(int id);
        Task AddAsync(Producto producto);
        Task UpdateAsync(Producto producto);
        Task DeleteAsync(int id);
        Task<Categoria?> GetCategoriaByIdAsync(int id); // Add this method to resolve the error  
    }
}
