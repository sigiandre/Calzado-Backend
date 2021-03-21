using JCalzado.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCalzado.Data.Contratos
{
    public interface IOrdenesRepositorio : IPerfilRepositorio<Orden>
    {
        Task<IEnumerable<Orden>> ObtenerTodosConDetallesAsync();
        Task<Orden> ObtenerConDetallesAsync(int id);
    }
}
