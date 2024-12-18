using AppStore.Models.Domain;

namespace AppStore.Models.DTO
{
    public class CategoriaListVm
    {
        public string? Nombre { get; set; }
        public IQueryable<Categoria>? CategoriaList { get; set; }
    }
}
