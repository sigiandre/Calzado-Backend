using JCalzado.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JCalzado.Data.Contratos
{
    public interface IUsuariosRepositorio : IPerfilRepositorio<Usuario>
    {
        Task<bool> CambiarContrasena(Usuario usuario);
        Task<bool> CambiarPerfil(Usuario usuario);
        Task<bool> ValidarContrasena(Usuario usuario);
        Task<(bool resultado, Usuario usuario)> ValidarDatosLogin(Usuario datosLoginUsuario);
    }
}
