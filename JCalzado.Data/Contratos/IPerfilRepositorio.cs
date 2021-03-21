using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCalzado.Data.Contratos
{
    public interface IPerfilRepositorio<T> where T : class
    {
        Task<IEnumerable<T>> ObtenerTodosAsync();
        Task<T> ObtenerAsync(int id);
        Task<T> Agregar(T entity);
        Task<bool> Actualizar(T entity);
        Task<bool> Eliminar(int id);
    }
}
