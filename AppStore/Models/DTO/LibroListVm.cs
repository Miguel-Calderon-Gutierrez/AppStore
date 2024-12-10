using AppStore.Models.Domain;

namespace AppStore.Models.DTO
{
    public class LibroListVm
    {
        public IQueryable<Libro>? LibroList { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public string? Term { get; set; }
    }
}
