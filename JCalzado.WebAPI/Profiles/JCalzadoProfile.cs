using AutoMapper;
using JCalzado.Dtos;
using JCalzado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCalzado.WebAPI.Profiles
{
    public class JCalzadoProfile : Profile
    {
        public JCalzadoProfile()
        {
            CreateMap<Producto, ProductoDto>().ReverseMap();

            CreateMap<Perfil, PerfilDto>().ReverseMap();

            CreateMap<Orden, OrdenDto>()
                .ForMember(u => u.Usuario, p => p.MapFrom(m => m.Usuario.Username))
                .ReverseMap()
                .ForMember(u => u.Usuario, p => p.Ignore());
            
            CreateMap<DetalleOrden, DetalleOrdenDto>()
                .ForMember(u => u.Producto, p => p.MapFrom(u => u.Producto.Nombre))
                .ReverseMap()
                .ForMember(u => u.Producto, p => p.Ignore());

            CreateMap<Usuario, UsuarioRegistroDto>()
                .ForMember(u => u.Perfil, p => p.MapFrom(m => m.Perfil.Nombre))
                .ReverseMap()
                .ForMember(u => u.Perfil, p => p.Ignore());

            CreateMap<Usuario, UsuarioActualizacionDto>()
                .ReverseMap();

            CreateMap<Usuario, UsuarioListaDto>()
                .ForMember(u => u.Perfil, p => p.MapFrom(m => m.Perfil.Nombre))
                .ForMember(u => u.NombreCompleto, p => p.MapFrom(m => string.Format("{0} {1}",
                        m.Nombre, m.Apellidos)))
                .ReverseMap();

            CreateMap<Usuario, LoginModelDto>().ReverseMap();

            CreateMap<Usuario, PerfilUsuarioDto>().ReverseMap();
        }
    }
}
