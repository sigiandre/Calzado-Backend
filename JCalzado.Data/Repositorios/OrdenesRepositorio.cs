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
    public class OrdenesRepositorio : IOrdenesRepositorio
    {
        private readonly TiendaDbContext _context;
        private readonly ILogger<OrdenesRepositorio> _logger;
        private DbSet<Orden> _dbSet;

        public OrdenesRepositorio(TiendaDbContext context, ILogger<OrdenesRepositorio> logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = _context.Set<Orden>();
        }

        public async Task<bool> Actualizar(Orden entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Erron en {nameof(Actualizar)}: " + ex.Message);
            }
            return false;
        }

        public async Task<Orden> Agregar(Orden entity)
        {
            entity.EstatusOrden = EstatusOrden.Activo;
            entity.FechaRegistro = DateTime.Now;
            _dbSet.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erron en {nameof(Agregar)}: " + ex.Message);
                return null;
            }
            return entity;
        }

        public async Task<bool> Eliminar(int id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(u => u.Id == id);
            entity.EstatusOrden = EstatusOrden.Inactivo;

            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erron en {nameof(Eliminar)}: " + ex.Message);
            }
            return false;
        }

        public async Task<Orden> ObtenerAsync(int id)
        {
            return await _dbSet.Include(orden => orden.Usuario)
                .SingleOrDefaultAsync(c => c.Id == id && c.EstatusOrden == EstatusOrden.Activo);
        }

        public async Task<Orden> ObtenerConDetallesAsync(int id)
        {
            return await _dbSet.Include(orden => orden.Usuario)
                .Include(orden => orden.DetalleOrden)
                .ThenInclude(detalleOrden => detalleOrden.Producto)
                .SingleOrDefaultAsync(c => c.Id == id && c.EstatusOrden == EstatusOrden.Activo);
        }

        public async Task<IEnumerable<Orden>> ObtenerTodosAsync()
        {
            return await _dbSet.Where(u => u.EstatusOrden == EstatusOrden.Activo)
                .Include(orden => orden.Usuario)
                .ToListAsync();
        }

        public async Task<IEnumerable<Orden>> ObtenerTodosConDetallesAsync()
        {
            return await _dbSet.Where(u => u.EstatusOrden == EstatusOrden.Activo)
                .Include(orden => orden.Usuario)
                .Include(orden => orden.DetalleOrden)
                .ThenInclude(detalleOrden => detalleOrden.Producto)
                .ToListAsync();
        }
    }
}
