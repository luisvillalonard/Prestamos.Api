using Microsoft.IdentityModel.Tokens;
using Prestamos.Core.Dto.Clientes;
using Prestamos.Core.Dto.Configuraciones;
using Prestamos.Core.Dto.DataMaestra;
using Prestamos.Core.Dto.Prestamos;
using Prestamos.Core.Dto.Seguridad;
using Prestamos.Core.Entidades.Clientes;
using Prestamos.Core.Entidades.Configuraciones;
using Prestamos.Core.Entidades.DataMaestra;
using Prestamos.Core.Entidades.Prestamos;
using Prestamos.Core.Entidades.Seguridad;
using System.Globalization;

namespace Prestamos.Infraestructure.Mapeo
{
    public class AutoMapperEntidades : AutoMapperBase
    {
        public AutoMapperEntidades()
        {
            // CLIENTES
            CreateMap<Cliente, ClienteDto>()
                .ForMember(dest => dest.DocumentoTipo, src => src.MapFrom(p => p.DocumentoTipo))
                .ForMember(dest => dest.Sexo, src => src.MapFrom(p => p.Sexo))
                .ForMember(dest => dest.Ciudad, src => src.MapFrom(p => p.Ciudad))
                .ForMember(dest => dest.Ocupacion, src => src.MapFrom(p => p.Ocupacion))
                .ForMember(dest => dest.Usuario, src => src.MapFrom(p => p.Usuario))
                .ForMember(dest => dest.UsuarioActualizado, src => src.MapFrom(p => p.UsuarioIdActualizadoNavigation))
                .ForMember(dest => dest.FechaCreacion, src => src.MapFrom(p => p.FechaCreacion.ToString(Date_DD_MM_YYYY)))
                .ForMember(dest => dest.FechaNacimiento, src => src.MapFrom(p => !p.FechaNacimiento.HasValue ? null : p.FechaNacimiento.Value.ToString(Date_DD_MM_YYYY)))
                .ForMember(dest => dest.FechaAntiguedad, src => src.MapFrom(p => !p.FechaAntiguedad.HasValue ? null : p.FechaAntiguedad.Value.ToString(Date_DD_MM_YYYY)))
                .ForMember(dest => dest.FechaActualizado, src => src.MapFrom(p => !p.FechaActualizado.HasValue ? null : p.FechaActualizado.Value.ToString(Date_DD_MM_YYYY)));
            CreateMap<ClienteDto, Cliente>()
                .ForMember(dest => dest.FechaCreacion, src => src.MapFrom(p => DateOnly.FromDateTime(DateTime.ParseExact(p.FechaCreacion, Date_DD_MM_YYYY, CultureInfo.InvariantCulture))))
                .ForMember(dest => dest.FechaNacimiento, src => src.MapFrom(p => string.IsNullOrEmpty(p.FechaNacimiento) ? new DateOnly?() : DateOnly.FromDateTime(DateTime.ParseExact(p.FechaNacimiento, Date_DD_MM_YYYY, CultureInfo.InvariantCulture))))
                .ForMember(dest => dest.FechaAntiguedad, src => src.MapFrom(p => string.IsNullOrEmpty(p.FechaAntiguedad) ? new DateOnly?() : DateOnly.FromDateTime(DateTime.ParseExact(p.FechaAntiguedad, Date_DD_MM_YYYY, CultureInfo.InvariantCulture))))
                .ForMember(dest => dest.FechaActualizado, src => src.MapFrom(p => string.IsNullOrEmpty(p.FechaActualizado) ? new DateOnly?() : DateOnly.FromDateTime(DateTime.ParseExact(p.FechaActualizado, Date_DD_MM_YYYY, CultureInfo.InvariantCulture))))
                .ForMember(dest => dest.DocumentoTipo, src => src.Ignore())
                .ForMember(dest => dest.Sexo, src => src.Ignore())
                .ForMember(dest => dest.Ciudad, src => src.Ignore())
                .ForMember(dest => dest.Ocupacion, src => src.Ignore())
                .ForMember(dest => dest.Usuario, src => src.Ignore())
                .ForMember(dest => dest.UsuarioIdActualizado, src => src.Ignore());
            CreateMap<Cliente, VwCliente>()
                .ForMember(dest => dest.NompreCompleto, src => src.MapFrom(p => string.Format("{0} {1}", p.Nombres, p.Apellidos).Trim()))
                .ForMember(dest => dest.DocumentoTipo, src => src.MapFrom(p => p.DocumentoTipo.Nombre))
                .ForMember(dest => dest.Sexo, src => src.MapFrom(p => p.Sexo.Nombre))
                .ForMember(dest => dest.Ciudad, src => src.MapFrom(p => p.Ciudad.Nombre))
                .ForMember(dest => dest.Ocupacion, src => src.MapFrom(p => p.Ocupacion.Nombre))
                .ForMember(dest => dest.Estado, src => src.MapFrom(p => p.Activo));

            // DATA MAESTRA
            CreateMap<Acesor, AcesorDto>();
            CreateMap<AcesorDto, Acesor>();
            
            CreateMap<Ciudad, CiudadDto>();
            CreateMap<CiudadDto, Ciudad>();
            
            CreateMap<DocumentoTipo, DocumentoTipoDto>();
            CreateMap<DocumentoTipoDto, DocumentoTipo>();

            CreateMap<FormaPagoFecha, FormaPagoFechaDto>();
            CreateMap<FormaPagoFechaDto, FormaPagoFecha>();
            
            CreateMap<FormaPago, FormaPagoDto>()
                .ForMember(dest => dest.Dias, src => src.MapFrom(p => p.FormaPagoFecha));
            CreateMap<FormaPagoDto, FormaPago>()
                .ForMember(dest => dest.FormaPagoFecha, src => src.MapFrom(p => p.Dias));

            CreateMap<MetodoPago, MetodoPagoDto>();
            CreateMap<MetodoPagoDto, MetodoPago>()
                .ForMember(dest => dest.Prestamo, src => src.Ignore())
                .ForMember(dest => dest.PrestamoPago, src => src.Ignore());

            CreateMap<Moneda, MonedaDto>();
            CreateMap<MonedaDto, Moneda>();

            CreateMap<Ocupacion, OcupacionDto>();
            CreateMap<OcupacionDto, Ocupacion>();

            CreateMap<PrestamoEstado, PrestamoEstadoDto>();
            CreateMap<PrestamoEstadoDto, PrestamoEstado>();

            CreateMap<Sexo, SexoDto>();
            CreateMap<SexoDto, Sexo>();

            // PRESTAMOS
            CreateMap<Prestamo, PrestamoDto>()
                .ForMember(dest => dest.FechaCredito, src => src.MapFrom(p => p.FechaCredito.ToString(Date_DD_MM_YYYY)))
                .ForMember(dest => dest.Cliente, src => src.MapFrom(p => p.Cliente))
                .ForMember(dest => dest.FormaPago, src => src.MapFrom(p => p.FormaPago))
                .ForMember(dest => dest.MetodoPago, src => src.MapFrom(p => p.MetodoPago))
                .ForMember(dest => dest.Estado, src => src.MapFrom(p => p.Estado))
                .ForMember(dest => dest.Cuotas, src => src.MapFrom(p => p.PrestamoCuota));
            CreateMap<PrestamoDto, Prestamo>()
                .ForMember(dest => dest.FechaRegistro, src => src.MapFrom(p => DateOnly.FromDateTime(DateTime.ParseExact(p.FechaRegistro, Date_DD_MM_YYYY, CultureInfo.InvariantCulture))))
                .ForMember(dest => dest.FechaCredito, src => src.MapFrom(p => DateOnly.FromDateTime(DateTime.ParseExact(p.FechaCredito, Date_DD_MM_YYYY, CultureInfo.InvariantCulture))))
                .ForMember(dest => dest.FechaActualizado, src => src.MapFrom(p => string.IsNullOrEmpty(p.FechaActualizado) ? new DateOnly?() : DateOnly.FromDateTime(DateTime.ParseExact(p.FechaActualizado, Date_DD_MM_YYYY, CultureInfo.InvariantCulture))))
                .ForMember(dest => dest.Cliente, src => src.Ignore())
                .ForMember(dest => dest.FormaPago, src => src.Ignore())
                .ForMember(dest => dest.MetodoPago, src => src.Ignore())
                .ForMember(dest => dest.Estado, src => src.Ignore())
                .ForMember(dest => dest.PrestamoCuota, src => src.MapFrom(p => p.Cuotas));

            CreateMap<PrestamoCuota, PrestamoCuotaDto>()
                .ForMember(dest => dest.FechaPago, src => src.MapFrom(p => p.FechaPago.ToString(Date_DD_MM_YYYY)))
                .ForMember(dest => dest.Pagos, src => src.MapFrom(p => p.PrestamoPago))
                .ForMember(dest => dest.Vencido, src => src.MapFrom(p => p.FechaPago < DateOnly.FromDateTime(DateTime.Now) ? true : false));
            CreateMap<PrestamoCuotaDto, PrestamoCuota>()
                .ForMember(dest => dest.FechaPago, src => src.MapFrom(p => DateOnly.FromDateTime(DateTime.ParseExact(p.FechaPago, Date_DD_MM_YYYY, CultureInfo.InvariantCulture))))
                .ForMember(dest => dest.PrestamoPago, src => src.Ignore());
            
            CreateMap<PrestamoPago, PrestamoPagoDto>();
            CreateMap<PrestamoPagoDto, PrestamoPago>();

            // SEGURIDAD
            CreateMap<Rol, RolDto>()
                .ForMember(dest => dest.Permisos, src => src.MapFrom(p => p.Permiso));
            CreateMap<RolDto, Rol>()
                .ForMember(dest => dest.Permiso, src => src.MapFrom(p => p.Permisos));
            
            CreateMap<Permiso, PermisoDto>();
            CreateMap<PermisoDto, Permiso>();

            CreateMap<Usuario, UserApp>();
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<UsuarioDto, Usuario>();

            // CONFIGURACIONES
            CreateMap<Configuracion, ConfiguracionDto>();
            CreateMap<ConfiguracionDto, Configuracion>();

        }
    }
}
