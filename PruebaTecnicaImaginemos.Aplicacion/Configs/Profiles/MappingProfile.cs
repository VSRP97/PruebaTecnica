using AutoMapper;
using PruebaTecnicaImaginemos.Aplicacion.DTOs;
using PruebaTecnicaImaginemos.Aplicacion.DTOs.Producto;
using PruebaTecnicaImaginemos.Aplicacion.DTOs.Venta;
using PruebaTecnicaImaginemos.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaImaginemos.Aplicacion.Configs.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Producto, ProductoDTO>()
                .ReverseMap();

            CreateMap<PostProductoRequestDTO, Producto>()
                .ReverseMap();

            CreateMap<Producto, DetalleVenta>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PrecioUnitario, opt => opt.MapFrom(src => src.Precio))
                .ForMember(dest => dest.Total, opt => opt.MapFrom((src, dest) => src.Precio * dest.Cantidad))
                .ReverseMap();

            CreateMap<Venta, VentaDTO>() 
                .ReverseMap();

            CreateMap<PostVentaRequestDTO, Venta>()
                .ForPath(dest => dest.UsuarioNavigation.Nombre, opt => opt.MapFrom(src => src.NombreUsuario))
                .ForPath(dest => dest.UsuarioNavigation.DNI, opt => opt.MapFrom(src => src.DNI))
                .ForMember(dest => dest.DetallesVenta, opt => opt.MapFrom(src => src.Detalles))
                .ReverseMap();

            CreateMap<PostVentaDetalleDTO, DetalleVenta>()
                .ReverseMap();

            CreateMap<Venta, PostVentaDTO>()
                .ForMember(dest => dest.DetalleVentaIds, opt => opt.MapFrom(src => src.DetallesVenta.Select(x => x.Id)))
                .ForPath(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.UsuarioNavigation.Nombre))
                .ReverseMap();

            CreateMap<Usuario, UsuarioDTO>()
                .ReverseMap();

            CreateMap<DetalleVenta, DetalleVentaDTO>()
                .ReverseMap();
        }
    }
}
