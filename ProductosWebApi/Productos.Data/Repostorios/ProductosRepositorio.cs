using Microsoft.EntityFrameworkCore;
using Productos.Data.Repostorios.Interfaces;
using Productos.Models;
using Productos.Models.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Productos.Data.Repostorios
{
    public class ProductosRepositorio : IProductosRepositorio
    {
        private readonly TiendaDbContext _context;

        public ProductosRepositorio(TiendaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Actualizar(Producto producto)
        {
            _context.Productos.Attach(producto);
            _context.Entry(producto).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<Producto> Agregar(Producto producto)
        {
            _context.Productos.Add(producto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                ;
            }
            return producto;
        }

        public async Task<bool> Eliminar(int id)
        {
            var producto = await _context.Productos.SingleOrDefaultAsync(c => c.Id ==  id);

            producto.Estatus = EstatusProducto.Inactivo;
            _context.Productos.Attach(producto);
            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (System.Exception)
            {
                ;
            }
            return false;
        }

        public async Task<Producto> ObtenerProductoAsync(int id)
        {
            return await _context.Productos.SingleOrDefaultAsync(c => c.Id == id && c.Estatus == EstatusProducto.Activo);
        }

        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            return await _context.Productos.Where(u => u.Estatus == EstatusProducto.Activo).OrderBy(u => u.Nombre).ToListAsync();
        }
    }
}
