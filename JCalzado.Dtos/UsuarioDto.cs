using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JCalzado.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Nombre del Usuario")]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellidos del Usuario")]
        public string Apellidos { get; set; }
        [Required]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Cuenta")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Perfil del usuario")]
        public int PerfilId { get; set; }
        public string Perfil { get; set; }
    }
}
