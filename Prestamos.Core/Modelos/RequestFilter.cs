namespace Prestamos.Core.Modelos
{
    public class RequestFilter
    {
        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public string Filter { get; set; } = string.Empty;
        public RequestFilter() { }
    }
}
