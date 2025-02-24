using Prestamos.Core.Attributos;

namespace Prestamos.Core.Enumerables
{
    public enum Menu
    {
        /* PRINCIPALES */
        Ninguno = 0,


        /* CLIENTES */
        [Menu(Id = (int)Clientes, Titulo = "Clientes", Padre = (int)Ninguno, Orden = (int)Clientes)]
        Clientes = 10,
        
        [Menu(Id = (int)ClientesFormulario, Titulo = "Formulario", Padre = (int)Clientes, Link = "clientes/formulario", Orden = (int)ClientesFormulario)]
        ClientesFormulario = 11,
        
        [Menu(Id = (int)ClientesRegistrados, Titulo = "Clientes Registrados", Padre = (int)Clientes, Link = "clientes/historico", Orden = (int)ClientesRegistrados)]
        ClientesRegistrados = 12,


        /* PRESTAMOS */
        [Menu(Id = (int)Prestamos, Titulo = "Autorizaciones", Padre = (int)Ninguno, Orden = (int)Prestamos)]
        Prestamos = 20,

        [Menu(Id = (int)PrestamosFormulario, Titulo = "Formulario de Prestamos", Padre = (int)Prestamos, Link = "prestamos/formulario", Orden = (int)PrestamosFormulario)]
        PrestamosFormulario = 21,
        
        [Menu(Id = (int)PrestamosRegistrados, Titulo = "Prestamos Registrados", Padre = (int)Prestamos, Link = "prestamos/registrados", Orden = (int)PrestamosRegistrados)]
        PrestamosRegistrados = 22,
        
        [Menu(Id = (int)PrestamosCobros, Titulo = "Cobro de Prestamo", Padre = (int)Prestamos, Link = "prestamos/cobros", Orden = (int)PrestamosCobros)]
        PrestamosCobros = 23,


        /* DATA MAESTRA */
        [Menu(Id = (int)DataMaestra, Titulo = "Data Maestra", Padre = (int)Ninguno, Orden = (int)DataMaestra)]
        DataMaestra = 30,

        [Menu(Id = (int)DataMaestraCiudades, Titulo = "Ciudades", Link = "maestra/ciudades", Padre = (int)DataMaestra, Orden = (int)DataMaestraCiudades)]
        DataMaestraCiudades = 31,

        [Menu(Id = (int)DataMaestraTiposDocumentos, Titulo = "Tipos de Documentos", Link = "maestra/documentos/tipos", Padre = (int)DataMaestra, Orden = (int)DataMaestraTiposDocumentos)]
        DataMaestraTiposDocumentos = 32,

        [Menu(Id = (int)DataMaestraFormasPago, Titulo = "Formas de Pago", Link = "maestra/formasPago", Padre = (int)DataMaestra, Orden = (int)DataMaestraFormasPago)]
        DataMaestraFormasPago = 33,

        [Menu(Id = (int)DataMaestraMetodosPago, Titulo = "Métodos de Pago", Link = "maestra/metodosPago", Padre = (int)DataMaestra, Orden = (int)DataMaestraMetodosPago)]
        DataMaestraMetodosPago = 34,

        [Menu(Id = (int)DataMaestraTiposMonedas, Titulo = "Tipo de Monedas", Link = "maestra/monedas", Padre = (int)DataMaestra, Orden = (int)DataMaestraTiposMonedas)]
        DataMaestraTiposMonedas = 35,

        [Menu(Id = (int)DataMaestraOcupaciones, Titulo = "Ocupaciones", Link = "maestra/ocupaciones", Padre = (int)DataMaestra, Orden = (int)DataMaestraOcupaciones)]
        DataMaestraOcupaciones = 36,

        [Menu(Id = (int)DataMaestraEstadosPrestamos, Titulo = "Estados de Prestamos", Link = "maestra/prestamos/estados", Padre = (int)DataMaestra, Orden = (int)DataMaestraEstadosPrestamos)]
        DataMaestraEstadosPrestamos = 37,

        [Menu(Id = (int)DataMaestraAcesores, Titulo = "Acesores", Link = "maestra/acesores", Padre = (int)DataMaestra, Orden = (int)DataMaestraAcesores)]
        DataMaestraAcesores = 38,


        /* SEGURIDAD */
        [Menu(Id = (int)Seguridad, Titulo = "Seguridad", Padre = (int)Ninguno, Orden = (int)Seguridad)]
        Seguridad = 40,

        [Menu(Id = (int)Perfiles, Titulo = "Perfiles de Usuarios", Link = "maestra/roles", Padre = (int)Seguridad, Orden = (int)Perfiles)]
        Perfiles = 41,

        [Menu(Id = (int)Permisos, Titulo = "Permisos", Link = "maestra/permisos", Padre = (int)Seguridad, Orden = (int)Permisos)]
        Permisos = 42,

        [Menu(Id = (int)Usuarios, Titulo = "Usuarios", Link = "maestra/usuarios", Padre = (int)Seguridad, Orden = (int)Usuarios)]
        Usuarios = 43,
    }
}
