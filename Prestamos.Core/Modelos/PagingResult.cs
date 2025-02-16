using System;

namespace Prestamos.Core.Modelos
{
    public class PagingResult : RequestFilter, IDisposable
    {
        public int TotalRecords { get; set; }

        public bool HasCurrentPage => CurrentPage > 0;

        public bool HasPageSize => PageSize > 0;

        public bool HasPreviousPage => PreviousPage.HasValue;

        public bool HasNextPage => NextPage.HasValue;

        public int TotalPage => TotalRecords == 0 || !HasPageSize ? 0 : PageSize == 0 ? 0 : (((TotalRecords + PageSize) - 1) / PageSize);

        public int? PreviousPage => TotalPage <= 0 || !HasCurrentPage || CurrentPage <= 1 ? null : CurrentPage - 1;

        public int? NextPage => TotalPage <= 0 || !HasCurrentPage || CurrentPage == TotalPage ? null : CurrentPage + 1;

        public string Descripcion
        {
            get
            {
                int desde = HasCurrentPage && HasPageSize
                    ? ((CurrentPage - 1) * PageSize) + 1
                    : 0;

                if (desde < 0)
                    desde = 0;

                else if (desde > TotalRecords)
                    desde = (int)(TotalRecords - (PageSize - ((TotalPage * PageSize) - TotalRecords))) + 1;

                int hasta = !HasCurrentPage || !HasPageSize
                    ? 0
                    : TotalPage == CurrentPage
                        ? desde + (TotalRecords - desde)
                        : CurrentPage * PageSize;

                string rango = TotalRecords > PageSize ? $"({desde:N0} a {hasta:N0})" : string.Empty;

                string totalRegistros = $"{TotalRecords:N0} registro{(TotalRecords > 1 ? "s" : "")}".Trim();

                return $"{totalRegistros} {rango}".Trim();
            }
        }

        public PagingResult() { }

        public PagingResult(int totalRecords)
        {
            TotalRecords = totalRecords;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
