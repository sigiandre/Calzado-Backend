using JCalzado.Data.Contratos;
using JCalzado.Models;
using JCalzado.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCalzado.Data.Repositorios
{
    public class ProductosRepositorio : IProductosRepositorio
    {
        private readonly TiendaDbContext _context;
        private readonly ILogger<ProductosRepositorio> _logger;

        public ProductosRepositorio(TiendaDbContext context, ILogger<ProductosRepositorio> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Producto>> ObtenerProductosAsync()
        {
            return await _context.Productos
                .Where(u => u.Estatus == EstatusProducto.Activo)
                .OrderBy(u => u.Nombre)
                .ToListAsync();
        }
        
        public async Task<Producto> ObtenerProductoAsync(int id)
        {
            return await _context.Productos.SingleOrDefaultAsync(c => c.Id == id && c.Estatus == EstatusProducto.Activo);
        }
        
        public async Task<Producto> Agregar(Producto producto)
        {
            producto.Estatus = EstatusProducto.Activo;
            producto.FechaRegistro = DateTime.Now;

            _context.Productos.Add(producto);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Agregar)}: {ex.Message}");
                return null;
            }
            return producto;
        }
        
        public async Task<bool> Actualizar(Producto producto)
        {
            var productoBd = await ObtenerProductoAsync(producto.Id);
            productoBd.Nombre = producto.Nombre;
            productoBd.Precio = producto.Precio;

            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Actualizar)}: {ex.Message}");
            }
            return false;
        }

        public async Task<bool> Eliminar(int id)
        {
            //Se realiza una eliminacion suave, solamente inactivamos el producto
            var producto = await _context.Productos.SingleOrDefaultAsync(c => c.Id == id);

            producto.Estatus = EstatusProducto.Inactivo;
            _context.Productos.Attach(producto);
            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Eliminar)}: {ex.Message}");
            }
            return false;
        }
    }
}