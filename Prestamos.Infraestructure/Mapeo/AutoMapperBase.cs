using AutoMapper;

namespace Prestamos.Infraestructure.Mapeo
{

    public abstract class AutoMapperBase : Profile
    {
        public readonly string Date_DD_MM_YYYY = "dd-MM-yyyy";
        public readonly string Date_YYYY_MM_DD = "yyyy-MM-dd";
        public readonly string Date_YYYYMMDD = "yyyyMMdd";
        public readonly string Date_YYYYDDMM = "yyyyddMM";
        public readonly string Time_HH_MM_SS = "hh\\:mm\\:ss";
    }
}
