using JCalzado.Data.Contratos;
using JCalzado.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCalzado.Data.Repositorios
{
    public class PerfilRepositorio : IPerfilRepositorio<Perfil>
    {
        private readonly TiendaDbContext _context;
        private readonly ILogger<PerfilRepositorio> _logger;
        private DbSet<Perfil> _dbSet;

        public PerfilRepositorio(TiendaDbContext context, ILogger<PerfilRepositorio> logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = _context.Set<Perfil>();
        }

        public async Task<IEnumerable<Perfil>> ObtenerTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<Perfil> ObtenerAsync(int id)
        {
            return await _dbSet.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Perfil> Agregar(Perfil entity)
        {
            _dbSet.Add(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Agregar)}: " + ex.Message);
                return null;
            }
            return entity;
        }

        public async Task<bool> Actualizar(Perfil entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error en {nameof(Actualizar)}: " + ex.Message);
            }
            return false;
        }

        public async Task<bool> Eliminar(int id)
        {
            var entity = await _dbSet.SingleOrDefaultAsync(u => u.Id == id);
            _dbSet.Remove(entity);

            try
            {
                return (await _context.SaveChangesAsync() > 0 ? true : false);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Eliminar)}: " + ex.Message);
            }
            return false;
        }
    }
}
